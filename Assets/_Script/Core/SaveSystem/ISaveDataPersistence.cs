
namespace Script.Core.SaveSystem
{
    public interface ISaveDataPersistence
    {
        public void Save(SaveData saveData);
        public void Load(SaveData saveData);
    }
}
