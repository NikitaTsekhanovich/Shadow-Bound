using Factories.Properties;
using InventoriesControllers;
using InventoriesControllers.ItemSlotViews;
using Models.Items;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Factories.Types
{
    public class ItemSlotMenuViewFactory : ICanGetItemSlotMenuView
    {
        private readonly ItemSlotMenuView _itemSlotMenuViewPrefab;
        private readonly Canvas _canvas;
        private readonly Transform _itemSlotBuffer;
        private readonly ICanGetItemSlot _itemSlotFactory;
        private readonly DescriptionItemController _descriptionItemController;
        
        public ItemSlotMenuViewFactory(
            ItemSlotMenuView itemSlotMenuViewPrefab,
            Canvas canvas,
            Transform itemSlotBuffer,
            ICanGetItemSlot itemSlotFactory,
            DescriptionItemController descriptionItemController)
        {
            _itemSlotMenuViewPrefab = itemSlotMenuViewPrefab;
            _canvas = canvas;
            _itemSlotBuffer = itemSlotBuffer;
            _itemSlotFactory = itemSlotFactory;
            _descriptionItemController = descriptionItemController;
        }
        
        public ItemSlotMenuView GetItemSlotMenuView(
            Transform transformSpawn,
            BasicItemCharacteristics basicItemCharacteristics, 
            SlotCatchingItemView catchingItemView)
        {
            var itemSlot = _itemSlotFactory.GetItemSlot(basicItemCharacteristics);
            var itemSlotMenuView = 
                Object.Instantiate(_itemSlotMenuViewPrefab, transformSpawn.position, Quaternion.identity, transformSpawn);
            itemSlotMenuView.Init(itemSlot, transformSpawn, _canvas, _itemSlotBuffer, _descriptionItemController);
            itemSlotMenuView.SetSlotView(catchingItemView);

            return itemSlotMenuView;
        }
    }
}
