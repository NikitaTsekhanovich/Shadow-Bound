using GameControllers.Models.ItemsEnums;
using Models.Items;
using UnityEngine;

namespace InventoriesControllers
{
    public class ItemSlot 
    {
        private readonly ItemType _itemType;
        private readonly LevelCharacteristicType _levelCharacteristicType;
        
        private const int MaxCapacity = 32;
        
        public readonly BasicItemCharacteristics BasicItemCharacteristics;
        
        public Sprite ItemIcon { get; private set; }
        public int Quantity => BasicItemCharacteristics.Quantity;

        public ItemSlot(Sprite itemIcon, BasicItemCharacteristics basicItemCharacteristics)
        {
            ItemIcon = itemIcon;
            _itemType = basicItemCharacteristics.BasicData.ItemType;
            _levelCharacteristicType = basicItemCharacteristics.BasicData.LevelCharacteristicType;
            BasicItemCharacteristics = basicItemCharacteristics;
        }

        public bool TryAddItemSlot(ItemSlot itemSlot)
        {
            if (itemSlot._itemType == _itemType && 
                itemSlot._levelCharacteristicType == _levelCharacteristicType &&
                _itemType != ItemType.Katana &&
                _itemType != ItemType.Armor &&
                _itemType != ItemType.MagicCard &&
                _itemType != ItemType.Bow &&
                Quantity != MaxCapacity && 
                itemSlot.Quantity != MaxCapacity)
            {
                var sumQuantity = Quantity + itemSlot.Quantity;
                CheckSumOverflow(itemSlot, sumQuantity);
                
                return true;
            }
            
            return false;
        }

        public bool TryIncreaseQuantity(int quantity)
        {
            if (Quantity + quantity <= MaxCapacity)
            {
                BasicItemCharacteristics.CalculateQuantity(quantity);
                return true;
            }
            
            return false;
        }

        private void CheckSumOverflow(ItemSlot itemSlot, int sumQuantity)
        {
            if (sumQuantity > MaxCapacity)
            {
                var secondItemQuantity = sumQuantity % MaxCapacity;
                BasicItemCharacteristics.UpdateQuantity(MaxCapacity);
                itemSlot.BasicItemCharacteristics.UpdateQuantity(secondItemQuantity);
            }
            else
            {
                BasicItemCharacteristics.UpdateQuantity(sumQuantity);
                itemSlot.BasicItemCharacteristics.UpdateQuantity(0);
            }
        }
    }
}
