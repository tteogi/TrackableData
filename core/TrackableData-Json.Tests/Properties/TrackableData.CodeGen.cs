// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Akka.Interfaced CodeGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;
using System.Linq;
using TrackableData;

#region IPerson

namespace TrackableData.Json.Tests
{
    public partial class TrackablePerson : IPerson
    {
        [IgnoreDataMember]
        public IPocoTracker<IPerson> Tracker { get; set; }

        public bool Changed { get { return Tracker != null && Tracker.HasChange; } }

        ITracker ITrackable.Tracker
        {
            get
            {
                return Tracker;
            }
            set
            {
                var t = (IPocoTracker<IPerson>)value;
                Tracker = t;
            }
        }

        ITracker<IPerson> ITrackable<IPerson>.Tracker
        {
            get
            {
                return Tracker;
            }
            set
            {
                var t = (IPocoTracker<IPerson>)value;
                Tracker = t;
            }
        }

        public ITrackable GetChildTrackable(object name)
        {
            switch ((string)name)
            {
                case "LeftHand":
                    return LeftHand as ITrackable;
                case "RightHand":
                    return RightHand as ITrackable;
                default:
                    return null;
            }
        }

        public IEnumerable<KeyValuePair<object, ITrackable>> GetChildTrackables(bool changedOnly = false)
        {
            var trackableLeftHand = LeftHand as ITrackable;
            if (trackableLeftHand != null && (changedOnly == false || trackableLeftHand.Changed))
                yield return new KeyValuePair<object, ITrackable>("LeftHand", trackableLeftHand);
            var trackableRightHand = RightHand as ITrackable;
            if (trackableRightHand != null && (changedOnly == false || trackableRightHand.Changed))
                yield return new KeyValuePair<object, ITrackable>("RightHand", trackableRightHand);
        }

        public static class PropertyTable
        {
            public static readonly PropertyInfo Name = typeof(IPerson).GetProperty("Name");
            public static readonly PropertyInfo Age = typeof(IPerson).GetProperty("Age");
            public static readonly PropertyInfo LeftHand = typeof(IPerson).GetProperty("LeftHand");
            public static readonly PropertyInfo RightHand = typeof(IPerson).GetProperty("RightHand");
        }

        private string _Name;

        public string Name
        {
            get
            {
                return _Name;
            }
            set
            {
                if (Tracker != null && Name != value)
                    Tracker.TrackSet(PropertyTable.Name, _Name, value);
                _Name = value;
            }
        }

        private int _Age;

        public int Age
        {
            get
            {
                return _Age;
            }
            set
            {
                if (Tracker != null && Age != value)
                    Tracker.TrackSet(PropertyTable.Age, _Age, value);
                _Age = value;
            }
        }

        private TrackableHand _LeftHand;

        public TrackableHand LeftHand
        {
            get
            {
                return _LeftHand;
            }
            set
            {
                if (Tracker != null && LeftHand != value)
                    Tracker.TrackSet(PropertyTable.LeftHand, _LeftHand, value);
                _LeftHand = value;
            }
        }

        private TrackableHand _RightHand;

        public TrackableHand RightHand
        {
            get
            {
                return _RightHand;
            }
            set
            {
                if (Tracker != null && RightHand != value)
                    Tracker.TrackSet(PropertyTable.RightHand, _RightHand, value);
                _RightHand = value;
            }
        }
    }
}

#endregion

#region IHand

namespace TrackableData.Json.Tests
{
    public partial class TrackableHand : IHand
    {
        [IgnoreDataMember]
        public IPocoTracker<IHand> Tracker { get; set; }

        public bool Changed { get { return Tracker != null && Tracker.HasChange; } }

        ITracker ITrackable.Tracker
        {
            get
            {
                return Tracker;
            }
            set
            {
                var t = (IPocoTracker<IHand>)value;
                Tracker = t;
            }
        }

        ITracker<IHand> ITrackable<IHand>.Tracker
        {
            get
            {
                return Tracker;
            }
            set
            {
                var t = (IPocoTracker<IHand>)value;
                Tracker = t;
            }
        }

        public ITrackable GetChildTrackable(object name)
        {
            switch ((string)name)
            {
                case "MainRing":
                    return MainRing as ITrackable;
                case "SubRing":
                    return SubRing as ITrackable;
                default:
                    return null;
            }
        }

        public IEnumerable<KeyValuePair<object, ITrackable>> GetChildTrackables(bool changedOnly = false)
        {
            var trackableMainRing = MainRing as ITrackable;
            if (trackableMainRing != null && (changedOnly == false || trackableMainRing.Changed))
                yield return new KeyValuePair<object, ITrackable>("MainRing", trackableMainRing);
            var trackableSubRing = SubRing as ITrackable;
            if (trackableSubRing != null && (changedOnly == false || trackableSubRing.Changed))
                yield return new KeyValuePair<object, ITrackable>("SubRing", trackableSubRing);
        }

        public static class PropertyTable
        {
            public static readonly PropertyInfo MainRing = typeof(IHand).GetProperty("MainRing");
            public static readonly PropertyInfo SubRing = typeof(IHand).GetProperty("SubRing");
        }

        private TrackableRing _MainRing;

        public TrackableRing MainRing
        {
            get
            {
                return _MainRing;
            }
            set
            {
                if (Tracker != null && MainRing != value)
                    Tracker.TrackSet(PropertyTable.MainRing, _MainRing, value);
                _MainRing = value;
            }
        }

        private TrackableRing _SubRing;

        public TrackableRing SubRing
        {
            get
            {
                return _SubRing;
            }
            set
            {
                if (Tracker != null && SubRing != value)
                    Tracker.TrackSet(PropertyTable.SubRing, _SubRing, value);
                _SubRing = value;
            }
        }
    }
}

#endregion

#region IRing

namespace TrackableData.Json.Tests
{
    public partial class TrackableRing : IRing
    {
        [IgnoreDataMember]
        public IPocoTracker<IRing> Tracker { get; set; }

        public bool Changed { get { return Tracker != null && Tracker.HasChange; } }

        ITracker ITrackable.Tracker
        {
            get
            {
                return Tracker;
            }
            set
            {
                var t = (IPocoTracker<IRing>)value;
                Tracker = t;
            }
        }

        ITracker<IRing> ITrackable<IRing>.Tracker
        {
            get
            {
                return Tracker;
            }
            set
            {
                var t = (IPocoTracker<IRing>)value;
                Tracker = t;
            }
        }

        public ITrackable GetChildTrackable(object name)
        {
            switch ((string)name)
            {
                default:
                    return null;
            }
        }

        public IEnumerable<KeyValuePair<object, ITrackable>> GetChildTrackables(bool changedOnly = false)
        {
            yield break;
        }

        public static class PropertyTable
        {
            public static readonly PropertyInfo Name = typeof(IRing).GetProperty("Name");
            public static readonly PropertyInfo Power = typeof(IRing).GetProperty("Power");
        }

        private string _Name;

        public string Name
        {
            get
            {
                return _Name;
            }
            set
            {
                if (Tracker != null && Name != value)
                    Tracker.TrackSet(PropertyTable.Name, _Name, value);
                _Name = value;
            }
        }

        private int _Power;

        public int Power
        {
            get
            {
                return _Power;
            }
            set
            {
                if (Tracker != null && Power != value)
                    Tracker.TrackSet(PropertyTable.Power, _Power, value);
                _Power = value;
            }
        }
    }
}

#endregion

#region IDataContainer

namespace TrackableData.Json.Tests
{
    public partial class TrackableDataContainer : IDataContainer
    {
        [IgnoreDataMember]
        private TrackableDataContainerTracker _tracker;

        [IgnoreDataMember]
        public TrackableDataContainerTracker Tracker
        {
            get
            {
                return _tracker;
            }
            set
            {
                _tracker = value;
                Person.Tracker = value?.PersonTracker;
                Dictionary.Tracker = value?.DictionaryTracker;
                List.Tracker = value?.ListTracker;
            }
        }

        public bool Changed { get { return Tracker != null && Tracker.HasChange; } }

        ITracker ITrackable.Tracker
        {
            get
            {
                return Tracker;
            }
            set
            {
                var t = (TrackableDataContainerTracker)value;
                Tracker = t;
            }
        }

        ITracker<IDataContainer> ITrackable<IDataContainer>.Tracker
        {
            get
            {
                return Tracker;
            }
            set
            {
                var t = (TrackableDataContainerTracker)value;
                Tracker = t;
            }
        }

        IContainerTracker<IDataContainer> ITrackableContainer<IDataContainer>.Tracker
        {
            get
            {
                return Tracker;
            }
            set
            {
                var t = (TrackableDataContainerTracker)value;
                Tracker = t;
            }
        }

        public ITrackable GetChildTrackable(object name)
        {
            switch ((string)name)
            {
                case "Person":
                    return Person as ITrackable;
                case "Dictionary":
                    return Dictionary as ITrackable;
                case "List":
                    return List as ITrackable;
                default:
                    return null;
            }
        }

        public IEnumerable<KeyValuePair<object, ITrackable>> GetChildTrackables(bool changedOnly = false)
        {
            var trackablePerson = Person as ITrackable;
            if (trackablePerson != null && (changedOnly == false || trackablePerson.Changed))
                yield return new KeyValuePair<object, ITrackable>("Person", trackablePerson);
            var trackableDictionary = Dictionary as ITrackable;
            if (trackableDictionary != null && (changedOnly == false || trackableDictionary.Changed))
                yield return new KeyValuePair<object, ITrackable>("Dictionary", trackableDictionary);
            var trackableList = List as ITrackable;
            if (trackableList != null && (changedOnly == false || trackableList.Changed))
                yield return new KeyValuePair<object, ITrackable>("List", trackableList);
        }

        private TrackablePerson _Person;

        public TrackablePerson Person
        {
            get
            {
                return _Person;
            }
            set
            {
                if (_Person != null)
                    _Person.Tracker = null;
                if (value != null)
                    value.Tracker = Tracker?.PersonTracker;
                _Person = value;
            }
        }

        TrackablePerson IDataContainer.Person
        {
            get { return _Person; }
            set { _Person = (TrackablePerson)value; }
        }

        private TrackableDictionary<int, string> _Dictionary;

        public TrackableDictionary<int, string> Dictionary
        {
            get
            {
                return _Dictionary;
            }
            set
            {
                if (_Dictionary != null)
                    _Dictionary.Tracker = null;
                if (value != null)
                    value.Tracker = Tracker?.DictionaryTracker;
                _Dictionary = value;
            }
        }

        TrackableDictionary<int, string> IDataContainer.Dictionary
        {
            get { return _Dictionary; }
            set { _Dictionary = (TrackableDictionary<int, string>)value; }
        }

        private TrackableList<string> _List;

        public TrackableList<string> List
        {
            get
            {
                return _List;
            }
            set
            {
                if (_List != null)
                    _List.Tracker = null;
                if (value != null)
                    value.Tracker = Tracker?.ListTracker;
                _List = value;
            }
        }

        TrackableList<string> IDataContainer.List
        {
            get { return _List; }
            set { _List = (TrackableList<string>)value; }
        }
    }

    public class TrackableDataContainerTracker : IContainerTracker<IDataContainer>
    {
        public TrackablePocoTracker<IPerson> PersonTracker = new TrackablePocoTracker<IPerson>();
        public TrackableDictionaryTracker<int, string> DictionaryTracker = new TrackableDictionaryTracker<int, string>();
        public TrackableListTracker<string> ListTracker = new TrackableListTracker<string>();

        public bool HasChange
        {
            get
            {
                return
                    PersonTracker.HasChange ||
                    DictionaryTracker.HasChange ||
                    ListTracker.HasChange ||
                    false;
            }
        }

        public void Clear()
        {
            PersonTracker.Clear();
            DictionaryTracker.Clear();
            ListTracker.Clear();
        }

        public void ApplyTo(object trackable)
        {
            ApplyTo((IDataContainer)trackable);
        }

        public void ApplyTo(IDataContainer trackable)
        {
            PersonTracker.ApplyTo(trackable.Person);
            DictionaryTracker.ApplyTo(trackable.Dictionary);
            ListTracker.ApplyTo(trackable.List);
        }

        public void ApplyTo(ITracker tracker)
        {
            ApplyTo((TrackableDataContainerTracker)tracker);
        }

        public void ApplyTo(ITracker<IDataContainer> tracker)
        {
            ApplyTo((TrackableDataContainerTracker)tracker);
        }

        public void ApplyTo(TrackableDataContainerTracker tracker)
        {
            PersonTracker.ApplyTo(tracker.PersonTracker);
            DictionaryTracker.ApplyTo(tracker.DictionaryTracker);
            ListTracker.ApplyTo(tracker.ListTracker);
        }

        public void RollbackTo(object trackable)
        {
            RollbackTo((IDataContainer)trackable);
        }

        public void RollbackTo(IDataContainer trackable)
        {
            PersonTracker.RollbackTo(trackable.Person);
            DictionaryTracker.RollbackTo(trackable.Dictionary);
            ListTracker.RollbackTo(trackable.List);
        }

        public void RollbackTo(ITracker tracker)
        {
            RollbackTo((TrackableDataContainerTracker)tracker);
        }

        public void RollbackTo(ITracker<IDataContainer> tracker)
        {
            RollbackTo((TrackableDataContainerTracker)tracker);
        }

        public void RollbackTo(TrackableDataContainerTracker tracker)
        {
            PersonTracker.RollbackTo(tracker.PersonTracker);
            DictionaryTracker.RollbackTo(tracker.DictionaryTracker);
            ListTracker.RollbackTo(tracker.ListTracker);
        }
    }
}

#endregion
