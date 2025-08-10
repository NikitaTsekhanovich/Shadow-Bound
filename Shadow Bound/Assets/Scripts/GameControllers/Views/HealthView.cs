using UnityEngine;
using UnityEngine.UI;

namespace GameControllers.Views
{
    public class HealthView : MonoBehaviour
    {
        [SerializeField] private Image _healthBar;

        public void UpdateHealth(float currentHealth)
        {
            _healthBar.fillAmount = currentHealth;
        }

        public void HideBar()
        {
            gameObject.SetActive(false);
        }
    }
}
