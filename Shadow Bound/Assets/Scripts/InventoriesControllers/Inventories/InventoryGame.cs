using Factories.Properties;
using GameControllers.Controllers.Properties;
using GameControllers.Models;
using InventoriesControllers.InventoriesViews;
using InventoriesControllers.SlotsViews;
using Models.Items;
using SaveSystems;

namespace InventoriesControllers.Inventories
{
    public class InventoryGame : Inventory
    {
        private readonly ICanGetItemSlotGameView _itemSlotGameViewFactory;
        private readonly InventoryGameView _inventoryGameView;
        private readonly Ninja _ninja;
        
        public InventoryGame(
            ICanGetItemSlotGameView itemSlotGameViewFactory,
            InventoryGameView inventoryGameView,
            SaveSystem saveSystem,
            string guidSlot,
            Ninja ninja) :
            base(inventoryGameView.SlotViews,
                saveSystem, 
                guidSlot)
        {
            _itemSlotGameViewFactory = itemSlotGameViewFactory;
            _inventoryGameView = inventoryGameView;
            _ninja = ninja;
            
            CreateSlots();
        }
        
        protected override ItemSlotView GetItemSlotView(
            ISlotView slotView, 
            BasicItemCharacteristics basicItemCharacteristics)
        {
            var itemSlotView = _itemSlotGameViewFactory.GetItemSlotGameView(slotView.Transform, basicItemCharacteristics);
            itemSlotView.InitUnitUsingItem(_ninja);
            return itemSlotView;
        }

        public bool TryCollectItem(ICollectibleItem collectibleItem)
        {
            foreach (var slot in _inventoryGameView.SlotViews)
            {
                if (slot.TrySumItems(collectibleItem.ItemSlot))
                    return true;
            }

            foreach (var slot in _inventoryGameView.SlotViews)
            {
                if (slot.TryCollectItemSlotView(collectibleItem.ItemSlot.BasicItemCharacteristics, _ninja))
                    return true;
            }
            
            return false;
        }
    }
}
