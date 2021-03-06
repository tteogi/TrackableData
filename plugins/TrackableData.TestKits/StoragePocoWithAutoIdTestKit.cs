﻿using System.Threading.Tasks;
using Xunit;

namespace TrackableData.TestKits
{
    public abstract class StoragePocoWithAutoIdTestKit<TTrackablePoco, TId>
        where TTrackablePoco : ITrackablePoco, new()
    {
        protected abstract Task CreateAsync(TTrackablePoco person);
        protected abstract Task<TId> DeleteAsync(TId id);
        protected abstract Task<TTrackablePoco> LoadAsync(TId id);
        protected abstract Task SaveAsync(TTrackablePoco person, TId id);

        [Fact]
        public async Task Test_CreateAndLoad()
        {
            dynamic person = new TTrackablePoco();
            person.Name = "Testor";
            person.Age = 10;
            await CreateAsync(person);

            var person2 = await LoadAsync(person.Id);
            Assert.NotEqual(default(TId), person2.Id);
            Assert.Equal(person.Name, person2.Name);
            Assert.Equal(person.Age, person2.Age);
        }

        [Fact]
        public async Task Test_Delete()
        {
            dynamic person = new TTrackablePoco();
            await CreateAsync(person);

            var count = await DeleteAsync(person.Id);
            var person2 = await LoadAsync(person.Id);

            Assert.Equal(1, count);
            Assert.Equal(null, person2);
        }

        [Fact]
        public async Task Test_Save()
        {
            dynamic person = new TTrackablePoco();
            person.Name = "Testor";
            person.Age = 10;
            await CreateAsync(person);

            ((ITrackable)person).SetDefaultTracker();
            person.Name = "Alice";
            person.Age = 11;
            await SaveAsync(person, person.Id);

            var person2 = await LoadAsync(person.Id);
            Assert.Equal(person.Id, person2.Id);
            Assert.Equal(person.Name, person2.Name);
            Assert.Equal(person.Age, person2.Age);
        }
    }
}
