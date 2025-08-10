using GameControllers.Models.Configs;
using GameControllers.Models.ItemsEnums;
using InventoriesControllers;
using Models.Items;

namespace Factories.Properties
{
    public interface ICanGetItemSlot
    {
        public ItemSlot GetItemSlot(BasicItemCharacteristics basicItemCharacteristics);

        public BasicItemCharacteristics GetNextLevelCharacteristics(
            ItemType itemType,
            LevelCharacteristicType levelCharacteristicType,
            int quantity = 1);

        public ItemSlotConfig GetItemSlotConfig(
            ItemType itemType,
            LevelCharacteristicType levelCharacteristicType);

        public BasicItemCharacteristics GetCharacteristics(
            ItemType itemType,
            LevelCharacteristicType levelCharacteristicType,
            int quantity = 1);
    }
}
