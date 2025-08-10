using UnityEngine;

namespace GameControllers.Models
{
    public class NinjaMenuModel : MonoBehaviour
    {
        [SerializeField] private MeshRenderer _weaponRenderer;
        [SerializeField] private MeshRenderer _secondWeaponRender;
        [SerializeField] private GameObject _armor;
        [SerializeField] private MeshRenderer _armorRenderer;

        public void InitMaterials(Material weaponMaterial, Material armorMaterial)
        {
            _weaponRenderer.material = weaponMaterial;

            if (_secondWeaponRender != null)
            {
                _secondWeaponRender.material = weaponMaterial;
            }

            if (armorMaterial != null)
            {
                _armorRenderer.material = armorMaterial;
                _armor.gameObject.SetActive(true);
            }
            else
            {
                _armor.gameObject.SetActive(false);
            }
        }
    }
}
