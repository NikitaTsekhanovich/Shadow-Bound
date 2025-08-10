using GameControllers.Models.ItemsEnums;
using InventoriesControllers.ItemSlotViews;

namespace InventoriesControllers.SlotsViews
{
    public class SlotUpgradeView : SlotCatchingItemView
    {
        public override bool CheckValidChangeTypeSlot(ChangeType changeType, ItemType itemType)
        {
            return changeType == ChangeType.CraftableAndUpgradeable ||
                   changeType == ChangeType.Upgradeable;
        }

        protected override void TryCollectItemSlotView(ItemSlotMenuView dragItemSlotMenuView)
        {
            if (!CheckValidChangeTypeSlot(
                    dragItemSlotMenuView.ItemSlot.BasicItemCharacteristics.BasicData.ChangeType,
                    dragItemSlotMenuView.ItemSlot.BasicItemCharacteristics.BasicData.ItemType))
            {
                return;
            }
            
            base.TryCollectItemSlotView(dragItemSlotMenuView);
        }
    }
}
