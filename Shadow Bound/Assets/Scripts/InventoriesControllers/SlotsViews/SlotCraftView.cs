using Factories.Properties;
using GameControllers.Models.ItemsEnums;
using InventoriesControllers.ItemSlotViews;
using Models.Items;
using Zenject;

namespace InventoriesControllers.SlotsViews
{
    public class SlotCraftView : SlotCatchingItemView
    {
        [Inject] private ICanGetItemSlotMenuView _itemSlotMenuViewFactory;

        public override bool CheckValidChangeTypeSlot(ChangeType changeType, ItemType itemType)
        {
            return changeType == ChangeType.Craftable ||
                   changeType == ChangeType.CraftableAndUpgradeable;
        }

        public void DecreaseItemSlotQuantity(int decreaseAmount)
        {
            CurrentItemSlotMenuView.ItemSlot.BasicItemCharacteristics.CalculateQuantity(decreaseAmount);
            CurrentItemSlotMenuView.UpdateQuantityText();

            if (CurrentItemSlotMenuView.ItemSlot.Quantity <= 0)
            {
                Destroy(CurrentItemSlotMenuView.gameObject);
                RemoveItemSlot();
            }
        }
        
        protected override void TryCollectItemSlotView(ItemSlotMenuView dragItemSlotMenuView)
        {
            var dragItemCharacteristics = dragItemSlotMenuView.ItemSlot.BasicItemCharacteristics;
            
            if (!CheckValidChangeTypeSlot(
                    dragItemCharacteristics.BasicData.ChangeType, 
                    dragItemCharacteristics.BasicData.ItemType))
            {
                return;
            }
            
            if (dragItemCharacteristics.Quantity != 1)
            {
                dragItemCharacteristics.CalculateQuantity(-1);
                dragItemSlotMenuView.UpdateQuantityText();
                
                var duplicateCharacteristics = dragItemCharacteristics.CloneCharacteristics();
                duplicateCharacteristics.UpdateQuantity(1);
                
                CreateItemSlotView(duplicateCharacteristics);
            }
            else
            {
                base.TryCollectItemSlotView(dragItemSlotMenuView);
            }
        }

        private void CreateItemSlotView(BasicItemCharacteristics basicItemCharacteristics)
        {
            var itemSlotView = _itemSlotMenuViewFactory.GetItemSlotMenuView(
                transform,
                basicItemCharacteristics,
                this);
            Init(itemSlotView);
        }
    }
}
