﻿using System;
using System.Collections.Generic;
using System.IO;
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

        private TypeModel CreateTypeModel()
        {
            var model = TypeModel.Create();
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
            var tracker2 = (TrackableListTracker<string>)typeModel.DeepClone(list.Tracker);

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
                typeModel.Deserialize(stream, tracker2, tracker2.GetType());
            }

            Assert.Equal(new[] { "OneModified", "TwoInserted", "Three" }, list2);
        }
    }
}
