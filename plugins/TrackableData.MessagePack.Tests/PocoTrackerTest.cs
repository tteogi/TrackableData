using MessagePack;
using Newtonsoft.Json;
using TrackableData.MessagePack;
using Xunit;

namespace TrackableData.Json.Tests
{
    public class PocoTrackerTest
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

        private MessagePackSerializerOptions GetMessagePackOption()
        {
            return MessagePackSerializerOptions.Standard.WithResolver(new TrackableDataMessagePacketResolver());
        }

        [Fact]
        public void Test_Poco_Serialize_Work()
        {
            var person = CreateTestPersonWithTracker();
            var serialize = MessagePackSerializer.Serialize(person, GetMessagePackOption());
            var person2 = MessagePackSerializer.Deserialize<TrackablePerson>(serialize, GetMessagePackOption());
            Assert.Equal(person.Name, person2.Name);
        }

        [Fact]
        public void Test_PocoTracker_Serialize_Work()
        {
            var person = CreateTestPersonWithTracker();
            person.Name = "Bob";
            person.Age = 30;

            var serialize = MessagePackSerializer.Serialize(person.Tracker, GetMessagePackOption());
            var tracker2 = MessagePackSerializer.Deserialize<TrackablePocoTracker<IPerson>>(serialize, GetMessagePackOption());

            var person2 = CreateTestPerson();
            tracker2.ApplyTo(person2);

            Assert.Equal(person.Name, person2.Name);
            Assert.Equal(person.Age, person2.Age);
        }
    }
}
