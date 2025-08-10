using Factories.Properties;
using GameControllers.Models;
using InventoriesControllers.ItemSlotViews;
using Models.Items;
using UnityEngine;
using Zenject;

namespace InventoriesControllers.SlotsViews
{
    public class SlotGameView : MonoBehaviour, ISlotView
    {
        [Inject] private ICanGetItemSlotGameView _itemSlotGameViewFactory;
        
        private ItemSlotGameView _currentItemSlotGameView;
        
        public Transform Transform => transform;

        public void Init(ItemSlotView itemSlotView)
        {
            _currentItemSlotGameView = (ItemSlotGameView)itemSlotView;
            _currentItemSlotGameView?.InitClearDelegate(ClearCurrentItemSlotGameView);
        }
        
        public bool TryCollectItemSlotView(BasicItemCharacteristics basicItemCharacteristics, Ninja ninja)
        {
            if (_currentItemSlotGameView?.ItemSlot == null)
            {
                var itemSlotView = _itemSlotGameViewFactory.GetItemSlotGameView(transform, basicItemCharacteristics);
                Init(itemSlotView);
                itemSlotView.InitUnitUsingItem(ninja);
                
                return true;
            }

            return false;
        }

        public bool TrySumItems(ItemSlot itemSlot)
        {
            return _currentItemSlotGameView != null && 
                   _currentItemSlotGameView.TrySumItems(itemSlot);
        }

        public ItemSlot GetCurrentItemSlot()
        {
            return _currentItemSlotGameView?.ItemSlot;
        }

        private void ClearCurrentItemSlotGameView()
        {
            _currentItemSlotGameView = null;
        }
    }
}
