using Factories.Properties;
using GameControllers.Models.ItemsEnums;
using InventoriesControllers.ItemSlotViews;
using Models.Items;
using Zenject;

namespace InventoriesControllers.SlotsViews
{
    public class SlotEndCraftView : SlotCatchingItemView
    {
        [Inject] private ICanGetItemSlotMenuView _itemSlotMenuViewFactory;
        
        public override bool CheckValidChangeTypeSlot(ChangeType changeType, ItemType itemType)
        {
            return false;
        }

        protected override void TryCollectItemSlotView(ItemSlotMenuView dragItemSlotMenuView)
        {
            
        }

        public void CreateItemSlotView(BasicItemCharacteristics basicItemCharacteristics)
        {
            var itemSlotView = _itemSlotMenuViewFactory.GetItemSlotMenuView(transform, basicItemCharacteristics, this);
            Init(itemSlotView);
        }

        public void UpdateQuantityText()
        {
            CurrentItemSlotMenuView.UpdateQuantityText();
        }
    }
}
