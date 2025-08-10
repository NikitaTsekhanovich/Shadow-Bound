using Factories.Types;
using GameControllers.Controllers;
using GameControllers.Controllers.Properties;
using GameControllers.Models.Combat;
using GameControllers.Models.ItemCharacteristicsData;
using UnityEngine;

namespace GameControllers.Models.Equipment
{
    public class Bow : Weapon
    {
        [SerializeField] private Arrow _arrowPrefab;
        [SerializeField] private MeshRenderer _bowstringMeshRenderer;
        [SerializeField] private Transform _arrowSpawnPoint;
        [SerializeField] private AudioSource _shootSound;
        
        private ProjectilePoolFactory _projectilePoolFactory;
        private ICanTakeDamage _ownerTakerDamage;
        private Health _ownerHealth;
        private Material _arrowMaterial;
        private Vector3 _shootDirection;
        
        public override void InitDamageDealer(
            ICanTakeDamage ownerTakerDamage, 
            Health health)
        {
            _projectilePoolFactory = new ProjectilePoolFactory(_arrowPrefab, 5);
            _ownerTakerDamage = ownerTakerDamage;
            _ownerHealth = health;
        }

        public void InitDirection(Vector3 shootDirection)
        {
            _shootDirection = shootDirection;
        }

        private void OnDestroy()
        {
            _projectilePoolFactory.ClearPoolEntity();
        }

        public void Shoot()
        {
            _shootSound.Play();
            var arrow = (Arrow)_projectilePoolFactory.GetPoolEntity(_arrowSpawnPoint);
            arrow.InitMaterial(_arrowMaterial);
            arrow.Init(_ownerTakerDamage, WeaponCharacteristicsData, _ownerHealth, _shootDirection, AttackSound);
        }

        public override void InitCharacteristics(WeaponCharacteristicsData weaponCharacteristicsData, Material material)
        {
            base.InitCharacteristics(weaponCharacteristicsData, material);
            _bowstringMeshRenderer.material = material;
            _arrowMaterial = material;
        }
    }
}
