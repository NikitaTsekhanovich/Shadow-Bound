using InventoriesControllers.SlotsViews;
using UnityEngine;
using UnityEngine.EventSystems;

namespace InventoriesControllers.ItemSlotViews
{
    public class ItemSlotMenuView : ItemSlotView, IPointerDownHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler, IPointerUpHandler
    {
        private Transform _itemSlotBuffer;
        private CanvasGroup _canvasGroup;
        private Canvas _canvas;
        private RectTransform _rectTransform;
        private SlotCatchingItemView _slotPlayerMenuView;
        private Transform _currentItemSlotParent;
        private DescriptionItemController _descriptionItemController;
        private bool _isDragging;
        
        public void Init(
            ItemSlot itemSlot, 
            Transform parent, 
            Canvas canvas, 
            Transform itemSlotBuffer,
            DescriptionItemController descriptionItemController)
        {
            _rectTransform = GetComponent<RectTransform>();
            _canvasGroup = GetComponent<CanvasGroup>();
            
            _descriptionItemController = descriptionItemController;
            _itemSlotBuffer = itemSlotBuffer;
            _canvas = canvas;
            _currentItemSlotParent = parent;
            ItemSlot = itemSlot;
            SetIcon();
            UpdateQuantityText();
        }

        public void SetSlotView(SlotCatchingItemView slotPlayerMenuView)
        {
            _slotPlayerMenuView = slotPlayerMenuView;
        }
        
        public void OnPointerUp(PointerEventData eventData)
        {
            if (!_isDragging)
                _descriptionItemController.ShowDescription(ItemSlot.BasicItemCharacteristics.GetInfoText());
            
            _isDragging = false;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            _isDragging = true;
            _currentItemSlotParent = transform.parent;
            transform.SetParent(_itemSlotBuffer);
            _canvasGroup.alpha = 0.5f;
            _canvasGroup.blocksRaycasts = false;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (eventData.pointerEnter.TryGetComponent(out ItemSlotMenuView dragItemSlotView) &&
                CheckCanSwap(dragItemSlotView))
            {
                SwapItems(dragItemSlotView);
            }
            else if (!eventData.pointerEnter.TryGetComponent(out SlotPlayerMenuView slotPlayerMenuView) ||
                (slotPlayerMenuView != null && 
                (slotPlayerMenuView.transform.name == _currentItemSlotParent.name || 
                 slotPlayerMenuView.CheckEqualityItemSlot(this))))
            {
                UpdatePosition(_currentItemSlotParent);
            }
            
            _canvasGroup.alpha = 1f;
            _canvasGroup.blocksRaycasts = true;
        }
        public void OnDrag(PointerEventData eventData)
        {
            _rectTransform.anchoredPosition += eventData.delta / _canvas.scaleFactor;
        }

        public void OnDrop(PointerEventData eventData)
        {
            eventData.pointerDrag.TryGetComponent(out ItemSlotMenuView dragItemSlotView);
            
            if (dragItemSlotView != null && ItemSlot.TryAddItemSlot(dragItemSlotView.ItemSlot))
            {
                SumUpItems(dragItemSlotView);
            }
        }

        public void ChangeSlot(Transform slotTransform)
        {
            _slotPlayerMenuView.RemoveItemSlot();
            UpdatePosition(slotTransform);
        }
        
        private void SumUpItems(ItemSlotMenuView dragItemSlotMenuView)
        {
            UpdateQuantityText();
            dragItemSlotMenuView._slotPlayerMenuView.RemoveItemSlot();

            if (dragItemSlotMenuView.ItemSlot.Quantity != 0)
            {
                dragItemSlotMenuView.UpdateQuantityText();
                dragItemSlotMenuView.UpdatePosition(dragItemSlotMenuView._currentItemSlotParent);
            }
            else
            {
                Destroy(dragItemSlotMenuView.gameObject);
            }
        }
        
        private void SwapItems(ItemSlotMenuView dragItemSlotMenuView)
        {
            var itemSlotViewParent = dragItemSlotMenuView._currentItemSlotParent;
            dragItemSlotMenuView.SwapSlot(_currentItemSlotParent, this);
            SwapSlot(itemSlotViewParent, dragItemSlotMenuView);

            var slotPlayerMenuView = dragItemSlotMenuView._slotPlayerMenuView;
            dragItemSlotMenuView.SetSlotView(_slotPlayerMenuView);
            SetSlotView(slotPlayerMenuView);
        }

        private void SwapSlot(Transform slotTransform, ItemSlotMenuView dragItemSlotMenuView)
        {
            UpdatePosition(slotTransform);
            _slotPlayerMenuView.SetItemSlotView(dragItemSlotMenuView);
        }

        private void UpdatePosition(Transform slotTransform)
        {
            _currentItemSlotParent = slotTransform;
            transform.SetParent(slotTransform);
            
            _rectTransform.offsetMin = new Vector2(0, 0);
            _rectTransform.offsetMax = new Vector2(0, 0);
        }

        private bool CheckCanSwap(ItemSlotMenuView dragItemSlotMenuView)
        {
            var dragItemBasicData = dragItemSlotMenuView.ItemSlot.BasicItemCharacteristics.BasicData;
            var currentItemBasicData = ItemSlot.BasicItemCharacteristics.BasicData;
            
            return dragItemSlotMenuView._slotPlayerMenuView.CheckValidChangeTypeSlot(
                       currentItemBasicData.ChangeType, currentItemBasicData.ItemType) && 
                   _slotPlayerMenuView.CheckValidChangeTypeSlot(
                       dragItemBasicData.ChangeType, dragItemBasicData.ItemType);
        }
    }
}
