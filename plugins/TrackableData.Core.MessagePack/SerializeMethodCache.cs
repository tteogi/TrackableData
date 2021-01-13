using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using MessagePack;

namespace TrackableData.MessagePack
{
	public class SerializeMethodCache
	{
		static MethodInfo SerializeMethodInfo;
		static MethodInfo DeserializeMethodInfo;
		private static Dictionary<Type, MethodInfo> _serializeMethodInfos = new Dictionary<Type, MethodInfo>();
		private static Dictionary<Type, MethodInfo> _deserializeMethodInfos = new Dictionary<Type, MethodInfo>();

		static SerializeMethodCache()
		{
			var type = typeof(MessagePackSerializer);
			var methods = type.GetMethods(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy);
			foreach (var methodInfo in methods)
			{
				var parameters = methodInfo.GetParameters();
				if (methodInfo.Name == "Deserialize" && parameters.Length == 3 &&
				    parameters[0].Name == "byteSequence" &&
				    parameters[1].ParameterType == typeof(MessagePackSerializerOptions))
				{
					DeserializeMethodInfo = methodInfo;
				}
				else if (methodInfo.Name == "Serialize" && parameters.Length == 4 &&
				         parameters[0].ParameterType == typeof(Stream) &&
				         parameters[1].ParameterType.IsGenericParameter &&
				         parameters[2].ParameterType == typeof(MessagePackSerializerOptions))
				{
					SerializeMethodInfo  = methodInfo;
				}
			}
		}

		public static MethodInfo GetSerializeMethod(Type type)
		{
			if (!_serializeMethodInfos.TryGetValue(type, out var method))
			{
				method = SerializeMethodInfo.MakeGenericMethod(type);
				_serializeMethodInfos[type] = method;
			}

			return method;
		}

		public static MethodInfo GetDeserializeMethod(Type type)
		{
			if (!_deserializeMethodInfos.TryGetValue(type, out var method))
			{
				method = DeserializeMethodInfo.MakeGenericMethod(type);
				_deserializeMethodInfos[type] = method;
			}

			return method;
		}
	}
}
