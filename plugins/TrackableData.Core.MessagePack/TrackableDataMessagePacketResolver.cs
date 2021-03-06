﻿using MessagePack;
using MessagePack.Formatters;
using MessagePack.Resolvers;
#if !NET35
using System;

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
                var type = typeof(T);
                var arguments = type.GetGenericArguments();
                if (typeof(IContainerTracker).IsAssignableFrom(type))
                {
                    var trackerType = typeof(TrackableContainerTrackerMessagePackFormatter<>)
                        .MakeGenericType(type);
                    formatter = (IMessagePackFormatter<T>) Activator.CreateInstance(trackerType);
                }
                else if (arguments.Length == 1)
                {
                    SetGenericFormatter(arguments, type);
                }
                else if (arguments.Length == 2)
                {
                    if (typeof(IDictionaryTracker<,>).MakeGenericType(arguments) == type)
                    {
                        var trackerType = typeof(TrackableDictionaryTrackerInterfaceMessagePackFormatter<,>)
                            .MakeGenericType(arguments);
                        formatter = (IMessagePackFormatter<T>) Activator.CreateInstance(trackerType);
                    }
                    else if (typeof(TrackableDictionaryTracker<,>).MakeGenericType(arguments) == type)
                    {
                        var trackerType = typeof(TrackableDictionaryTrackerClassMessagePackFormatter<,>)
                            .MakeGenericType(arguments);
                        formatter = (IMessagePackFormatter<T>) Activator.CreateInstance(trackerType);
                    }
                    else if (typeof(TrackableDictionary<,>).MakeGenericType(arguments) == type)
                    {
                        var trackerType = typeof(TrackableDictionaryMessagePackFormatter<,>)
                            .MakeGenericType(arguments);
                        formatter = (IMessagePackFormatter<T>) Activator.CreateInstance(trackerType);
                    }
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
                else if (typeof(TrackableSet<>).MakeGenericType(arguments) == type)
                {
                    var trackerType = typeof(TrackableSetMessagePackFormatter<>)
                        .MakeGenericType(arguments);
                    formatter = (IMessagePackFormatter<T>) Activator.CreateInstance(trackerType);
                }
                else if (typeof(TrackableList<>).MakeGenericType(arguments) == type)
                {
                    var trackerType = typeof(TrackableListMessagePackFormatter<>)
                        .MakeGenericType(arguments);
                    formatter = (IMessagePackFormatter<T>) Activator.CreateInstance(trackerType);
                }
            }
        }
    }
}
#endif
