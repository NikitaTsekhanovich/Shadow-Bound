using GameControllers.Models.Equipment;
using UnityEngine;

namespace GameControllers.Models.Ninjas
{
    public class NinjaMage : Ninja
    {
        private MagicCard _magicCard;

        public override void SetWeapon(Weapon weapon)
        {
            base.SetWeapon(weapon);
            Weapon.transform.localRotation = Quaternion.Euler(-90, 0, 90);
            _magicCard = (MagicCard)Weapon;
            _magicCard.InitDirection(Vector3.right);
        }

        public void Shoot()
        {
            _magicCard.Shoot();
        }
    }
}
