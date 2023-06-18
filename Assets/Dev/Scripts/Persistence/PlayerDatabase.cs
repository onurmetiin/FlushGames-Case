using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace Assets.Dev.Scripts.Persistence
{
    [CreateAssetMenu(fileName = "database", menuName = "Database")]
    public class PlayerDatabase : ScriptableObject
    {
        private static PlayerDatabase _inst;
        public static PlayerDatabase Instance
        {
            get
            {
                if (!_inst)
                {
#if UNITY_EDITOR
                    _inst = (PlayerDatabase)AssetDatabase.LoadAssetAtPath("Assets/Dev/Scripts/Persistence/Database.asset", typeof(PlayerDatabase));
#else
                    _inst = Resources.Load<PlayerDatabase>("Database");
#endif
                    LoadData();
                    SaveData();
                }
                return _inst;
            }
        }

        public Dictionary<string, int> gemStatistics = new Dictionary<string, int>();

        public static void SaveData()
        {
            string path = Path.Combine(Application.persistentDataPath, "PlayerDatabase.data");
            string json = JsonUtility.ToJson(_inst, true);
            File.WriteAllText(path, json);
        }
        public static void LoadData()
        {
            string path = Path.Combine(Application.persistentDataPath, "PlayerDatabase.data");
            Debug.Log(path);
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                JsonUtility.FromJsonOverwrite(json, _inst);
            }
        }
    }
}
