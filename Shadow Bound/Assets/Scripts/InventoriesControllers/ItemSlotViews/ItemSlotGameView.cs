using System;
using GameControllers.Controllers.Properties;
using GameControllers.Models;
using UnityEngine.EventSystems;

namespace InventoriesControllers.ItemSlotViews
{
    public class ItemSlotGameView : ItemSlotView, IPointerDownHandler
    {
        private IUsableItem _usableItem;
        private Action _clearCurrentItemSlotGameView;

        public void Init(ItemSlot itemSlot)
        {
            ItemSlot = itemSlot;
            _usableItem = ItemSlot as IUsableItem;
            SetIcon();
            UpdateQuantityText();
        }

        public void InitUnitUsingItem(Ninja ninja)
        {
            _usableItem?.InitUnitUsingItem(ninja);
        }

        public void InitClearDelegate(Action clearCurrentItemSlotGameView)
        {
            _clearCurrentItemSlotGameView = clearCurrentItemSlotGameView;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            UseItem();
        }

        public bool TrySumItems(ItemSlot newItemSlot)
        {
            if (ItemSlot != null && ItemSlot.TryAddItemSlot(newItemSlot))
            {
                UpdateQuantityText();
                return newItemSlot.Quantity <= 0;
            }
            
            return false;
        }

        private void UseItem()
        {
            if (_usableItem == null) return;
            
            _usableItem.Use();

            if (ItemSlot.Quantity <= 0)
            {
                _clearCurrentItemSlotGameView?.Invoke();
                Destroy(gameObject);
            }
            else
            {
                UpdateQuantityText();
            }
        }
    }
}
