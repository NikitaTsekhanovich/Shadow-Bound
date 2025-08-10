using UnityEngine;

namespace InventoriesControllers.SlotsViews
{
    public interface ISlotView
    {
        public Transform Transform { get; }
        public void Init(ItemSlotView itemSlotView);
        public ItemSlot GetCurrentItemSlot();
    }
}
