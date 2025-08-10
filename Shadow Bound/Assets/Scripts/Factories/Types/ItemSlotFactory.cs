using System.Collections.Generic;
using Factories.Properties;
using GameControllers.Models.Configs;
using GameControllers.Models.Configs.Items;
using GameControllers.Models.ItemsEnums;
using InventoriesControllers;
using InventoriesControllers.ItemSlots;
using Models.Items;
using Models.Items.BasicItemCharacteristicsTypes;

namespace Factories.Types
{
    public class ItemSlotFactory : ICanGetItemSlot
    {
        private readonly Dictionary<ItemType, Dictionary<LevelCharacteristicType, ItemSlotConfig>> _itemSlotConfigs = new ();
        
        public ItemSlotFactory(ItemSlotConfig[] itemSlotConfigs)
        {
            foreach (var itemSlotConfig in itemSlotConfigs)
            {
                if (_itemSlotConfigs.ContainsKey(itemSlotConfig.BasicItemCharacteristicsData.ItemType))
                {
                    _itemSlotConfigs[itemSlotConfig.BasicItemCharacteristicsData.ItemType]
                        [itemSlotConfig.BasicItemCharacteristicsData.LevelCharacteristicType] = itemSlotConfig;
                }
                else
                {
                    var levelCharacteristicTypes = new Dictionary<LevelCharacteristicType, ItemSlotConfig>
                    {
                        [itemSlotConfig.BasicItemCharacteristicsData.LevelCharacteristicType] = itemSlotConfig
                    };
                    _itemSlotConfigs[itemSlotConfig.BasicItemCharacteristicsData.ItemType] = levelCharacteristicTypes;
                }
            }
        }
        
        public ItemSlot GetItemSlot(BasicItemCharacteristics basicItemCharacteristics)
        {
            var itemSlotConfig = GetItemSlotConfig(
                basicItemCharacteristics.BasicData.ItemType, 
                basicItemCharacteristics.BasicData.LevelCharacteristicType);

            if (basicItemCharacteristics.BasicData.ItemType == ItemType.Heal)
                return new UsableItemSlot(
                    itemSlotConfig.ItemIcon,
                    (HealCharacteristics)basicItemCharacteristics);
            
            return new ItemSlot(
                itemSlotConfig.ItemIcon, 
                basicItemCharacteristics);
        }

        public BasicItemCharacteristics GetNextLevelCharacteristics(
            ItemType itemType, 
            LevelCharacteristicType levelCharacteristicType,
            int quantity = 1)
        {
            var nextLevel = levelCharacteristicType + 1;
            
            if (System.Enum.IsDefined(typeof(LevelCharacteristicType), nextLevel) &&
                _itemSlotConfigs[itemType].ContainsKey(nextLevel))
            {
                return GetCharacteristics(itemType, nextLevel, quantity);
            }

            return null;
        }

        public BasicItemCharacteristics GetCharacteristics(
            ItemType itemType, 
            LevelCharacteristicType levelCharacteristicType,
            int quantity = 1)
        {
            if (itemType == ItemType.Crystal)
            {
                var crystalConfig = _itemSlotConfigs[itemType][levelCharacteristicType] as CrystalConfig;
                return new CrystalCharacteristics(
                    crystalConfig.CrystalCharacteristicsData,
                    crystalConfig.BasicItemCharacteristicsData,
                    quantity);
            }
            if (itemType == ItemType.Katana || 
                itemType == ItemType.MagicCard || 
                itemType == ItemType.Bow)
            {
                var weaponConfig = _itemSlotConfigs[itemType][levelCharacteristicType] as WeaponConfig;
                return new WeaponCharacteristics(
                    weaponConfig.WeaponCharacteristicsData,
                    weaponConfig.BasicItemCharacteristicsData,
                    quantity);
            }
            if (itemType == ItemType.Armor)
            {
                var armorConfig = _itemSlotConfigs[itemType][levelCharacteristicType] as ArmorConfig;
                return new ArmorCharacteristics(
                    armorConfig.ArmorCharacteristicsData,
                    armorConfig.BasicItemCharacteristicsData,
                    quantity);
            }

            if (itemType == ItemType.Heal)
            {
                var healConfig = _itemSlotConfigs[itemType][levelCharacteristicType] as HealConfig;
                return new HealCharacteristics(
                    healConfig.HealCharacteristicsData,
                    healConfig.BasicItemCharacteristicsData,
                    quantity);
            }

            return null;
        }

        public ItemSlotConfig GetItemSlotConfig(
            ItemType itemType, 
            LevelCharacteristicType levelCharacteristicType)
        {
            return _itemSlotConfigs[itemType][levelCharacteristicType];
        }
    }
}
