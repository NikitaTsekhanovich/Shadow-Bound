using InventoriesControllers.ItemSlotViews;
using Models.Items;
using UnityEngine;

namespace Factories.Properties
{
    public interface ICanGetItemSlotGameView
    {
        public ItemSlotGameView GetItemSlotGameView(
            Transform transformSpawn, 
            BasicItemCharacteristics basicItemCharacteristics);
    }
}
