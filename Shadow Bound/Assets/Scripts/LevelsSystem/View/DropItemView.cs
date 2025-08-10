using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LevelsSystem.View
{
    public class DropItemView : MonoBehaviour
    {
        [SerializeField] private Image _itemIcon;
        [SerializeField] private TMP_Text _descriptionText;
        [SerializeField] private RectTransform _rectTransform;
        
        public const float SizeFactor = 3.88f;

        public void Init(Sprite itemSprite, string descriptionText)
        {
            _itemIcon.sprite = itemSprite;
            _descriptionText.text = descriptionText;
        }
    }
}
