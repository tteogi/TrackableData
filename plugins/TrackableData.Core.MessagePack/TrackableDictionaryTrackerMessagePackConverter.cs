using MessagePack;
using MessagePack.Formatters;

namespace TrackableData.MessagePack
{
    public class TrackableDictionaryTrackerInterfaceMessagePackConverter<TKey, TValue> : IMessagePackFormatter<IDictionaryTracker<TKey, TValue>>
    {
        public void Serialize(ref MessagePackWriter writer, IDictionaryTracker<TKey, TValue> value, MessagePackSerializerOptions options)
        {
            TrackableDictionaryTrackerMessagePackConverter<TKey, TValue>.Serialize(ref writer, (TrackableDictionaryTracker<TKey, TValue>)value, options);
        }

        public IDictionaryTracker<TKey, TValue> Deserialize(ref MessagePackReader reader, MessagePackSerializerOptions options)
        {
            return TrackableDictionaryTrackerMessagePackConverter<TKey, TValue>.Deserialize(ref reader, options);
        }
    }

    public class TrackableDictionaryTrackerClassMessagePackConverter<TKey, TValue> : IMessagePackFormatter<TrackableDictionaryTracker<TKey, TValue>>
    {
        public void Serialize(ref MessagePackWriter writer, TrackableDictionaryTracker<TKey, TValue> value, MessagePackSerializerOptions options)
        {
            TrackableDictionaryTrackerMessagePackConverter<TKey, TValue>.Serialize(ref writer, value, options);
        }

        public TrackableDictionaryTracker<TKey, TValue> Deserialize(ref MessagePackReader reader, MessagePackSerializerOptions options)
        {
            return TrackableDictionaryTrackerMessagePackConverter<TKey, TValue>.Deserialize(ref reader, options);
        }
    }

    public class TrackableDictionaryTrackerMessagePackConverter<TKey, TValue>
    {
        [MessagePackObject()]
        public class Changed
        {
            public Changed(int operation, TKey index, TValue value)
            {
                Operation = operation;
                Index = index;
                Value = value;
            }

            [Key(0)]
            public int Operation { get; set; }

            [Key(1)]
            public TKey Index { get; set; }

            [Key(2)]
            public TValue Value { get; set; }
        }

        public static void Serialize(ref MessagePackWriter writer, TrackableDictionaryTracker<TKey, TValue> value,
            MessagePackSerializerOptions options)
        {
            var tracker = value;
            writer.WriteArrayHeader(tracker.ChangeMap.Count);
            foreach (var item in tracker.ChangeMap)
            {
                switch (item.Value.Operation)
                {
                    case TrackableDictionaryOperation.Add:
                        MessagePackSerializer.Serialize(ref writer,
                            new Changed((int) item.Value.Operation, item.Key, item.Value.NewValue), options);
                        break;

                    case TrackableDictionaryOperation.Remove:
                        MessagePackSerializer.Serialize(ref writer,
                            new Changed((int) item.Value.Operation, item.Key, default(TValue)), options);
                        break;

                    case TrackableDictionaryOperation.Modify:
                        MessagePackSerializer.Serialize(ref writer,
                            new Changed((int) item.Value.Operation, item.Key, item.Value.NewValue), options);
                        break;
                }
            }
        }

        public static TrackableDictionaryTracker<TKey, TValue> Deserialize(ref MessagePackReader reader,
            MessagePackSerializerOptions options)
        {
            var count = reader.ReadArrayHeader();
            TrackableDictionaryTracker<TKey, TValue> tracker = new TrackableDictionaryTracker<TKey, TValue>();
            for (var i = 0; i < count; i++)
            {
                var changed = MessagePackSerializer.Deserialize<Changed>(ref reader, options);
                switch (changed.Operation)
                {
                    case (int) TrackableDictionaryOperation.Add:
                        tracker.TrackAdd(changed.Index, changed.Value);
                        break;

                    case (int) TrackableDictionaryOperation.Remove:
                        tracker.TrackRemove(changed.Index, default(TValue));
                        break;
                    case (int) TrackableDictionaryOperation.Modify:
                        tracker.TrackModify(changed.Index, default(TValue), changed.Value);
                        break;
                }
            }

            return tracker;
        }
    }
}
