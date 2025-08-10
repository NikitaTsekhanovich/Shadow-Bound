using System;
using System.Collections.Generic;
using GameControllers.Models.Configs.Items;
using GameControllers.Models.ItemsEnums;
using Models.Items;
using Models.Items.BasicItemCharacteristicsTypes;
using Newtonsoft.Json;

namespace SaveSystems.DataTypes
{
    public class InventorySaveData : SaveDataHandler
    {
        public const string GUIDMainInventorySlot = "MainInventorySlot";
        public const string GUIDMainInventorySlot1 = "MainInventorySlot1";
        public const string GUIDMainInventorySlot2 = "MainInventorySlot2";
        public const string GUIDMainInventorySlot3 = "MainInventorySlot3";
        public const string GUIDMainInventorySlot4 = "MainInventorySlot4";
        public const string GUIDMainInventorySlot5 = "MainInventorySlot5";
        public const string GUIDMainInventorySlot6 = "MainInventorySlot6";
        public const string GUIDMainInventorySlot7 = "MainInventorySlot7";
        public const string GUIDMainInventorySlot8 = "MainInventorySlot8";
        public const string GUIDMainInventorySlot9 = "MainInventorySlot9";
        public const string GUIDMainInventorySlot10 = "MainInventorySlot10";
        public const string GUIDMainInventorySlot11 = "MainInventorySlot11";
        public const string GUIDMainInventorySlot12 = "MainInventorySlot12";
        public const string GUIDMainInventorySlot13 = "MainInventorySlot13";
        public const string GUIDMainInventorySlot14 = "MainInventorySlot14";
        public const string GUIDMainInventorySlot15 = "MainInventorySlot15";
        public const string GUIDMainInventorySlot16 = "MainInventorySlot16";
        
        public const string GUIDPlayerInventorySlot = "PlayerInventorySlot";
        public const string GUIDPlayerInventorySlot1 = "PlayerInventorySlot1";
        public const string GUIDPlayerInventorySlot2 = "PlayerInventorySlot2";
        public const string GUIDPlayerInventorySlot3 = "PlayerInventorySlot3";
        public const string GUIDPlayerInventorySlot4 = "PlayerInventorySlot4";
        public const string GUIDPlayerInventorySlot5 = "PlayerInventorySlot5";
        public const string GUIDPlayerInventorySlot6 = "PlayerInventorySlot6";
        public const string GUIDPlayerInventorySlot7 = "PlayerInventorySlot7";
        
        public const string GUIDCraftInventorySlot = "CraftInventorySlot";
        public const string GUIDCraftInventorySlot1 = "CraftInventorySlot1";
        public const string GUIDCraftInventorySlot2 = "CraftInventorySlot2";
        public const string GUIDCraftInventorySlot3 = "CraftInventorySlot3";
        public const string GUIDCraftInventorySlot4 = "CraftInventorySlot4";
        public const string GUIDCraftInventorySlot5 = "CraftInventorySlot5";
        public const string GUIDCraftInventorySlot6 = "CraftInventorySlot6";
        public const string GUIDCraftInventorySlot7 = "CraftInventorySlot7";
        public const string GUIDCraftInventorySlot8 = "CraftInventorySlot8";

        public const string GUIDEquipmentSlot = "EquipmentSlot";
        public const string GUIDEquipmentSlot1 = "EquipmentSlot1";
        public const string GUIDEquipmentSlot2 = "EquipmentSlot2";
        
        public readonly Dictionary<string, object> InventoryData = new ();
        
        public InventorySaveData(WeaponConfig weaponConfig)
        {
            WeaponCharacteristics startWeapon = null;
            
            if (weaponConfig != null)
                startWeapon = new WeaponCharacteristics(
                    weaponConfig.WeaponCharacteristicsData,
                    weaponConfig.BasicItemCharacteristicsData,
                    1);

            InitSaveData(startWeapon);
        }
        
        [JsonConstructor]
        public InventorySaveData()
        {
            InitSaveData(null);
        }

        private void InitSaveData(WeaponCharacteristics startWeapon)
        {
            TypeClass = typeof(InventorySaveData);

            SavedData = new Dictionary<Type, Dictionary<string, object>>
            {
                {
                    typeof(BasicItemCharacteristics), new Dictionary<string, object>
                    {
                        { GUIDMainInventorySlot1, null },
                        { GUIDMainInventorySlot2, null },
                        { GUIDMainInventorySlot3, null },
                        { GUIDMainInventorySlot4, null },
                        { GUIDMainInventorySlot5, null },
                        { GUIDMainInventorySlot6, null },
                        { GUIDMainInventorySlot7, null },
                        { GUIDMainInventorySlot8, null },
                        { GUIDMainInventorySlot9, null },
                        { GUIDMainInventorySlot10, null },
                        { GUIDMainInventorySlot11, null },
                        { GUIDMainInventorySlot12, null },
                        { GUIDMainInventorySlot13, null },
                        { GUIDMainInventorySlot14, null },
                        { GUIDMainInventorySlot15, null },
                        { GUIDMainInventorySlot16, null },
                        
                        { GUIDPlayerInventorySlot1, null },
                        { GUIDPlayerInventorySlot2, null },
                        { GUIDPlayerInventorySlot3, null },
                        { GUIDPlayerInventorySlot4, null },
                        { GUIDPlayerInventorySlot5, null },
                        { GUIDPlayerInventorySlot6, null },
                        { GUIDPlayerInventorySlot7, null },
                        
                        { GUIDCraftInventorySlot1, null },
                        { GUIDCraftInventorySlot2, null },
                        { GUIDCraftInventorySlot3, null },
                        { GUIDCraftInventorySlot4, null },
                        { GUIDCraftInventorySlot5, null },
                        { GUIDCraftInventorySlot6, null },
                        { GUIDCraftInventorySlot7, null },
                        { GUIDCraftInventorySlot8, null },
                        
                        { GUIDEquipmentSlot1, startWeapon },
                        { GUIDEquipmentSlot2, null }
                    }
                },
            };
        }

        public override void RefreshDataTypes()
        {
            var temporaryMainInventoryData = new Dictionary<string, object>(InventoryData);
            
            foreach (var mainInventoryData in temporaryMainInventoryData)
            {
                var json = JsonConvert.SerializeObject(mainInventoryData.Value);
                var basicItemCharacteristics = JsonConvert.DeserializeObject<BasicItemCharacteristics>(json);
                InventoryData[mainInventoryData.Key] = GetDeserializeCharacteristics(basicItemCharacteristics, json);
            }
        }

        public override void RefreshSavedData()
        {
            foreach (var mainInventoryData in InventoryData)
            {
                var basicItemCharacteristics = (BasicItemCharacteristics)InventoryData[mainInventoryData.Key];
                SavedData[typeof(BasicItemCharacteristics)][mainInventoryData.Key] = GetCastCharacteristics(basicItemCharacteristics);
            }
        }

        public override void RefreshDataForSave()
        {
            foreach (var savedData in SavedData[typeof(BasicItemCharacteristics)])
            {
                var basicItemCharacteristics = (BasicItemCharacteristics)savedData.Value;
                InventoryData[savedData.Key] = GetCastCharacteristics(basicItemCharacteristics);
            }
        }
        
        private BasicItemCharacteristics GetCastCharacteristics(BasicItemCharacteristics basicItemCharacteristics)
        {
            if (basicItemCharacteristics == null)
                return null;
            if (basicItemCharacteristics.BasicData.ItemType == ItemType.Crystal)
                return (CrystalCharacteristics)basicItemCharacteristics;
            if (basicItemCharacteristics.BasicData.ItemType == ItemType.Heal)
                return (HealCharacteristics)basicItemCharacteristics;
            if (basicItemCharacteristics.BasicData.ItemType == ItemType.Katana ||
                basicItemCharacteristics.BasicData.ItemType == ItemType.MagicCard || 
                basicItemCharacteristics.BasicData.ItemType == ItemType.Bow)
                return (WeaponCharacteristics)basicItemCharacteristics;
            if (basicItemCharacteristics.BasicData.ItemType == ItemType.Armor)
                return (ArmorCharacteristics)basicItemCharacteristics;
            
            return null;
        }
        
        private BasicItemCharacteristics GetDeserializeCharacteristics(BasicItemCharacteristics basicItemCharacteristics, string jsonCharacteristics)
        {
            if (basicItemCharacteristics == null)
                return null;
            if (basicItemCharacteristics.BasicData.ItemType == ItemType.Crystal)
                return JsonConvert.DeserializeObject<CrystalCharacteristics>(jsonCharacteristics);
            if (basicItemCharacteristics.BasicData.ItemType == ItemType.Heal)
                return JsonConvert.DeserializeObject<HealCharacteristics>(jsonCharacteristics);
            if (basicItemCharacteristics.BasicData.ItemType == ItemType.Katana ||
                basicItemCharacteristics.BasicData.ItemType == ItemType.MagicCard ||
                basicItemCharacteristics.BasicData.ItemType == ItemType.Bow)
                return JsonConvert.DeserializeObject<WeaponCharacteristics>(jsonCharacteristics);
            if (basicItemCharacteristics.BasicData.ItemType == ItemType.Armor)
                return JsonConvert.DeserializeObject<ArmorCharacteristics>(jsonCharacteristics);
            
            return null;
        }
    }
}
