﻿using System;
using System.Collections.Generic;
using System.IO;
using MessagePack;
using MessagePack.Formatters;

namespace TrackableData.MessagePack
{
	public class TrackablePocoTrackerInterfaceMessagePackFormatter<T> : IMessagePackFormatter<IPocoTracker<T>>
	{
		public void Serialize(ref MessagePackWriter writer, IPocoTracker<T> value, MessagePackSerializerOptions options)
		{
			TrackablePocoTrackerMessagePackFormatter<T>.Serialize(ref writer, value, options);
		}

		public IPocoTracker<T> Deserialize(ref MessagePackReader reader, MessagePackSerializerOptions options)
		{
			return TrackablePocoTrackerMessagePackFormatter<T>.Deserialize(ref reader, options);
		}
	}

	public class TrackablePocoTrackerClassMessagePackFormatter<T> : IMessagePackFormatter<TrackablePocoTracker<T>>
	{
		public void Serialize(ref MessagePackWriter writer, TrackablePocoTracker<T> value,
			MessagePackSerializerOptions options)
		{
			TrackablePocoTrackerMessagePackFormatter<T>.Serialize(ref writer, value, options);
		}

		public TrackablePocoTracker<T> Deserialize(ref MessagePackReader reader, MessagePackSerializerOptions options)
		{
			return (TrackablePocoTracker<T>) TrackablePocoTrackerMessagePackFormatter<T>.Deserialize(ref reader,
				options);
		}
	}

	public class TrackablePocoTrackerMessagePackFormatter<T>
	{
		public static void Serialize(ref MessagePackWriter writer, IPocoTracker<T> value,
			MessagePackSerializerOptions options)
		{
			var tracker = (TrackablePocoTracker<T>) value;
			writer.WriteArrayHeader(tracker.ChangeMap.Count);
			foreach (var item in tracker.ChangeMap)
			{
				writer.Write(item.Key.Name);
			}

			foreach (var item in tracker.ChangeMap)
			{
				if (item.Value.NewValue == null)
				{
					writer.WriteNil();
				}
				else
				{
					var method = SerializeMethodCache.GetSerializeMethod(item.Value.NewValue.GetType());
					var stream = new MemoryStream(10);
					method.Invoke(null, new object[] {stream, item.Value.NewValue, options, null});
					writer.Write(stream.ToArray());
				}
			}
		}

		public static IPocoTracker<T> Deserialize(ref MessagePackReader reader, MessagePackSerializerOptions options)
		{
			var tracker = new TrackablePocoTracker<T>();
			var length = reader.ReadArrayHeader();

			List<string> list = new List<string>();
			for (var i = 0; i < length; i++)
			{
				list.Add(reader.ReadString());
			}

			var objectType = typeof(T);
			for (var i = 0; i < length; i++)
			{
				var pi = objectType.GetProperty(list[i]);
				if (reader.TryReadNil())
				{
					tracker.TrackSet(pi, null, null);
				}
				else
				{
					var data = reader.ReadBytes();
					var method = SerializeMethodCache.GetDeserializeMethod(pi.PropertyType);
					var value = method.Invoke(null, new object[] {data.Value, options, null});
					tracker.TrackSet(pi, null, value);
				}
			}

			return tracker;
		}
	}
}
