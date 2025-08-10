using GameControllers.Models.ItemsEnums;
using InventoriesControllers.ItemSlotViews;
using InventoriesControllers.SlotsViews;
using UnityEngine;
using UnityEngine.EventSystems;

namespace InventoriesControllers
{
    public abstract class SlotCatchingItemView : MonoBehaviour, ISlotView, IDropHandler
    {
        protected ItemSlotMenuView CurrentItemSlotMenuView;
        
        public Transform Transform => transform;
        
        public void Init(ItemSlotView itemSlotView)
        {
            CurrentItemSlotMenuView = (ItemSlotMenuView)itemSlotView;
        }
        
        public void OnDrop(PointerEventData eventData)
        {
            eventData.pointerDrag.TryGetComponent(out ItemSlotMenuView dragItemSlotView);
            if (dragItemSlotView != null && CurrentItemSlotMenuView == null)
                TryCollectItemSlotView(dragItemSlotView);
        }
        
        public bool CheckEqualityItemSlot(ItemSlotMenuView itemSlotMenuView)
        {
            return CurrentItemSlotMenuView != itemSlotMenuView;
        }
        
        public void SetItemSlotView(ItemSlotMenuView itemSlotMenuView)
        {
            CurrentItemSlotMenuView = itemSlotMenuView;
        }

        public ItemSlot GetCurrentItemSlot()
        {
            return CurrentItemSlotMenuView?.ItemSlot;
        }
        
        public void RemoveItemSlot()
        {
            CurrentItemSlotMenuView = null;
        }

        public virtual bool CheckValidChangeTypeSlot(ChangeType changeType, ItemType itemType)
        {
            return true;
        }

        protected virtual void TryCollectItemSlotView(ItemSlotMenuView dragItemSlotMenuView)
        {
            dragItemSlotMenuView.ChangeSlot(transform);
            SetItemSlotView(dragItemSlotMenuView);
            CurrentItemSlotMenuView.SetSlotView(this);
        }
    }
}
