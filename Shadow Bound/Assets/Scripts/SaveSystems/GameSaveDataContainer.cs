using System;
using GameControllers.Models.Configs.Items;
using Newtonsoft.Json;
using SaveSystems.DataTypes;

namespace SaveSystems
{
    [Serializable]
    public class GameSaveDataContainer
    {
        public InventorySaveData InventorySaveData { get; private set; }
        public PlayerSaveData PlayerSaveData { get; private set; }
        public LevelsSaveData LevelsSaveData { get; private set; }
         
        public GameSaveDataContainer(WeaponConfig weaponConfig)
        {
            InventorySaveData = new InventorySaveData(weaponConfig);
            PlayerSaveData = new PlayerSaveData();
            LevelsSaveData = new LevelsSaveData();
        }
        
        [JsonConstructor]
        public GameSaveDataContainer()
        {
            InventorySaveData = new InventorySaveData();
            PlayerSaveData = new PlayerSaveData();
            LevelsSaveData = new LevelsSaveData();
        }
    }
}
