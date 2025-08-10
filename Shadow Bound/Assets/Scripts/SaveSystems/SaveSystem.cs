using System;
using System.Collections.Generic;
using System.IO;
using GameControllers.Models.Configs.Items;
using Newtonsoft.Json;
using SaveSystems.DataTypes;
using SaveSystems.Properties;
using UnityEngine;

namespace SaveSystems
{
    public class SaveSystem
    {
        private Dictionary<Type, ISaveDataHandler> _savesData;
        private GameSaveDataContainer _gameSaveDataContainer;
        private string _savePath;
        
        public const string SaveFileNameNinja = "NinjaRegularData.json";
        public const string SaveFileNameOnmyoji = "OnmyojiData.json";
        public const string SaveFileNameArcher = "ArcherData.json";

        public void InitializeFile(string fileName, WeaponConfig weaponConfig)
        {
            #if UNITY_EDITOR
                _savePath = Path.Combine(Application.dataPath, fileName);
            #else 
                _savePath = Path.Combine(Application.persistentDataPath, fileName);
            #endif
            
            if (!File.Exists(_savePath))
            {
                CreateSaveDataContainer(weaponConfig);
            }
            else
            {
                LoadData();
            }
        }
        
        private void LoadData()
        {
            var json = File.ReadAllText(_savePath);
            _gameSaveDataContainer = JsonConvert.DeserializeObject<GameSaveDataContainer>(json);
            
            _savesData = new Dictionary<Type, ISaveDataHandler>
            {
                [typeof(InventorySaveData)] = _gameSaveDataContainer.InventorySaveData,
                [typeof(PlayerSaveData)] = _gameSaveDataContainer.PlayerSaveData,
                [typeof(LevelsSaveData)]  = _gameSaveDataContainer.LevelsSaveData,
            };
            
            foreach (var dataHandler in _savesData.Values)
                dataHandler.RefreshDataTypes();
            
            foreach (var dataHandler in _savesData.Values)
                dataHandler.RefreshSavedData();
        }
        
        private void CreateSaveDataContainer(WeaponConfig weaponConfig)
        {
            _gameSaveDataContainer = new GameSaveDataContainer(weaponConfig);
            
            _savesData = new Dictionary<Type, ISaveDataHandler>
            {
                [typeof(InventorySaveData)] = _gameSaveDataContainer.InventorySaveData,
                [typeof(PlayerSaveData)] = _gameSaveDataContainer.PlayerSaveData,
                [typeof(LevelsSaveData)]  = _gameSaveDataContainer.LevelsSaveData,
            };
            
            WriteToJson();
        }

        public void WriteToJson()
        {
            foreach (var dataHandler in _savesData.Values)
                dataHandler.RefreshDataForSave();
            
            var json = JsonConvert.SerializeObject(_gameSaveDataContainer, Formatting.Indented);
            File.WriteAllText(_savePath, json);
        }

        public void SaveData<TData, TType>(TType data, string guid)
            where TData : ISaveDataHandler
        {
            _savesData[typeof(TData)].SaveData(data, guid);
        }

        public TType GetData<TData, TType>(string guid)
            where TData : ISaveDataHandler
        {
            return _savesData[typeof(TData)].GetData<TType>(guid);
        }
    }
}
