using MessagePack;
using MessagePack.Formatters;

namespace TrackableData.MessagePack
{
	public sealed class TrackableListMessagePackFormatter<T> : IMessagePackFormatter<TrackableList<T>>
        {
            public void Serialize(ref MessagePackWriter writer, TrackableList<T> value, MessagePackSerializerOptions options)
            {
                if (value == null)
                {
                    writer.WriteNil();
                }
                else
                {
                    IMessagePackFormatter<T> formatter = options.Resolver.GetFormatterWithVerify<T>();

                    var c = value.Count;
                    writer.WriteArrayHeader(c);

                    for (int i = 0; i < c; i++)
                    {
                        writer.CancellationToken.ThrowIfCancellationRequested();
                        formatter.Serialize(ref writer, value[i], options);
                    }
                }
            }

            public TrackableList<T> Deserialize(ref MessagePackReader reader, MessagePackSerializerOptions options)
            {
                if (reader.TryReadNil())
                {
                    return default;
                }
                else
                {
                    IMessagePackFormatter<T> formatter = options.Resolver.GetFormatterWithVerify<T>();

                    var len = reader.ReadArrayHeader();
                    var list = new TrackableList<T>();
                    options.Security.DepthStep(ref reader);
                    try
                    {
                        for (int i = 0; i < len; i++)
                        {
                            reader.CancellationToken.ThrowIfCancellationRequested();
                            list.Add(formatter.Deserialize(ref reader, options));
                        }
                    }
                    finally
                    {
                        reader.Depth--;
                    }

                    return list;
                }
            }
        }

	public class TrackableListMessagePackFormatter
	{

	}
}
