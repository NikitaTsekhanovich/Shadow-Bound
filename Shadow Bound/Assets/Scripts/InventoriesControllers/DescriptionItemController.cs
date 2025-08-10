using TMPro;
using UnityEngine;

namespace InventoriesControllers
{
    public class DescriptionItemController : MonoBehaviour
    {
        [SerializeField] private TMP_Text _descriptionText;

        public void ShowDescription(string description)
        {
            gameObject.SetActive(true);
            _descriptionText.text = description;
        }
    }
}
