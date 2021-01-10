using MessagePack;
using MessagePack.Formatters;

namespace TrackableData.MessagePack
{
	public class TrackableSetMessagePackFormatter<T> : IMessagePackFormatter<TrackableSet<T>>
	{
		public void Serialize(ref MessagePackWriter writer, TrackableSet<T> value, MessagePackSerializerOptions options)
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

				foreach (var item in value)
				{
					formatter.Serialize(ref writer, item, options);
				}

				writer.CancellationToken.ThrowIfCancellationRequested();
			}
		}

		public TrackableSet<T> Deserialize(ref MessagePackReader reader, MessagePackSerializerOptions options)
		{
			if (reader.TryReadNil())
			{
				return default;
			}
			else
			{
				IMessagePackFormatter<T> formatter = options.Resolver.GetFormatterWithVerify<T>();

				var len = reader.ReadArrayHeader();
				var list = new TrackableSet<T>();
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
}