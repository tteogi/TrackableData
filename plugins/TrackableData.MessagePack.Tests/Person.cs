using MessagePack;

namespace TrackableData.Json.Tests
{
    [Union(0, typeof(TrackablePerson))]
    public interface IPerson : ITrackablePoco<IPerson>
    {
        [Key(0)]
        string Name { get; set; }
        [Key(1)]
        int Age { get; set; }
        [Key(2)]
        TrackableHand LeftHand { get; set; }
        [Key(3)]
        TrackableHand RightHand { get; set; }
    }

    [Union(0, typeof(TrackableHand))]
    public interface IHand : ITrackablePoco<IHand>
    {
        [Key(0)]
        TrackableRing MainRing { get; set; }
        [Key(1)]
        TrackableRing SubRing { get; set; }
    }

    [Union(0, typeof(TrackableRing))]
    public interface IRing : ITrackablePoco<IRing>
    {
        [Key(0)]
        string Name { get; set; }
        [Key(1)]
        int Power { get; set; }
    }
}
