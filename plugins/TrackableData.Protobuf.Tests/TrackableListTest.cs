using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using ProtoBuf.Meta;
using Xunit;

namespace TrackableData.Protobuf.Tests
{
    public class TrackableListTest
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

        private RuntimeTypeModel CreateTypeModel()
        {
            var model = RuntimeTypeModel.Create();
            model.Add(typeof(TrackableListTracker<string>), false)
                 .SetSurrogate(typeof(TrackableListTrackerSurrogate<string>));
            return model;
        }

        [Fact]
        public void Test_TrackableList_Serialize_Work()
        {
            var list = CreateTestListWithTracker();

            var typeModel = CreateTypeModel();
            var list2 = (IEnumerable<string>)typeModel.DeepClone(list);

            Assert.Equal(new[] { "One", "Two", "Three" }, list2);
        }

        [Fact]
        public void Test_TrackableListTracker_Serialize_Work()
        {
            var list = CreateTestListWithTracker();
            list[0] = "OneModified";
            list.RemoveAt(1);
            list.Insert(1, "TwoInserted");

            var typeModel = CreateTypeModel();
            var tracker2 = typeModel.DeepClone((TrackableListTracker<string>)list.Tracker);

            var list2 = CreateTestList();
            tracker2.ApplyTo(list2);

            ArraySegment<byte> segment;
            using (var stream = new MemoryStream())
            {
                typeModel.Serialize(stream, list.Tracker);
                segment = new ArraySegment<byte>(stream.GetBuffer(), 0, (int)stream.Length);
            }

            using (var stream = new MemoryStream())
            {
                stream.Write(segment);
                stream.Seek(0, SeekOrigin.Begin);
                var type = tracker2.GetType();
                var go = typeModel.Deserialize(stream, (object)tracker2, type);
            }

            Assert.Equal(new[] { "OneModified", "TwoInserted", "Three" }, list2);
        }
    }
}
