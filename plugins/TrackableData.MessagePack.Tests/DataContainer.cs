using MessagePack;

namespace TrackableData.Json.Tests
{
    [Union(0, typeof(TrackableDataContainer))]
    public interface IDataContainer : ITrackableContainer<IDataContainer>
    {
        [Key(0)]
        TrackablePerson Person { get; set; }
        [Key(1)]
        TrackableDictionary<int, string> Dictionary { get; set; }
        [Key(2)]
        TrackableList<string> List { get; set; }
        [Key(3)]
        TrackableSet<int> Set { get; set; }
    }
}
