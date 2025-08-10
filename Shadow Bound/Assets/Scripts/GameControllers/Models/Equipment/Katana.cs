using GameControllers.Controllers;
using GameControllers.Controllers.Properties;
using GameControllers.Models.Combat;
using UnityEngine;

namespace GameControllers.Models.Equipment
{
    public class Katana : Weapon
    {
        [SerializeField] private DamageTakerCollision _damageTakerCollision;
        
        private void OnDestroy()
        {
            _damageTakerCollision.OnDamageTakerCollision -= DamageDealer.DealDamage;
        }

        public override void InitDamageDealer(
            ICanTakeDamage ownerTakerDamage, 
            Health health)
        {
            DamageDealer = new MeleeWeapon(
                ownerTakerDamage,
                WeaponCharacteristicsData,
                health,
                AttackSound);
            
            _damageTakerCollision.OnDamageTakerCollision += DamageDealer.DealDamage;
        }
    }
}
