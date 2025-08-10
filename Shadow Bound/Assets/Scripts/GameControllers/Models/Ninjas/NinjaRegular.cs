using UnityEngine;

namespace GameControllers.Models.Ninjas
{
    public class NinjaRegular : Ninja
    {
        [Header("Model components")]
        [SerializeField] private Transform _backSlot;
        [SerializeField] private Transform _rightHandSlot;
        [SerializeField] private Transform _generalSlot;

        public override void SetWeapon(Weapon weapon)
        {
            base.SetWeapon(weapon);
            Weapon.transform.localScale = Vector3.one;
            Weapon.transform.localRotation = Quaternion.identity;
        }

        public void PlaceWeaponRightHand()
        {
            Weapon?.transform.SetParent(_rightHandSlot, true);
        }
        
        public void PlaceWeaponGeneralSlot()
        {
            Weapon?.transform.SetParent(_generalSlot, true);
        }
        
        public void ReturnKatana()
        {
            Weapon.transform.SetParent(_backSlot);
            Weapon.transform.localPosition = Vector3.zero;
            Weapon.transform.localRotation = Quaternion.identity;
        }
    }
}
