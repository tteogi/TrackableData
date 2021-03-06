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
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using TrackableData.TestKits;
using Xunit;
using Xunit.Abstractions;
using Xunit.Sdk;

#region ITestPocoForContainer

namespace TrackableData.MongoDB.Tests
{
    public partial class TrackableTestPocoForContainer : ITestPocoForContainer
    {
        [IgnoreDataMember]
        public IPocoTracker<ITestPocoForContainer> Tracker { get; set; }

        public TrackableTestPocoForContainer Clone()
        {
            var o = new TrackableTestPocoForContainer();
            o._Name = _Name;
            o._Age = _Age;
            o._Extra = _Extra;
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
                var t = (IPocoTracker<ITestPocoForContainer>)value;
                Tracker = t;
            }
        }

        ITracker<ITestPocoForContainer> ITrackable<ITestPocoForContainer>.Tracker
        {
            get
            {
                return Tracker;
            }
            set
            {
                var t = (IPocoTracker<ITestPocoForContainer>)value;
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
            public static readonly PropertyInfo Name = typeof(ITestPocoForContainer).GetProperty("Name");
            public static readonly PropertyInfo Age = typeof(ITestPocoForContainer).GetProperty("Age");
            public static readonly PropertyInfo Extra = typeof(ITestPocoForContainer).GetProperty("Extra");
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

        private int _Extra;

        public int Extra
        {
            get
            {
                return _Extra;
            }
            set
            {
                if (Tracker != null && Extra != value)
                    Tracker.TrackSet(PropertyTable.Extra, _Extra, value);
                _Extra = value;
            }
        }
    }
}

#endregion
#region ITestPoco

namespace TrackableData.MongoDB.Tests
{
    public partial class TrackableTestPoco : ITestPoco
    {
        [IgnoreDataMember]
        public IPocoTracker<ITestPoco> Tracker { get; set; }

        public TrackableTestPoco Clone()
        {
            var o = new TrackableTestPoco();
            o._Id = _Id;
            o._Name = _Name;
            o._Age = _Age;
            o._Extra = _Extra;
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
                var t = (IPocoTracker<ITestPoco>)value;
                Tracker = t;
            }
        }

        ITracker<ITestPoco> ITrackable<ITestPoco>.Tracker
        {
            get
            {
                return Tracker;
            }
            set
            {
                var t = (IPocoTracker<ITestPoco>)value;
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
            public static readonly PropertyInfo Id = typeof(ITestPoco).GetProperty("Id");
            public static readonly PropertyInfo Name = typeof(ITestPoco).GetProperty("Name");
            public static readonly PropertyInfo Age = typeof(ITestPoco).GetProperty("Age");
            public static readonly PropertyInfo Extra = typeof(ITestPoco).GetProperty("Extra");
        }

        private ObjectId _Id;

        public ObjectId Id
        {
            get
            {
                return _Id;
            }
            set
            {
                if (Tracker != null && Id != value)
                    Tracker.TrackSet(PropertyTable.Id, _Id, value);
                _Id = value;
            }
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

        private int _Extra;

        public int Extra
        {
            get
            {
                return _Extra;
            }
            set
            {
                if (Tracker != null && Extra != value)
                    Tracker.TrackSet(PropertyTable.Extra, _Extra, value);
                _Extra = value;
            }
        }
    }
}

#endregion
#region ITestPocoWithCustomId

namespace TrackableData.MongoDB.Tests
{
    public partial class TrackableTestPocoWithCustomId : ITestPocoWithCustomId
    {
        [IgnoreDataMember]
        public IPocoTracker<ITestPocoWithCustomId> Tracker { get; set; }

        public TrackableTestPocoWithCustomId Clone()
        {
            var o = new TrackableTestPocoWithCustomId();
            o._CustomId = _CustomId;
            o._Name = _Name;
            o._Age = _Age;
            o._Extra = _Extra;
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
                var t = (IPocoTracker<ITestPocoWithCustomId>)value;
                Tracker = t;
            }
        }

        ITracker<ITestPocoWithCustomId> ITrackable<ITestPocoWithCustomId>.Tracker
        {
            get
            {
                return Tracker;
            }
            set
            {
                var t = (IPocoTracker<ITestPocoWithCustomId>)value;
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
            public static readonly PropertyInfo CustomId = typeof(ITestPocoWithCustomId).GetProperty("CustomId");
            public static readonly PropertyInfo Name = typeof(ITestPocoWithCustomId).GetProperty("Name");
            public static readonly PropertyInfo Age = typeof(ITestPocoWithCustomId).GetProperty("Age");
            public static readonly PropertyInfo Extra = typeof(ITestPocoWithCustomId).GetProperty("Extra");
        }

        private long _CustomId;

        public long CustomId
        {
            get
            {
                return _CustomId;
            }
            set
            {
                if (Tracker != null && CustomId != value)
                    Tracker.TrackSet(PropertyTable.CustomId, _CustomId, value);
                _CustomId = value;
            }
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

        private int _Extra;

        public int Extra
        {
            get
            {
                return _Extra;
            }
            set
            {
                if (Tracker != null && Extra != value)
                    Tracker.TrackSet(PropertyTable.Extra, _Extra, value);
                _Extra = value;
            }
        }
    }
}

#endregion
#region ITestContainer

namespace TrackableData.MongoDB.Tests
{
    public partial class TrackableTestContainer : ITestContainer
    {
        [IgnoreDataMember]
        private TrackableTestContainerTracker _tracker;

        [IgnoreDataMember]
        public TrackableTestContainerTracker Tracker
        {
            get
            {
                return _tracker;
            }
            set
            {
                _tracker = value;
                Person.Tracker = value?.PersonTracker;
                Missions.Tracker = value?.MissionsTracker;
                Tags.Tracker = value?.TagsTracker;
                Aliases.Tracker = value?.AliasesTracker;
            }
        }

        public TrackableTestContainer Clone()
        {
            var o = new TrackableTestContainer();
            o._Person = _Person?.Clone();
            o._Missions = _Missions?.Clone();
            o._Tags = _Tags?.Clone();
            o._Aliases = _Aliases?.Clone();
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
                var t = (TrackableTestContainerTracker)value;
                Tracker = t;
            }
        }

        ITracker<ITestContainer> ITrackable<ITestContainer>.Tracker
        {
            get
            {
                return Tracker;
            }
            set
            {
                var t = (TrackableTestContainerTracker)value;
                Tracker = t;
            }
        }

        IContainerTracker<ITestContainer> ITrackableContainer<ITestContainer>.Tracker
        {
            get
            {
                return Tracker;
            }
            set
            {
                var t = (TrackableTestContainerTracker)value;
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
                case "Missions":
                    return Missions as ITrackable;
                case "Tags":
                    return Tags as ITrackable;
                case "Aliases":
                    return Aliases as ITrackable;
                default:
                    return null;
            }
        }

        public IEnumerable<KeyValuePair<object, ITrackable>> GetChildTrackables(bool changedOnly = false)
        {
            var trackablePerson = Person as ITrackable;
            if (trackablePerson != null && (changedOnly == false || trackablePerson.Changed))
                yield return new KeyValuePair<object, ITrackable>("Person", trackablePerson);
            var trackableMissions = Missions as ITrackable;
            if (trackableMissions != null && (changedOnly == false || trackableMissions.Changed))
                yield return new KeyValuePair<object, ITrackable>("Missions", trackableMissions);
            var trackableTags = Tags as ITrackable;
            if (trackableTags != null && (changedOnly == false || trackableTags.Changed))
                yield return new KeyValuePair<object, ITrackable>("Tags", trackableTags);
            var trackableAliases = Aliases as ITrackable;
            if (trackableAliases != null && (changedOnly == false || trackableAliases.Changed))
                yield return new KeyValuePair<object, ITrackable>("Aliases", trackableAliases);
        }

        private TrackableTestPocoForContainer _Person = new TrackableTestPocoForContainer();

        public TrackableTestPocoForContainer Person
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

        TrackableTestPocoForContainer ITestContainer.Person
        {
            get { return _Person; }
            set { _Person = (TrackableTestPocoForContainer)value; }
        }

        private TrackableDictionary<int, MissionData> _Missions = new TrackableDictionary<int, MissionData>();

        public TrackableDictionary<int, MissionData> Missions
        {
            get
            {
                return _Missions;
            }
            set
            {
                if (_Missions != null)
                    _Missions.Tracker = null;
                if (value != null)
                    value.Tracker = Tracker?.MissionsTracker;
                _Missions = value;
            }
        }

        TrackableDictionary<int, MissionData> ITestContainer.Missions
        {
            get { return _Missions; }
            set { _Missions = (TrackableDictionary<int, MissionData>)value; }
        }

        private TrackableList<TagData> _Tags = new TrackableList<TagData>();

        public TrackableList<TagData> Tags
        {
            get
            {
                return _Tags;
            }
            set
            {
                if (_Tags != null)
                    _Tags.Tracker = null;
                if (value != null)
                    value.Tracker = Tracker?.TagsTracker;
                _Tags = value;
            }
        }

        TrackableList<TagData> ITestContainer.Tags
        {
            get { return _Tags; }
            set { _Tags = (TrackableList<TagData>)value; }
        }

        private TrackableSet<string> _Aliases = new TrackableSet<string>();

        public TrackableSet<string> Aliases
        {
            get
            {
                return _Aliases;
            }
            set
            {
                if (_Aliases != null)
                    _Aliases.Tracker = null;
                if (value != null)
                    value.Tracker = Tracker?.AliasesTracker;
                _Aliases = value;
            }
        }

        TrackableSet<string> ITestContainer.Aliases
        {
            get { return _Aliases; }
            set { _Aliases = (TrackableSet<string>)value; }
        }
    }

    public class TrackableTestContainerTracker : IContainerTracker<ITestContainer>
    {
        public TrackablePocoTracker<ITestPocoForContainer> PersonTracker { get; set; } = new TrackablePocoTracker<ITestPocoForContainer>();
        public TrackableDictionaryTracker<int, MissionData> MissionsTracker { get; set; } = new TrackableDictionaryTracker<int, MissionData>();
        public TrackableListTracker<TagData> TagsTracker { get; set; } = new TrackableListTracker<TagData>();
        public TrackableSetTracker<string> AliasesTracker { get; set; } = new TrackableSetTracker<string>();

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

            if (MissionsTracker != null && MissionsTracker.HasChange)
            {
                if (first)
                    first = false;
                else
                    sb.Append(", ");
                sb.Append("Missions:");
                sb.Append(MissionsTracker);
            }

            if (TagsTracker != null && TagsTracker.HasChange)
            {
                if (first)
                    first = false;
                else
                    sb.Append(", ");
                sb.Append("Tags:");
                sb.Append(TagsTracker);
            }

            if (AliasesTracker != null && AliasesTracker.HasChange)
            {
                if (first)
                    first = false;
                else
                    sb.Append(", ");
                sb.Append("Aliases:");
                sb.Append(AliasesTracker);
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
                    (MissionsTracker != null && MissionsTracker.HasChange) ||
                    (TagsTracker != null && TagsTracker.HasChange) ||
                    (AliasesTracker != null && AliasesTracker.HasChange) ||
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
            if (MissionsTracker != null)
                MissionsTracker.Clear();
            if (TagsTracker != null)
                TagsTracker.Clear();
            if (AliasesTracker != null)
                AliasesTracker.Clear();
        }

        public void ApplyTo(object trackable)
        {
            ApplyTo((ITestContainer)trackable);
        }

        public void ApplyTo(ITestContainer trackable)
        {
            if (PersonTracker != null)
                PersonTracker.ApplyTo(trackable.Person);
            if (MissionsTracker != null)
                MissionsTracker.ApplyTo(trackable.Missions);
            if (TagsTracker != null)
                TagsTracker.ApplyTo(trackable.Tags);
            if (AliasesTracker != null)
                AliasesTracker.ApplyTo(trackable.Aliases);
        }

        public void ApplyTo(ITracker tracker)
        {
            ApplyTo((TrackableTestContainerTracker)tracker);
        }

        public void ApplyTo(ITracker<ITestContainer> tracker)
        {
            ApplyTo((TrackableTestContainerTracker)tracker);
        }

        public void ApplyTo(TrackableTestContainerTracker tracker)
        {
            if (PersonTracker != null)
                PersonTracker.ApplyTo(tracker.PersonTracker);
            if (MissionsTracker != null)
                MissionsTracker.ApplyTo(tracker.MissionsTracker);
            if (TagsTracker != null)
                TagsTracker.ApplyTo(tracker.TagsTracker);
            if (AliasesTracker != null)
                AliasesTracker.ApplyTo(tracker.AliasesTracker);
        }

        public void RollbackTo(object trackable)
        {
            RollbackTo((ITestContainer)trackable);
        }

        public void RollbackTo(ITestContainer trackable)
        {
            if (PersonTracker != null)
                PersonTracker.RollbackTo(trackable.Person);
            if (MissionsTracker != null)
                MissionsTracker.RollbackTo(trackable.Missions);
            if (TagsTracker != null)
                TagsTracker.RollbackTo(trackable.Tags);
            if (AliasesTracker != null)
                AliasesTracker.RollbackTo(trackable.Aliases);
        }

        public void RollbackTo(ITracker tracker)
        {
            RollbackTo((TrackableTestContainerTracker)tracker);
        }

        public void RollbackTo(ITracker<ITestContainer> tracker)
        {
            RollbackTo((TrackableTestContainerTracker)tracker);
        }

        public void RollbackTo(TrackableTestContainerTracker tracker)
        {
            if (PersonTracker != null)
                PersonTracker.RollbackTo(tracker.PersonTracker);
            if (MissionsTracker != null)
                MissionsTracker.RollbackTo(tracker.MissionsTracker);
            if (TagsTracker != null)
                TagsTracker.RollbackTo(tracker.TagsTracker);
            if (AliasesTracker != null)
                AliasesTracker.RollbackTo(tracker.AliasesTracker);
        }
    }
}

#endregion
