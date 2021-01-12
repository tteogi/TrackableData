using System.Collections.Generic;
using MessagePack;
using Newtonsoft.Json;
using TrackableData.MessagePack;
using Xunit;

namespace TrackableData.Json.Tests
{
    public class SetTrackerTest
    {
        private TrackableSet<int> CreateTestSet()
        {
            return new TrackableSet<int>() { 1, 2, 3 };
        }

        private TrackableSet<int> CreateTestSetWithTracker()
        {
            var set = CreateTestSet();
            set.SetDefaultTrackerDeep();
            return set;
        }

        [Fact]
        public void Test_Set_Serialize_Work()
        {
            var set = CreateTestSetWithTracker();
            var serialize = MessagePackSerializer.Serialize(set, TestResolver.GetMessagePackOption());
            var set2 = MessagePackSerializer.Deserialize<TrackableSet<int>>(serialize, TestResolver.GetMessagePackOption());

            Assert.Equal(set.Count, set2.Count);
        }

        [Fact]
        public void Test_SetTracker_Serialize_Work()
        {
            var set = CreateTestSetWithTracker();
            set.Remove(1);
            set.Remove(2);
            set.Add(4);
            set.Add(5);

            var serialize = MessagePackSerializer.Serialize(set.Tracker, TestResolver.GetMessagePackOption());
            var tracker2 = MessagePackSerializer.Deserialize<TrackableSetTracker<int>>(serialize, TestResolver.GetMessagePackOption());

            var set2 = CreateTestSet();
            tracker2.ApplyTo(set2);

            Assert.Equal(new HashSet<int> { 5, 4, 3 },
                         set2);
        }
    }
}
