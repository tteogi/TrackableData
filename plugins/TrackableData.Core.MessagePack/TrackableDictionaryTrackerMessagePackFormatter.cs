using MessagePack;
using MessagePack.Formatters;

namespace TrackableData.MessagePack
{
    public class TrackableDictionaryTrackerInterfaceMessagePackFormatter<TKey, TValue> : IMessagePackFormatter<IDictionaryTracker<TKey, TValue>>
    {
        public void Serialize(ref MessagePackWriter writer, IDictionaryTracker<TKey, TValue> value, MessagePackSerializerOptions options)
        {
            TrackableDictionaryTrackerMessagePackFormatter<TKey, TValue>.Serialize(ref writer, (TrackableDictionaryTracker<TKey, TValue>)value, options);
        }

        public IDictionaryTracker<TKey, TValue> Deserialize(ref MessagePackReader reader, MessagePackSerializerOptions options)
        {
            return TrackableDictionaryTrackerMessagePackFormatter<TKey, TValue>.Deserialize(ref reader, options);
        }
    }

    public class TrackableDictionaryTrackerClassMessagePackFormatter<TKey, TValue> : IMessagePackFormatter<TrackableDictionaryTracker<TKey, TValue>>
    {
        public void Serialize(ref MessagePackWriter writer, TrackableDictionaryTracker<TKey, TValue> value, MessagePackSerializerOptions options)
        {
            TrackableDictionaryTrackerMessagePackFormatter<TKey, TValue>.Serialize(ref writer, value, options);
        }

        public TrackableDictionaryTracker<TKey, TValue> Deserialize(ref MessagePackReader reader, MessagePackSerializerOptions options)
        {
            return TrackableDictionaryTrackerMessagePackFormatter<TKey, TValue>.Deserialize(ref reader, options);
        }
    }

    public class TrackableDictionaryTrackerMessagePackFormatter<TKey, TValue>
    {
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
                        writer.Write((int) item.Value.Operation);
                        MessagePackSerializer.Serialize(ref writer, item.Key, options);
                        MessagePackSerializer.Serialize(ref writer, item.Value.NewValue, options);
                        break;

                    case TrackableDictionaryOperation.Remove:
                        writer.Write((int) item.Value.Operation);
                        MessagePackSerializer.Serialize(ref writer, item.Key, options);
                        MessagePackSerializer.Serialize(ref writer, default(TValue), options);
                        break;

                    case TrackableDictionaryOperation.Modify:
                        writer.Write((int) item.Value.Operation);
                        MessagePackSerializer.Serialize(ref writer, item.Key, options);
                        MessagePackSerializer.Serialize(ref writer, item.Value.NewValue, options);
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
                var operation = reader.ReadInt32();
                var key = MessagePackSerializer.Deserialize<TKey>(ref reader, options);
                var value = MessagePackSerializer.Deserialize<TValue>(ref reader, options);
                switch (operation)
                {
                    case (int) TrackableDictionaryOperation.Add:
                        tracker.TrackAdd(key, value);
                        break;

                    case (int) TrackableDictionaryOperation.Remove:
                        tracker.TrackRemove(key, value);
                        break;
                    case (int) TrackableDictionaryOperation.Modify:
                        tracker.TrackModify(key, default(TValue), value);
                        break;
                }
            }

            return tracker;
        }
    }
}
