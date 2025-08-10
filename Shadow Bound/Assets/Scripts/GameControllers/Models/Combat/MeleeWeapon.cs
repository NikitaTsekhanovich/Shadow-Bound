using GameControllers.Controllers;
using GameControllers.Controllers.Properties;
using GameControllers.Models.ItemCharacteristicsData;
using UnityEngine;

namespace GameControllers.Models.Combat
{
    public class MeleeWeapon : IDealDamage
    {
        private readonly DamageDealer _damageDealer;

        public MeleeWeapon(
            ICanTakeDamage ownerTakerDamage, 
            WeaponCharacteristicsData weaponCharacteristicsData,
            Health health,
            AudioSource damageSound)
        {
            _damageDealer = new DamageDealer(
                ownerTakerDamage, 
                weaponCharacteristicsData,
                health,
                damageSound);
        }
        
        public void DealDamage(ICanTakeDamage takerDamage)
        {
            _damageDealer.DealDamage(takerDamage);
        }
    }
}
