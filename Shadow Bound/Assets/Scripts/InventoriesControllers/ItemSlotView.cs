using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace InventoriesControllers
{
    public class ItemSlotView : MonoBehaviour
    {
        [SerializeField] private Image _itemIconImage;
        [SerializeField] private TMP_Text _quantityText;
        
        public ItemSlot ItemSlot { get; protected set; }
        
        public void UpdateQuantityText()
        {
            _quantityText.text = ItemSlot.Quantity.ToString();
        }

        protected void SetIcon()
        {
            _itemIconImage.sprite = ItemSlot.ItemIcon;
        }
    }
}
