using GameControllers.Controllers;
using GameControllers.Controllers.Properties;
using GameControllers.Models.ItemCharacteristicsData;
using UnityEngine;

namespace GameControllers.Models
{
    public abstract class Weapon : MonoBehaviour
    {
        [SerializeField] private MeshRenderer _meshRenderer;
        
        [SerializeField] protected AudioSource AttackSound;

        protected WeaponCharacteristicsData WeaponCharacteristicsData;
        protected IDealDamage DamageDealer;
        
        public abstract void InitDamageDealer(
            ICanTakeDamage ownerTakerDamage,
            Health health);
        
        public virtual void InitCharacteristics(
            WeaponCharacteristicsData weaponCharacteristicsData, 
            Material material)
        {
            WeaponCharacteristicsData = weaponCharacteristicsData;
            _meshRenderer.material = material;
        }
    }
}
