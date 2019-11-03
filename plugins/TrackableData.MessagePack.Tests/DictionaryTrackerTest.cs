using System.Collections.Generic;
using System.Linq;
using MessagePack;
using Newtonsoft.Json;
using TrackableData.MessagePack;
using Xunit;

namespace TrackableData.Json.Tests
{
    public class DictionaryTrackerTest
    {
        private TrackableDictionary<int, string> CreateTestDictionary()
        {
            return new TrackableDictionary<int, string>()
            {
                { 1, "One" },
                { 2, "Two" },
                { 3, "Three" }
            };
        }

        private TrackableDictionary<int, string> CreateTestDictionaryWithTracker()
        {
            var dict = CreateTestDictionary();
            dict.SetDefaultTrackerDeep();
            return dict;
        }

        private MessagePackSerializerOptions GetMessagePackOption()
        {
            return MessagePackSerializerOptions.Standard.WithResolver(new TrackableDataMessagePacketResolver());
        }

        [Fact]
        public void Test_Dictionary_Serialize_Work()
        {
            var dict = CreateTestDictionaryWithTracker();
            var data = MessagePackSerializer.Serialize(dict, GetMessagePackOption());
            var dict2 = MessagePackSerializer.Deserialize<TrackableDictionary<int, string>>(data, GetMessagePackOption());
            Assert.Equal(dict.Count, dict2.Count);
        }

        [Fact]
        public void Test_DictionaryTracker_Serialize_Work()
        {
            var dict = CreateTestDictionaryWithTracker();
            dict[1] = "OneModified";
            dict.Remove(2);
            dict[4] = "FourAdded";

            var data = MessagePackSerializer.Serialize(dict.Tracker, GetMessagePackOption());
            var tracker2 = MessagePackSerializer.Deserialize<TrackableDictionaryTracker<int, string>>(data, GetMessagePackOption());

            var dict2 = CreateTestDictionary();
            tracker2.ApplyTo(dict2);

            Assert.Equal(
                new[]
                {
                    new KeyValuePair<int, string>(1, "OneModified"),
                    new KeyValuePair<int, string>(3, "Three"),
                    new KeyValuePair<int, string>(4, "FourAdded")
                },
                dict2.OrderBy(kv => kv.Key));
        }
    }
}
