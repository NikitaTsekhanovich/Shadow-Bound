using GameControllers.Models.Equipment;
using UnityEngine;

namespace GameControllers.Models.Ninjas
{
    public class NinjaArcher : Ninja
    {
        [Header("Model components")]
        [SerializeField] private Transform _leftHandSlot;
        [SerializeField] private Transform _backSlot;
        
        private Bow _bow;

        public void Shoot()
        {
            _bow.Shoot();
        }

        public override void SetWeapon(Weapon weapon)
        {
            base.SetWeapon(weapon);
            Weapon.transform.localRotation = Quaternion.identity;
            Weapon.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
            _bow = (Bow)Weapon;
            _bow.InitDirection(Vector3.right);
        }

        public void TakeWeaponLeftHand()
        {
            Weapon.transform.SetParent(_leftHandSlot, true);
            Weapon.transform.localPosition = Vector3.zero;
            Weapon.transform.localRotation = Quaternion.identity;
            Weapon.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
        }
        
        public void ReturnBow()
        {
            Weapon.transform.SetParent(_backSlot);
            Weapon.transform.localPosition = Vector3.zero;
            Weapon.transform.localRotation = Quaternion.identity;
            Weapon.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
        }
    }
}
