using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
//using UnityEditor;
using UnityEngine;

namespace system {
    public static class LocalStorage {
       
        public static int Gold {
            get { return Get("gold", () => 0); }
            set { Set("gold", value); }
        }

        public static int LastChoosedHatID {
            get { return Get("lastChoosedHat", () => 0); }
            set { Set("lastChoosedHat", value); }
        }
        public static Dictionary<int,bool> SoldHats {
            get { return Get("score", () => new Dictionary<int, bool>(){{0, true}}); }
            set { Set("score", value); }
        }

        public static int Level {
            get {
                var v = Get("level", () => 1);

                return v == 0 ? 1 : v;
            }
            set { Set("level", value); }
        }

   


        private static readonly string StoredDataPath = Application.persistentDataPath + "/data.dt";

        private static Dictionary<string, object> _data;

        private static Dictionary<string, object> Data {
            get {
                EnsureData();
                return _data;
            }
        }

        private static void EnsureData() {
            var path = Application.persistentDataPath + "/data.dt";

            if (_data != null)
                return;
            if (!File.Exists(path)) {
                _data = new Dictionary<string, object>();
            } else {
                using (var f = File.Open(path, FileMode.Open)) {
                    _data = (Dictionary<string, object>) new BinaryFormatter().Deserialize(f);
                }
            }
        }

        private static T Get<T>(string key, Func<T> init) {
            if (Data.ContainsKey(key))
                return (T) Data[key];
            var result = init.Invoke();
            Data[key] = result;
            return result;
        }

        private static void Set<T>(string key, T value) {
            Data[key] = value;
            Save();
        }


        private static void Save() {
            using (var file = File.Create(StoredDataPath)) {
                new BinaryFormatter().Serialize(file, _data);
            }
        }
    }
}