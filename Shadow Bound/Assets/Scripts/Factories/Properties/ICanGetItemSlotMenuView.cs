using InventoriesControllers;
using InventoriesControllers.ItemSlotViews;
using Models.Items;
using UnityEngine;

namespace Factories.Properties
{
    public interface ICanGetItemSlotMenuView
    {
        public ItemSlotMenuView GetItemSlotMenuView(
            Transform transformSpawn, 
            BasicItemCharacteristics basicItemCharacteristics,
            SlotCatchingItemView catchingItemView);
    }
}
