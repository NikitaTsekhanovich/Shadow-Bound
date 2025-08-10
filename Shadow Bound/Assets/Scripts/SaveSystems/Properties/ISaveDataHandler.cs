namespace SaveSystems.Properties
{
    public interface ISaveDataHandler
    {
        public void RefreshDataTypes();
        public void RefreshSavedData();
        public void RefreshDataForSave();
        public void SaveData<TType>(TType data, string guid);
        public TType GetData<TType>(string guid);
    }
}
