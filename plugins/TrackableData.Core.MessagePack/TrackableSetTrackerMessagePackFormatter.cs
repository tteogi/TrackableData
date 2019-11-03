using MessagePack;
using MessagePack.Formatters;

namespace TrackableData.MessagePack
{
    public class TrackableSetTrackerInterfaceMessagePackFormatter<T> : IMessagePackFormatter<ISetTracker<T>>
    {
        public void Serialize(ref MessagePackWriter writer, ISetTracker<T> value, MessagePackSerializerOptions options)
        {
            TrackableSetTrackerMessagePackFormatter<T>.Serialize(ref writer, (TrackableSetTracker<T>)value, options);
        }

        public ISetTracker<T> Deserialize(ref MessagePackReader reader, MessagePackSerializerOptions options)
        {
            return TrackableSetTrackerMessagePackFormatter<T>.Deserialize(ref reader, options);
        }
    }

    public class TrackableSetTrackerClassMessagePackFormatter<T> : IMessagePackFormatter<TrackableSetTracker<T>>
    {
        public void Serialize(ref MessagePackWriter writer, TrackableSetTracker<T> value, MessagePackSerializerOptions options)
        {
            TrackableSetTrackerMessagePackFormatter<T>.Serialize(ref writer, value, options);
        }

        public TrackableSetTracker<T> Deserialize(ref MessagePackReader reader, MessagePackSerializerOptions options)
        {
            return TrackableSetTrackerMessagePackFormatter<T>.Deserialize(ref reader, options);
        }
    }

    public class TrackableSetTrackerMessagePackFormatter<T>
    {
        [MessagePackObject()]
        public class Changed
        {
            public Changed(bool add, T value)
            {
                Add = add;
                Value = value;
            }

            [Key(0)]
            public bool Add { get; set; }

            [Key(1)]
            public T Value { get; set; }
        }

//        public int Serialize(ref byte[] bytes, int offset, TrackableSetTracker<T> value, IFormatterResolver formatterResolver)
//        {
//            var tracker = value;
//            var startOffset = offset;
//            offset += MessagePackBinary.WriteArrayHeader(ref bytes, offset, tracker.ChangeMap.Count);
//            foreach (var item in tracker.ChangeMap)
//            {
//                var changed = new Changed(item.Value == TrackableSetOperation.Add, item.Key);
//                offset += formatterResolver.GetFormatterWithVerify<Changed>()
//                    .Serialize(ref bytes, offset, changed, formatterResolver);
//            }
//
//            return offset - startOffset;
//        }
//
//        public TrackableSetTracker<T> Deserialize(byte[] bytes, int offset, IFormatterResolver formatterResolver, out int readSize)
//        {
//            var tracker = new TrackableSetTracker<T>();
//            var startOffset = offset;
//            var length = MessagePackBinary.ReadArrayHeader(bytes, offset, out readSize);
//            offset += readSize;
//
//            for (int i = 0; i < length; i++)
//            {
//                var item = formatterResolver.GetFormatterWithVerify<Changed>()
//                    .Deserialize(bytes, offset, formatterResolver, out readSize);
//                offset += readSize;
//                if(item.Add)
//                    tracker.TrackAdd(item.Value);
//                else
//                    tracker.TrackRemove(item.Value);
//            }
//
//            readSize = offset - startOffset;
//            return tracker;
//        }

        public static void Serialize(ref MessagePackWriter writer, TrackableSetTracker<T> value,
            MessagePackSerializerOptions options)
        {
            var tracker = value;
            writer.WriteArrayHeader(tracker.ChangeMap.Count);
            foreach (var item in tracker.ChangeMap)
            {
                var changed = new Changed(item.Value == TrackableSetOperation.Add, item.Key);
                MessagePackSerializer.Serialize(ref writer, changed, options);
            }
        }

        public static TrackableSetTracker<T> Deserialize(ref MessagePackReader reader, MessagePackSerializerOptions options)
        {
            var tracker = new TrackableSetTracker<T>();
            var length = reader.ReadArrayHeader();
            for (int i = 0; i < length; i++)
            {
                var item = MessagePackSerializer.Deserialize<Changed>(ref reader, options);
                if (item.Add)
                    tracker.TrackAdd(item.Value);
                else
                    tracker.TrackRemove(item.Value);
            }

            return tracker;
        }
    }
}
