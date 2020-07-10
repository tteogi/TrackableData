using MessagePack;
using MessagePack.Formatters;

namespace TrackableData.MessagePack
{
    public class TrackableListTrackerClassMessagePackFormatter<T> : IMessagePackFormatter<TrackableListTracker<T>>
    {
        public void Serialize(ref MessagePackWriter writer, TrackableListTracker<T> value, MessagePackSerializerOptions options)
        {
            TrackableListTrackerMessagePackFormatter<T>.Serialize(ref writer, value, options);
        }

        public TrackableListTracker<T> Deserialize(ref MessagePackReader reader, MessagePackSerializerOptions options)
        {
            return (TrackableListTracker<T>)TrackableListTrackerMessagePackFormatter<T>.Deserialize(ref reader, options);
        }
    }

    public class TrackableListTrackerInterfaceMessagePackFormatter<T> : IMessagePackFormatter<IListTracker<T>>
    {
        public void Serialize(ref MessagePackWriter writer, IListTracker<T> value, MessagePackSerializerOptions options)
        {
            TrackableListTrackerMessagePackFormatter<T>.Serialize(ref writer, value, options);
        }

        public IListTracker<T> Deserialize(ref MessagePackReader reader, MessagePackSerializerOptions options)
        {
            return TrackableListTrackerMessagePackFormatter<T>.Deserialize(ref reader, options);
        }
    }

    public class TrackableListTrackerMessagePackFormatter<T>
    {
        [MessagePackObject()]
        public class Changed
        {
            public Changed(int operation, int index, T value)
            {
                Operation = operation;
                Index = index;
                Value = value;
            }

            [Key(0)]
            public int Operation { get; set; }
            [Key(1)]
            public int Index { get; set; }
            [Key(2)]
            public T Value { get; set; }
        }

        public static void Serialize(ref MessagePackWriter writer, IListTracker<T> value, MessagePackSerializerOptions options)
        {
            var tracker = (TrackableListTracker<T>)value;
            writer.WriteArrayHeader(tracker.ChangeList.Count);
            foreach (var item in tracker.ChangeList)
            {
                writer.Write((int) item.Operation);
                writer.Write(item.Index);
                MessagePackSerializer.Serialize(ref writer, item.NewValue, options);
            }
        }

        public static IListTracker<T> Deserialize(ref MessagePackReader reader, MessagePackSerializerOptions options)
        {
            var tracker = new TrackableListTracker<T>();
            var length = reader.ReadArrayHeader();
            for (int i = 0; i < length; i++)
            {
                var operation = reader.ReadInt32();
                var index = reader.ReadInt32();
                var value = MessagePackSerializer.Deserialize<T>(ref reader, options);
                switch ((TrackableListOperation) operation)
                {
                    case TrackableListOperation.Insert:
                        tracker.TrackInsert(index, value);
                        break;
                    case TrackableListOperation.Remove:
                        tracker.TrackRemove(index, value);
                        break;
                    case TrackableListOperation.Modify:
                        tracker.TrackModify(index, default(T), value);
                        break;
                    case TrackableListOperation.PushFront:
                        tracker.TrackPushFront(value);
                        break;
                    case TrackableListOperation.PushBack:
                        tracker.TrackPushBack(value);
                        break;
                    case TrackableListOperation.PopFront:
                        tracker.TrackPopFront(default(T));
                        break;
                    case TrackableListOperation.PopBack:
                        tracker.TrackPopBack(default(T));
                        break;
                }
            }
            return tracker;
        }
    }
}
