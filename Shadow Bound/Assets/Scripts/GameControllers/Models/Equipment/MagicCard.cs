using Factories.Types;
using GameControllers.Controllers;
using GameControllers.Controllers.Properties;
using GameControllers.Models.Combat;
using UnityEngine;

namespace GameControllers.Models.Equipment
{
    public class MagicCard : Weapon
    {
        [SerializeField] private MagicBall _magicBallEnemyPrefab;
        [SerializeField] private MagicBall _magicBallPlayerPrefab;
        [SerializeField] private AudioSource _shootSound;
        
        private ProjectilePoolFactory _projectilePoolFactory;
        private ICanTakeDamage _ownerTakerDamage;
        private Health _ownerHealth;
        private Vector3 _shootDirection;
        
        public override void InitDamageDealer(
            ICanTakeDamage ownerTakerDamage, 
            Health health)
        {
            if (ownerTakerDamage.GameEntityType == GameEntityTypes.Player)
                _projectilePoolFactory = new ProjectilePoolFactory(_magicBallPlayerPrefab, 5);
            else
                _projectilePoolFactory = new ProjectilePoolFactory(_magicBallEnemyPrefab, 5);
            
            _ownerTakerDamage = ownerTakerDamage;
            _ownerHealth = health;
        }

        private void OnDestroy()
        {
            _projectilePoolFactory.ClearPoolEntity();
        }

        public void InitDirection(Vector3 shootDirection)
        {
            _shootDirection = shootDirection;
        }

        public void Shoot()
        {
            _shootSound.Play();
            var magicBall = _projectilePoolFactory.GetPoolEntity(transform);
            magicBall.Init(_ownerTakerDamage, WeaponCharacteristicsData, _ownerHealth, _shootDirection, AttackSound);
        }
    }
}
