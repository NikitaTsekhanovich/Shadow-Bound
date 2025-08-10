using Factories.Properties;
using InventoriesControllers.InventoriesViews;
using InventoriesControllers.SlotsViews;
using Models.Items;
using SaveSystems;

namespace InventoriesControllers.Inventories
{
    public class InventoryMainMenu : Inventory
    {
        private readonly ICanGetItemSlotMenuView _itemSlotMenuViewFactory;
        
        public InventoryMainMenu(
            ICanGetItemSlotMenuView itemSlotMenuViewFactory,
            InventoryPlayerMenuView inventoryPlayerMenuView,
            SaveSystem saveSystem, 
            string guidSlot) : 
            base(inventoryPlayerMenuView.SlotViews,
                saveSystem,
                guidSlot)
        {
            _itemSlotMenuViewFactory = itemSlotMenuViewFactory;
            
            CreateSlots();
        }

        protected override ItemSlotView GetItemSlotView(
            ISlotView slotView, 
            BasicItemCharacteristics basicItemCharacteristics)
        {
            return _itemSlotMenuViewFactory.GetItemSlotMenuView(
                slotView.Transform,
                basicItemCharacteristics,
                (SlotCatchingItemView)slotView);
        }
    }
}
