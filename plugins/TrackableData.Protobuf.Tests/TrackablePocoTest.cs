using System;
using System.IO;
using ProtoBuf.Meta;
using Xunit;

namespace TrackableData.Protobuf.Tests
{
    public class TrackablePocoTest
    {
        private TrackablePerson CreateTestPerson()
        {
            return new TrackablePerson
            {
                Name = "Alice",
                Age = 20,
            };
        }

        private TrackablePerson CreateTestPersonWithTracker()
        {
            var person = CreateTestPerson();
            person.SetDefaultTrackerDeep();
            return person;
        }

        private RuntimeTypeModel CreateTypeModel()
        {
            var model = RuntimeTypeModel.Create();
            model.Add(typeof(TrackablePocoTracker<IPerson>), false)
                 .SetSurrogate(typeof(TrackablePersonTrackerSurrogate));
            return model;
        }

        [Fact]
        public void Test_TrackablePoco_Serialize_Work()
        {
            var person = CreateTestPersonWithTracker();
            var typeModel = CreateTypeModel();
            var person2 = (IPerson)typeModel.DeepClone(person);

            Assert.Equal(person.Name, person2.Name);
            Assert.Equal(person.Age, person2.Age);
        }

        [Fact]
        public void Test_TrackablePocoTracker_Serialize_Work()
        {
            var person = CreateTestPersonWithTracker();
            person.Name = "Bob";
            person.Age = 30;

            var typeModel = CreateTypeModel();
            var tracker2 = typeModel.DeepClone((TrackablePocoTracker<IPerson>)person.Tracker);

            var person2 = CreateTestPerson();
            tracker2.ApplyTo(person2);

            ArraySegment<byte> segment;
            using (var stream = new MemoryStream())
            {
                typeModel.Serialize(stream, person.Tracker);
                segment = new ArraySegment<byte>(stream.GetBuffer(), 0, (int)stream.Length);
            }

            using (var stream = new MemoryStream())
            {
                stream.Write(segment);
                stream.Seek(0, SeekOrigin.Begin);
                typeModel.Deserialize(stream, (object)tracker2, tracker2.GetType());
            }

            Assert.Equal(person.Name, person2.Name);
            Assert.Equal(person.Age, person2.Age);
        }
    }
}
