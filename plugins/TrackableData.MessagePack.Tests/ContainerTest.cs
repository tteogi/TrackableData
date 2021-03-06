﻿using System.Linq;
using MessagePack;
using Newtonsoft.Json;
using TrackableData.MessagePack;
using Xunit;

namespace TrackableData.Json.Tests
{
    public class ContainerTest
    {
        private TrackableDataContainer CreateTestContainer()
        {
            return new TrackableDataContainer
            {
                Person = new TrackablePerson
                {
                    Name = "Alice",
                    Age = 20,
                },
                Dictionary = new TrackableDictionary<int, string>
                {
                    { 1, "One" },
                    { 2, "Two" },
                    { 3, "Three" }
                },
                List = new TrackableList<string>
                {
                    "One",
                    "Two",
                    "Three"
                },
                Set = new TrackableSet<int>()
                {
                    1, 2, 3
                }
            };
        }

        private TrackableDataContainer CreateTestContainerWithTracker()
        {
            var container = CreateTestContainer();
            container.Tracker = new TrackableDataContainerTracker();
            return container;
        }


        [Fact]
        public void Test_Container_Serialize_Work()
        {
            var c = CreateTestContainerWithTracker();
            var data = MessagePackSerializer.Serialize(c, TestResolver.GetMessagePackOption());
            var c2 = MessagePackSerializer.Deserialize<TrackableDataContainer>(data, TestResolver.GetMessagePackOption());
            Assert.Equal(c.Dictionary.Count, c2.Dictionary.Count);
            Assert.Equal(c.List.Count, c2.List.Count);
        }

        [Fact]
        public void Test_ContainerTracker_Serialize_Work()
        {
            var c = CreateTestContainerWithTracker();

            // Act

            c.Person.Name = "Bob";
            c.Person.Age = 30;

            c.Dictionary[1] = "OneModified";
            c.Dictionary.Remove(2);
            c.Dictionary[4] = "FourAdded";

            c.List[0] = "OneModified";
            c.List.RemoveAt(1);
            c.List.Insert(1, "TwoInserted");

            c.Set.Remove(1);
            c.Set.Remove(2);
            c.Set.Add(4);
            c.Set.Add(5);

            // Assert

            var data = MessagePackSerializer.Serialize(c.Tracker, TestResolver.GetMessagePackOption());
            var tracker2 = MessagePackSerializer.Deserialize<TrackableDataContainerTracker>(data, TestResolver.GetMessagePackOption());

            var c2 = CreateTestContainer();
            tracker2.ApplyTo(c2);

            Assert.Equal(c.Person.Name, c2.Person.Name);
            Assert.Equal(c.Person.Age, c2.Person.Age);
            Assert.Equal(c.Dictionary.OrderBy(x => x.Key), c2.Dictionary.OrderBy(x => x.Key));
            Assert.Equal(c.List, c2.List);
            Assert.Equal(c.Set.OrderBy(x => x), c2.Set.OrderBy(x => x));
        }
    }
}
