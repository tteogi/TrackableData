using MessagePack;
using MessagePack.Formatters;
using Newtonsoft.Json;
using TrackableData.MessagePack;
using Xunit;

namespace TrackableData.Json.Tests
{
    public class ListTrackerTest
    {
        private TrackableList<string> CreateTestList()
        {
            return new TrackableList<string>()
            {
                "One",
                "Two",
                "Three"
            };
        }

        private TrackableList<string> CreateTestListWithTracker()
        {
            var list = CreateTestList();
            list.SetDefaultTrackerDeep();
            return list;
        }

        [Fact]
        public void Test_List_Serialize_Work()
        {
            var list = CreateTestListWithTracker();
            var data = MessagePackSerializer.Serialize(list, TestResolver.GetMessagePackOption());
            var list2 = MessagePackSerializer.Deserialize<TrackableList<string>>(data, TestResolver.GetMessagePackOption());
            Assert.Equal(list.Count, list2.Count);
        }

        [Fact]
        public void Test_ListTracker_Serialize_Work()
        {
            var list = CreateTestListWithTracker();
            list[0] = "OneModified";
            list.RemoveAt(1);
            list.Insert(1, "TwoInserted");
            list.Insert(0, "Zero");
            list.RemoveAt(0);
            list.Insert(0, "ZeroAgain");
            list.Insert(4, "Four");
            list.RemoveAt(4);
            list.Insert(4, "FourAgain");

            var data = MessagePackSerializer.Serialize(list.Tracker, TestResolver.GetMessagePackOption());
            var tracker2= MessagePackSerializer.Deserialize<TrackableListTracker<string>>(data, TestResolver.GetMessagePackOption());

            var list2 = CreateTestList();
            tracker2.ApplyTo(list2);

            Assert.Equal(new[] {"ZeroAgain", "OneModified", "TwoInserted", "Three", "FourAgain"},
                list2);
        }
    }
}
