using GameControllers.Controllers;
using GameControllers.Controllers.Properties;
using GameControllers.Models.ItemCharacteristicsData;
using UnityEngine;

namespace GameControllers.Models.Combat
{
    public class MagicBall : Projectile
    {
        [SerializeField] private ParticleSystem _particleSystem;

        public override void Init(
            ICanTakeDamage ownerTakerDamage, 
            WeaponCharacteristicsData weaponCharacteristicsData, 
            Health health,
            Vector3 direction,
            AudioSource damageSound)
        {
            _particleSystem.Play();
            base.Init(ownerTakerDamage, weaponCharacteristicsData, health, direction, damageSound);
        }

        protected override void ReturnToPool()
        {
            _particleSystem.Clear();
            base.ReturnToPool();
        }
    }
}
