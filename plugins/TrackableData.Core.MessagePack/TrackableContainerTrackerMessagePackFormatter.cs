using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using MessagePack;
using MessagePack.Formatters;

namespace TrackableData.MessagePack
{
    public class TrackableContainerTrackerMessagePackFormatter<T> : IMessagePackFormatter<T>
    {
        struct Changed
        {
            public string PropertyName;
            public ITracker Value;

            public Changed(string propertyName, ITracker value)
            {
                PropertyName = propertyName;
                Value = value;
            }
        }

        static MethodInfo SerializeMethodInfo;
        static MethodInfo DeserializeMethodInfo;
        private static Dictionary<Type, MethodInfo> _serializeMethodInfos = new Dictionary<Type, MethodInfo>();
        private static Dictionary<Type, MethodInfo> _deserializeMethodInfos = new Dictionary<Type, MethodInfo>();

        static TrackableContainerTrackerMessagePackFormatter()
        {
            var type = typeof(MessagePackSerializer);
            var methods = type.GetMethods(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy);
            foreach (var methodInfo in methods)
            {
                var parameters = methodInfo.GetParameters();
                if (methodInfo.Name == "Deserialize" && parameters.Length == 2 &&
                    parameters[0].Name == "byteSequence" &&
                    parameters[1].ParameterType == typeof(MessagePackSerializerOptions))
                {
                    DeserializeMethodInfo = methodInfo;
                }
                else if (methodInfo.Name == "Serialize" && parameters.Length == 3 &&
                         parameters[0].ParameterType == typeof(Stream) &&
                         parameters[1].ParameterType.IsGenericParameter &&
                         parameters[2].ParameterType == typeof(MessagePackSerializerOptions))
                {
                    SerializeMethodInfo  = methodInfo;
                }
            }
        }

        public void Serialize(ref MessagePackWriter writer, T value, MessagePackSerializerOptions options)
        {
            var properties = new List<Changed>();
            foreach (var pi in typeof(T).GetProperties(BindingFlags.Instance | BindingFlags.Public))
            {
                if (typeof(ITracker).IsAssignableFrom(pi.PropertyType) == false)
                    continue;

                var subTracker = (ITracker) pi.GetValue(value, null);
                if (subTracker != null && subTracker.HasChange)
                    properties.Add(new Changed(pi.Name, subTracker));
            }

            writer.WriteArrayHeader(properties.Count);
            foreach (var pi in properties)
            {
                writer.Write(pi.PropertyName);
            }

            foreach (var pi in properties)
            {
                if (pi.Value == null)
                {
                    writer.WriteNil();
                }
                else
                {
                    var type = pi.Value.GetType();
                    var method = SerializeMethodCache.GetSerializeMethod(type);
                    var stream = new MemoryStream(10);
                    method.Invoke(null, new object[] { stream, pi.Value, options, null});
                    writer.Write(stream.ToArray());
                }
            }
        }

        public T Deserialize(ref MessagePackReader reader, MessagePackSerializerOptions options)
        {
            var objectType = typeof(T);
            var tracker = (T) Activator.CreateInstance(objectType);

            var length = reader.ReadArrayHeader();
            List<string> list = new List<string>();
            for (var i = 0; i < length; i++)
            {
                list.Add(reader.ReadString());
            }

            for (var i = 0; i < length; i++)
            {
                var pi = objectType.GetProperty(list[i]);
                if (reader.TryReadNil())
                {
                    pi.SetValue(tracker, null, null);
                }
                else
                {
                    var data = reader.ReadBytes();
                    var method = SerializeMethodCache.GetDeserializeMethod(pi.PropertyType);
                    var value = method.Invoke(null, new object[] {data.Value, options, null});
                    pi.SetValue(tracker, value, null);
                }
            }

            return tracker;
        }
    }
}
