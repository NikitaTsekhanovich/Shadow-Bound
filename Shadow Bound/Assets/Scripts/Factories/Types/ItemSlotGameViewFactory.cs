using Factories.Properties;
using InventoriesControllers.ItemSlotViews;
using Models.Items;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Factories.Types
{
    public class ItemSlotGameViewFactory : ICanGetItemSlotGameView
    {
        private readonly ItemSlotGameView _itemSlotGameViewPrefab;
        private readonly ICanGetItemSlot _itemSlotFactory;

        public ItemSlotGameViewFactory(
            ItemSlotGameView itemSlotGameViewPrefab,
            ICanGetItemSlot itemSlotFactory)
        {
            _itemSlotGameViewPrefab = itemSlotGameViewPrefab;
            _itemSlotFactory = itemSlotFactory;
        }
        
        public ItemSlotGameView GetItemSlotGameView(
            Transform transformSpawn,
            BasicItemCharacteristics basicItemCharacteristics)
        {
            var itemSlot = _itemSlotFactory.GetItemSlot(basicItemCharacteristics);
            var itemSlotGameView =
                Object.Instantiate(_itemSlotGameViewPrefab, transformSpawn.position, Quaternion.identity, transformSpawn);
            itemSlotGameView.Init(itemSlot);

            return itemSlotGameView;
        }
    }
}
