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
using TrackableData;

#region IUserData

namespace Basic.Data
{
    public class TrackableUserData : IUserData, ITrackable<IUserData>
    {
        [IgnoreDataMember]
        public IPocoTracker<IUserData> Tracker { get; set; }

        public bool Changed { get { return Tracker != null && Tracker.HasChange; } }

        ITracker ITrackable.Tracker
        {
            get
            {
                return Tracker;
            }
            set
            {
                var t = (IPocoTracker<IUserData>)value;
                Tracker = t;
            }
        }

        ITracker<IUserData> ITrackable<IUserData>.Tracker
        {
            get
            {
                return Tracker;
            }
            set
            {
                var t = (IPocoTracker<IUserData>)value;
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
            public static readonly PropertyInfo Name = typeof(IUserData).GetProperty("Name");
            public static readonly PropertyInfo Gold = typeof(IUserData).GetProperty("Gold");
            public static readonly PropertyInfo Level = typeof(IUserData).GetProperty("Level");
            public static readonly PropertyInfo LeftHand = typeof(IUserData).GetProperty("LeftHand");
            public static readonly PropertyInfo RightHand = typeof(IUserData).GetProperty("RightHand");
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

        private int _Gold;

        public int Gold
        {
            get
            {
                return _Gold;
            }
            set
            {
                if (Tracker != null && Gold != value)
                    Tracker.TrackSet(PropertyTable.Gold, _Gold, value);
                _Gold = value;
            }
        }

        private int _Level;

        public int Level
        {
            get
            {
                return _Level;
            }
            set
            {
                if (Tracker != null && Level != value)
                    Tracker.TrackSet(PropertyTable.Level, _Level, value);
                _Level = value;
            }
        }

        private IUserHandData _LeftHand;

        public IUserHandData LeftHand
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

        private IUserHandData _RightHand;

        public IUserHandData RightHand
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

#region IUserHandData

namespace Basic.Data
{
    public class TrackableUserHandData : IUserHandData, ITrackable<IUserHandData>
    {
        [IgnoreDataMember]
        public IPocoTracker<IUserHandData> Tracker { get; set; }

        public bool Changed { get { return Tracker != null && Tracker.HasChange; } }

        ITracker ITrackable.Tracker
        {
            get
            {
                return Tracker;
            }
            set
            {
                var t = (IPocoTracker<IUserHandData>)value;
                Tracker = t;
            }
        }

        ITracker<IUserHandData> ITrackable<IUserHandData>.Tracker
        {
            get
            {
                return Tracker;
            }
            set
            {
                var t = (IPocoTracker<IUserHandData>)value;
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
            public static readonly PropertyInfo FingerCount = typeof(IUserHandData).GetProperty("FingerCount");
            public static readonly PropertyInfo Dirty = typeof(IUserHandData).GetProperty("Dirty");
        }

        private int _FingerCount;

        public int FingerCount
        {
            get
            {
                return _FingerCount;
            }
            set
            {
                if (Tracker != null && FingerCount != value)
                    Tracker.TrackSet(PropertyTable.FingerCount, _FingerCount, value);
                _FingerCount = value;
            }
        }

        private bool _Dirty;

        public bool Dirty
        {
            get
            {
                return _Dirty;
            }
            set
            {
                if (Tracker != null && Dirty != value)
                    Tracker.TrackSet(PropertyTable.Dirty, _Dirty, value);
                _Dirty = value;
            }
        }
    }
}

#endregion