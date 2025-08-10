using DG.Tweening;
using GameControllers.Models.ItemsEnums;
using InventoriesControllers.ItemSlotViews;
using UnityEngine;
using UnityEngine.UI;

namespace InventoriesControllers.SlotsViews
{
    public class SlotEquipmentView : SlotCatchingItemView
    {
        [SerializeField] private ItemType _availableItemType;
        [SerializeField] private Image _slotImage;

        private bool _isShowWarning;
        
        public override bool CheckValidChangeTypeSlot(ChangeType changeType, ItemType itemType)
        {
            if (_availableItemType == ItemType.Katana && 
                (itemType == ItemType.Bow || itemType == ItemType.MagicCard))
            {
                return true;
            }
            
            return itemType == _availableItemType;
        }

        public void ShowWarningVoidSlot()
        {
            if (_isShowWarning) return;
            
            _isShowWarning = true;
            
            _slotImage.transform.DOShakePosition(1f, Vector3.one);
            DOTween.Sequence()
                .Append(_slotImage.DOColor(Color.red, 0.5f))
                .Append(_slotImage.DOColor(Color.white, 0.5f))
                .AppendCallback(() => _isShowWarning = false);
        }
        
        protected override void TryCollectItemSlotView(ItemSlotMenuView dragItemSlotMenuView)
        {
            var itemType = dragItemSlotMenuView.ItemSlot.BasicItemCharacteristics.BasicData.ItemType;
            
            if (itemType == _availableItemType || 
                (_availableItemType == ItemType.Katana &&
                 (itemType == ItemType.Bow || itemType == ItemType.MagicCard)))
                base.TryCollectItemSlotView(dragItemSlotMenuView);
        }
    }
}
