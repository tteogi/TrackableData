﻿#define USE_PRECOMPILED_PROTOBUF_TYPEMODEL

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using ProtoBuf;
using ProtoBuf.Meta;
using TrackableData;
using TrackableData.Protobuf;
using Unity.Data;

namespace Basic
{
    class ProtobufExample
    {
        private static TypeModel _protobufTypeModel;

        private static TypeModel ProtobufTypeModel
        {
            get
            {
                if (_protobufTypeModel != null)
                    return _protobufTypeModel;
#if USE_PRECOMPILED_PROTOBUF_TYPEMODEL
                var model = new DataProtobufSerializer();
#else
                var model = TypeModel.Create();
                model.Add(typeof(TrackablePocoTracker<IUserData>), false)
                     .SetSurrogate(typeof(TrackableUserDataTrackerSurrogate));
                model.Add(typeof(TrackableDictionaryTracker<int, string>), false)
                     .SetSurrogate(typeof(TrackableDictionaryTrackerSurrogate<int, string>));
                model.Add(typeof(TrackableSetTracker<int>), false)
                     .SetSurrogate(typeof(TrackableSetTrackerSurrogate<int>));
                model.Add(typeof(TrackableListTracker<string>), false)
                     .SetSurrogate(typeof(TrackableListTrackerSurrogate<string>));
#endif
                return _protobufTypeModel = model;
            }
        }

        private static byte[] Serialize(object obj)
        {
            byte[] buf;
            using (var stream = new MemoryStream())
            {
                ProtobufTypeModel.Serialize(stream, obj);
                buf = stream.ToArray();
            }
            return buf;
        }

        private static T Deserialize<T>(byte[] buf)
        {
            using (var stream = new MemoryStream(buf))
            {
                return (T)ProtobufTypeModel.Deserialize(stream, null, typeof(T));
            }
        }

        private static byte[] PrintBytes(byte[] buf)
        {
            var c = 0;
            foreach (var b in buf)
            {
                Log.Write(b.ToString("X2"));
                if ((++c % 8) == 0)
                    Log.Write(" ");
            }
            Log.WriteLine(string.Format(" (Len: {0})", buf.Length));
            return buf;
        }

        private static void RunTrackablePoco()
        {
            Log.WriteLine("***** TrackablePoco (Protobuf) *****");

            var u = new TrackableUserData();
            u.SetDefaultTracker();

            u.Name = "Bob";
            u.Level = 1;
            u.Gold = 10;

            var buf = PrintBytes(Serialize(u.Tracker));
            Log.WriteLine(Deserialize<TrackablePocoTracker<IUserData>>(buf).ToString());
            u.Tracker.Clear();

            u.Level += 10;
            u.Gold += 100;

            var buf2 = PrintBytes(Serialize(u.Tracker));
            Log.WriteLine(Deserialize<TrackablePocoTracker<IUserData>>(buf2).ToString());
            u.Tracker.Clear();

            Log.WriteLine();
        }

        private static void RunTrackableDictionary()
        {
            Log.WriteLine("***** TrackableDictionary (Protobuf) *****");

            var dict = new TrackableDictionary<int, string>();
            dict.SetDefaultTracker();

            dict.Add(1, "One");
            dict.Add(2, "Two");
            dict.Add(3, "Three");

            var buf = PrintBytes(Serialize(dict.Tracker));
            Log.WriteLine(Deserialize<TrackableDictionaryTracker<int, string>>(buf).ToString());
            dict.Tracker.Clear();

            dict.Remove(1);
            dict[2] = "TwoTwo";
            dict.Add(4, "Four");

            var buf2 = PrintBytes(Serialize(dict.Tracker));
            Log.WriteLine(Deserialize<TrackableDictionaryTracker<int, string>>(buf2).ToString());
            dict.Tracker.Clear();

            Log.WriteLine();
        }

        private static void RunTrackableSet()
        {
            Log.WriteLine("***** TrackableSet (Protobuf) *****");

            var set = new TrackableSet<int>();
            set.SetDefaultTracker();

            set.Add(1);
            set.Add(2);
            set.Add(3);

            var buf = PrintBytes(Serialize(set.Tracker));
            Log.WriteLine(Deserialize<TrackableSetTracker<int>>(buf).ToString());
            set.Tracker.Clear();

            set.Remove(1);
            set.Add(4);

            var buf2 = PrintBytes(Serialize(set.Tracker));
            Log.WriteLine(Deserialize<TrackableSetTracker<int>>(buf2).ToString());
            set.Tracker.Clear();

            Log.WriteLine();
        }

        private static void RunTrackableList()
        {
            Log.WriteLine("***** TrackableList (Protobuf) *****");

            var list = new TrackableList<string>();
            list.SetDefaultTracker();

            list.Add("One");
            list.Add("Two");
            list.Add("Three");

            var buf = PrintBytes(Serialize(list.Tracker));
            Log.WriteLine(Deserialize<TrackableListTracker<string>>(buf).ToString());
            list.Tracker.Clear();

            list.RemoveAt(0);
            list[1] = "TwoTwo";
            list.Add("Four");

            var buf2 = PrintBytes(Serialize(list.Tracker));
            Log.WriteLine(Deserialize<TrackableListTracker<string>>(buf2).ToString());
            list.Tracker.Clear();

            Log.WriteLine();
        }

        public static void Run()
        {
            RunTrackablePoco();
            RunTrackableDictionary();
            RunTrackableSet();
            RunTrackableList();
        }
    }
}
