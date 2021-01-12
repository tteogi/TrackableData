using MessagePack;
using MessagePack.Resolvers;
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

        [Fact]
        public void Test_Poco_Serialize_Work()
        {
            var person = CreateTestPersonWithTracker();
            var serialize = MessagePackSerializer.Serialize(person, TestResolver.GetMessagePackOption());
            var person2 = MessagePackSerializer.Deserialize<TrackablePerson>(serialize, TestResolver.GetMessagePackOption());
        }

        [Fact]
        public void Test_PocoTracker_Serialize_Work()
        {
            var person = CreateTestPersonWithTracker();
            person.Name = "Bob";
            person.Age = 30;

            var serialize = MessagePackSerializer.Serialize(person.Tracker, TestResolver.GetMessagePackOption());
            var tracker2 =
                MessagePackSerializer.Deserialize<TrackablePocoTracker<IPerson>>(serialize, TestResolver.GetMessagePackOption());

            var person2 = CreateTestPerson();
            tracker2.ApplyTo(person2);

            Assert.Equal(person.Name, person2.Name);
            Assert.Equal(person.Age, person2.Age);
        }
    }
}
