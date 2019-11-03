using System.Collections.Generic;
using MessagePack;
using MessagePack.Formatters;
using MessagePack.Resolvers;
#if !NET35
using System;
using System.Collections.Concurrent;
using System.Reflection;

namespace TrackableData.MessagePack
{
    public class TrackableDataMessagePacketResolver : IFormatterResolver
    {
        public IMessagePackFormatter<T> GetFormatter<T>()
        {
            return GMsgPacketFormatterCache<T>.formatter;
        }

        static class GMsgPacketFormatterCache<T>
        {
            public static IMessagePackFormatter<T> formatter;

            // generic's static constructor should be minimized for reduce type generation size!
            // use outer helper method.
            static GMsgPacketFormatterCache()
            {
                formatter = StandardResolver.Instance.GetFormatter<T>();
                if (formatter != null)
                {
                    return;
                }

                var type = typeof(T);
                var arguments = type.GetGenericArguments();
                if (arguments.Length == 0)
                {
                    if (typeof(IContainerTracker).IsAssignableFrom(type))
                    {
                        var trackerType = typeof(TrackableContainerTrackerMessagePackFormatter<>)
                            .MakeGenericType(type);
                        formatter = (IMessagePackFormatter<T>) Activator.CreateInstance(trackerType);
                    }
                }
                else if (arguments.Length == 1)
                {
                    SetGenericFormatter(arguments, type);
                }
                else if (typeof(IDictionaryTracker<,>).MakeGenericType(arguments) == type)
                {
                    var trackerType = typeof(TrackableDictionaryTrackerInterfaceMessagePackConverter<,>)
                        .MakeGenericType(arguments);
                    formatter = (IMessagePackFormatter<T>) Activator.CreateInstance(trackerType);
                }
                else if (typeof(TrackableDictionaryTracker<,>).MakeGenericType(arguments) == type)
                {
                    var trackerType = typeof(TrackableDictionaryTrackerClassMessagePackConverter<,>)
                        .MakeGenericType(arguments);
                    formatter = (IMessagePackFormatter<T>) Activator.CreateInstance(trackerType);
                }
            }

            private static void SetGenericFormatter(Type[] arguments, Type type)
            {
                if (typeof(IPocoTracker<>).MakeGenericType(arguments) == type)
                {
                    var trackerType = typeof(TrackablePocoTrackerInterfaceMessagePackFormatter<>)
                        .MakeGenericType(arguments);
                    formatter = (IMessagePackFormatter<T>) Activator.CreateInstance(trackerType);
                }
                else if (typeof(TrackablePocoTracker<>).MakeGenericType(arguments) == type)
                {
                    var trackerType = typeof(TrackablePocoTrackerClassMessagePackFormatter<>)
                        .MakeGenericType(arguments);
                    formatter = (IMessagePackFormatter<T>) Activator.CreateInstance(trackerType);
                }
                else if (typeof(IListTracker<>).MakeGenericType(arguments) == type)
                {
                    var trackerType = typeof(TrackableListTrackerInterfaceMessagePackFormatter<>)
                        .MakeGenericType(arguments);
                    formatter = (IMessagePackFormatter<T>) Activator.CreateInstance(trackerType);
                }
                else if (typeof(TrackableListTracker<>).MakeGenericType(arguments) == type)
                {
                    var trackerType = typeof(TrackableListTrackerClassMessagePackFormatter<>)
                        .MakeGenericType(arguments);
                    formatter = (IMessagePackFormatter<T>) Activator.CreateInstance(trackerType);
                }
                else if (typeof(ISetTracker<>).MakeGenericType(arguments) == type)
                {
                    var trackerType = typeof(TrackableSetTrackerInterfaceMessagePackFormatter<>)
                        .MakeGenericType(arguments);
                    formatter = (IMessagePackFormatter<T>) Activator.CreateInstance(trackerType);
                }
                else if (typeof(TrackableSetTracker<>).MakeGenericType(arguments) == type)
                {
                    var trackerType = typeof(TrackableSetTrackerClassMessagePackFormatter<>)
                        .MakeGenericType(arguments);
                    formatter = (IMessagePackFormatter<T>) Activator.CreateInstance(trackerType);
                }
            }
        }
    }
}

#endif
