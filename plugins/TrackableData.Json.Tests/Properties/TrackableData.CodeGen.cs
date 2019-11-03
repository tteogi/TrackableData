﻿// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by TrackableData.CodeGenerator.Core.
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
using System.Text;
using TrackableData;

#region IPerson

namespace TrackableData.Json.Tests
{
    public partial class TrackablePerson : IPerson
    {
        [IgnoreDataMember]

        public IPocoTracker<IPerson> Tracker { get; set; }

        public TrackablePerson Clone()
        {
            var o = new TrackablePerson();
            o._Name = _Name;
            o._Age = _Age;
            o._LeftHand = _LeftHand?.Clone();
            o._RightHand = _RightHand?.Clone();
            return o;
        }

        [IgnoreDataMember]
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

        ITrackable ITrackable.Clone()
        {
            return Clone();
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

        public TrackableHand Clone()
        {
            var o = new TrackableHand();
            o._MainRing = _MainRing?.Clone();
            o._SubRing = _SubRing?.Clone();
            return o;
        }

        [IgnoreDataMember]
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

        ITrackable ITrackable.Clone()
        {
            return Clone();
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

        public TrackableRing Clone()
        {
            var o = new TrackableRing();
            o._Name = _Name;
            o._Power = _Power;
            return o;
        }

        [IgnoreDataMember]
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

        ITrackable ITrackable.Clone()
        {
            return Clone();
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
                Set.Tracker = value?.SetTracker;
            }
        }

        public TrackableDataContainer Clone()
        {
            var o = new TrackableDataContainer();
            o._Person = _Person?.Clone();
            o._Dictionary = _Dictionary?.Clone();
            o._List = _List?.Clone();
            o._Set = _Set?.Clone();
            return o;
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

        ITrackable ITrackable.Clone()
        {
            return Clone();
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
                case "Set":
                    return Set as ITrackable;
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
            var trackableSet = Set as ITrackable;
            if (trackableSet != null && (changedOnly == false || trackableSet.Changed))
                yield return new KeyValuePair<object, ITrackable>("Set", trackableSet);
        }

        private TrackablePerson _Person = new TrackablePerson();

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

        private TrackableDictionary<int, string> _Dictionary = new TrackableDictionary<int, string>();

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

        private TrackableList<string> _List = new TrackableList<string>();

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

        private TrackableSet<int> _Set = new TrackableSet<int>();

        public TrackableSet<int> Set
        {
            get
            {
                return _Set;
            }
            set
            {
                if (_Set != null)
                    _Set.Tracker = null;
                if (value != null)
                    value.Tracker = Tracker?.SetTracker;
                _Set = value;
            }
        }

        TrackableSet<int> IDataContainer.Set
        {
            get { return _Set; }
            set { _Set = (TrackableSet<int>)value; }
        }
    }

    public class TrackableDataContainerTracker : IContainerTracker<IDataContainer>
    {
        public TrackablePocoTracker<IPerson> PersonTracker { get; set; } = new TrackablePocoTracker<IPerson>();
        public TrackableDictionaryTracker<int, string> DictionaryTracker { get; set; } = new TrackableDictionaryTracker<int, string>();
        public TrackableListTracker<string> ListTracker { get; set; } = new TrackableListTracker<string>();
        public TrackableSetTracker<int> SetTracker { get; set; } = new TrackableSetTracker<int>();

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("{ ");
            var first = true;
            if (PersonTracker != null && PersonTracker.HasChange)
            {
                if (first)
                    first = false;
                else
                    sb.Append(", ");
                sb.Append("Person:");
                sb.Append(PersonTracker);
            }

            if (DictionaryTracker != null && DictionaryTracker.HasChange)
            {
                if (first)
                    first = false;
                else
                    sb.Append(", ");
                sb.Append("Dictionary:");
                sb.Append(DictionaryTracker);
            }

            if (ListTracker != null && ListTracker.HasChange)
            {
                if (first)
                    first = false;
                else
                    sb.Append(", ");
                sb.Append("List:");
                sb.Append(ListTracker);
            }

            if (SetTracker != null && SetTracker.HasChange)
            {
                if (first)
                    first = false;
                else
                    sb.Append(", ");
                sb.Append("Set:");
                sb.Append(SetTracker);
            }

            sb.Append(" }");
            return sb.ToString();
        }

        public bool HasChange
        {
            get
            {
                return
                    (PersonTracker != null && PersonTracker.HasChange) ||
                    (DictionaryTracker != null && DictionaryTracker.HasChange) ||
                    (ListTracker != null && ListTracker.HasChange) ||
                    (SetTracker != null && SetTracker.HasChange) ||
                    false;
            }
        }

        public event TrackerHasChangeSet HasChangeSet
        {
            add { throw new NotImplementedException(); }
            remove { throw new NotImplementedException(); }
        }

        public void Clear()
        {
            if (PersonTracker != null)
                PersonTracker.Clear();
            if (DictionaryTracker != null)
                DictionaryTracker.Clear();
            if (ListTracker != null)
                ListTracker.Clear();
            if (SetTracker != null)
                SetTracker.Clear();
        }

        public void ApplyTo(object trackable)
        {
            ApplyTo((IDataContainer)trackable);
        }

        public void ApplyTo(IDataContainer trackable)
        {
            if (PersonTracker != null)
                PersonTracker.ApplyTo(trackable.Person);
            if (DictionaryTracker != null)
                DictionaryTracker.ApplyTo(trackable.Dictionary);
            if (ListTracker != null)
                ListTracker.ApplyTo(trackable.List);
            if (SetTracker != null)
                SetTracker.ApplyTo(trackable.Set);
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
            if (PersonTracker != null)
                PersonTracker.ApplyTo(tracker.PersonTracker);
            if (DictionaryTracker != null)
                DictionaryTracker.ApplyTo(tracker.DictionaryTracker);
            if (ListTracker != null)
                ListTracker.ApplyTo(tracker.ListTracker);
            if (SetTracker != null)
                SetTracker.ApplyTo(tracker.SetTracker);
        }

        public void RollbackTo(object trackable)
        {
            RollbackTo((IDataContainer)trackable);
        }

        public void RollbackTo(IDataContainer trackable)
        {
            if (PersonTracker != null)
                PersonTracker.RollbackTo(trackable.Person);
            if (DictionaryTracker != null)
                DictionaryTracker.RollbackTo(trackable.Dictionary);
            if (ListTracker != null)
                ListTracker.RollbackTo(trackable.List);
            if (SetTracker != null)
                SetTracker.RollbackTo(trackable.Set);
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
            if (PersonTracker != null)
                PersonTracker.RollbackTo(tracker.PersonTracker);
            if (DictionaryTracker != null)
                DictionaryTracker.RollbackTo(tracker.DictionaryTracker);
            if (ListTracker != null)
                ListTracker.RollbackTo(tracker.ListTracker);
            if (SetTracker != null)
                SetTracker.RollbackTo(tracker.SetTracker);
        }
    }
}

#endregion
