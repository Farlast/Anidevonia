using UnityEngine;
using UnityEngine.Events;
using System.Linq;
using System.Collections.Generic;

namespace Script.Core.SaveSystem
{
    public class SaveLoadSystem :MonoBehaviour
    {
        public static event UnityAction<SaveData> OnLoad = delegate { };
        //public static event UnityAction<SaveData> OnSave = delegate { };
        private List<ISaveDataPersistence> dataPersistenceObjects;
        [SerializeField] private SaveData saveData = new();

        private void Start()
        {
            FindObjectToSave();
        }
        public void Load()
        {
            print("game load");
            var data = JsonSerialization.Load(1);
            print(data.Player.HP);
            OnLoad?.Invoke(data);
        }

        public void Save()
        {
            print("game save");
            foreach(ISaveDataPersistence data in dataPersistenceObjects)
            {
                data.Save(saveData);
            }
            JsonSerialization.Save(saveData, 1);
        }
        private void FindObjectToSave()
        {
            IEnumerable<ISaveDataPersistence> dataPersistences = FindObjectsOfType<MonoBehaviour>().OfType<ISaveDataPersistence>();

            dataPersistenceObjects = new List<ISaveDataPersistence>(dataPersistences);
        }
    }
}
