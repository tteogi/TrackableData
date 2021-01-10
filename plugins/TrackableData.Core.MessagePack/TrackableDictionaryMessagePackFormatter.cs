using MessagePack;
using MessagePack.Formatters;

namespace TrackableData.MessagePack
{
	public class TrackableDictionaryMessagePackFormatter<TKey, TValue>  : DictionaryFormatterBase<TKey, TValue, TrackableDictionary<TKey, TValue>>
	{
		protected override TrackableDictionary<TKey, TValue> Create(int count, MessagePackSerializerOptions options)
		{
			return new TrackableDictionary<TKey, TValue>();
		}

		protected override void Add(TrackableDictionary<TKey, TValue> collection, int index, TKey key, TValue value, MessagePackSerializerOptions options)
		{
			collection.Add(key, value);
		}
	}
	
	// public class TrackableDictionaryMessagePackFormatter<T1, T2> : IMessagePackFormatter<TrackableDictionary<T1, T2>>
	// {
	// 	public void Serialize(ref MessagePackWriter writer, TrackableDictionary<T1, T2> value, MessagePackSerializerOptions options)
	// 	{
	// 		throw new System.NotImplementedException();
	// 	}
	//
	// 	public TrackableDictionary<T1, T2> Deserialize(ref MessagePackReader reader, MessagePackSerializerOptions options)
	// 	{
	// 		throw new System.NotImplementedException();
	// 	}
	// }
}