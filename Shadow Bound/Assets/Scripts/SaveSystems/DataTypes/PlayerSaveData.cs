using System;
using System.Collections.Generic;

namespace SaveSystems.DataTypes
{
    public class PlayerSaveData : SaveDataHandler
    {
        public const string GUIDExperience = "Experience";
        public const string GUIDLevel = "Level";
        
        public readonly Dictionary<string, int> PlayerData = new ();

        public PlayerSaveData()
        {
            TypeClass = typeof(PlayerSaveData);
            
            SavedData = new Dictionary<Type, Dictionary<string, object>>
            {
                {
                    typeof(int), new Dictionary<string, object>
                    {
                        { GUIDExperience, 0 },
                        { GUIDLevel, 1 },
                    }
                },
            };
        }
        
        public override void RefreshDataTypes()
        {
            
        }

        public override void RefreshSavedData()
        {
            foreach (var playerData in PlayerData)
            {
                SavedData[typeof(int)][playerData.Key] = PlayerData[playerData.Key];
            }
        }

        public override void RefreshDataForSave()
        {
            foreach (var savedData in SavedData[typeof(int)])
            {
                PlayerData[savedData.Key] = (int)SavedData[typeof(int)][savedData.Key];
            }
        }
    }
}
