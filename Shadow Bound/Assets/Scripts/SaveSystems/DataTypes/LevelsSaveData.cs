using System;
using System.Collections.Generic;

namespace SaveSystems.DataTypes
{
    public class LevelsSaveData : SaveDataHandler
    {
        public const string GUIDLevelState = "LevelState";
        public const string GUIDLevelState0 = "LevelState0";
        public const string GUIDLevelState1 = "LevelState1";
        public const string GUIDLevelState2 = "LevelState2";
        public const string GUIDLevelState3 = "LevelState3";
        public const string GUIDLevelState4 = "LevelState4";
        public const string GUIDLevelState5 = "LevelState5";
        public const string GUIDLevelState6 = "LevelState6";
        public const string GUIDLevelState7 = "LevelState7";
        public const string GUIDLevelState8 = "LevelState8";
        public const string GUIDLevelState9 = "LevelState9";
        public const string GUIDLevelState10 = "LevelState10";
        public const string GUIDLevelState11 = "LevelState11";
        public const string GUIDLevelState12 = "LevelState12";
        public const string GUIDLevelState13 = "LevelState13";
        public const string GUIDLevelState14 = "LevelState14";
        public const string GUIDLevelState15 = "LevelState15";
        public const string GUIDLevelState16 = "LevelState16";
        public const string GUIDLevelState17 = "LevelState17";

        public const int LastLevelIndex = 17;
        
        public readonly Dictionary<string, bool> LevelsData = new ();
        
        public LevelsSaveData()
        {
            TypeClass = typeof(LevelsSaveData);
            
            SavedData = new Dictionary<Type, Dictionary<string, object>>
            {
                {
                    typeof(bool), new Dictionary<string, object>
                    {
                        { GUIDLevelState0, true },
                        { GUIDLevelState1, false },
                        { GUIDLevelState2, false },
                        { GUIDLevelState3, false },
                        { GUIDLevelState4, false },
                        { GUIDLevelState5, false },
                        { GUIDLevelState6, false },
                        { GUIDLevelState7, false },
                        { GUIDLevelState8, false },
                        { GUIDLevelState9, false },
                        { GUIDLevelState10, false },
                        { GUIDLevelState11, false },
                        { GUIDLevelState12, false },
                        { GUIDLevelState13, false },
                        { GUIDLevelState14, false },
                        { GUIDLevelState15, false },
                        { GUIDLevelState16, false },
                        { GUIDLevelState17, false },
                    }
                },
            };
        }
        
        public override void RefreshDataTypes()
        {
            
        }

        public override void RefreshSavedData()
        {
            foreach (var levelData in LevelsData)
            {
                SavedData[typeof(bool)][levelData.Key] = LevelsData[levelData.Key];
            }
        }

        public override void RefreshDataForSave()
        {
            foreach (var savedData in SavedData[typeof(bool)])
            {
                LevelsData[savedData.Key] = (bool)SavedData[typeof(bool)][savedData.Key];
            }
        }
    }
}
