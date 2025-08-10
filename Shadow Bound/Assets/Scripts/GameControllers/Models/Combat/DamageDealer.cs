using GameControllers.Controllers;
using GameControllers.Controllers.Properties;
using GameControllers.Models.ItemCharacteristicsData;
using UnityEngine;

namespace GameControllers.Models.Combat
{
    public class DamageDealer
    {
        private readonly ICanTakeDamage _ownerTakerDamage;
        private readonly WeaponCharacteristicsData _weaponCharacteristicsData;
        private readonly Health _health;
        private readonly AudioSource _damageSound;
        
        public DamageDealer(
            ICanTakeDamage ownerTakerDamage, 
            WeaponCharacteristicsData weaponCharacteristicsData,
            Health health,
            AudioSource damageSound)
        {
            _ownerTakerDamage = ownerTakerDamage;
            _weaponCharacteristicsData = weaponCharacteristicsData;
            _health = health;
            _damageSound = damageSound;
        }
        
        public void DealDamage(ICanTakeDamage takerDamage)
        {
            if (_ownerTakerDamage != null && 
                _ownerTakerDamage != takerDamage &&
                _ownerTakerDamage.GameEntityType != takerDamage.GameEntityType &&
                !_health.IsDead)
            {
                var currentDamage = _weaponCharacteristicsData.Damage;
                
                var isCriticalDamage = TryDealCriticalDamage(out var criticalDamage, currentDamage);
                currentDamage += criticalDamage;
                GetVampirism(currentDamage);
                
                _damageSound.Play();
                takerDamage.TakeDamage(currentDamage, _ownerTakerDamage, isCriticalDamage);
            }
        }

        private bool TryDealCriticalDamage(out float criticalValue, float currentDamage)
        {
            var randomValue = Random.Range(0f, 100f);

            if (randomValue <= _weaponCharacteristicsData.CriticalHitChance)
            {
                criticalValue = currentDamage * _weaponCharacteristicsData.CriticalDamage / 100f;
                return true;
            }

            criticalValue = 0;
            return false;
        }

        private void GetVampirism(float currentDamage)
        {
            if (_weaponCharacteristicsData.PercentageVampirism == 0) return;
            
            var vampirism = currentDamage * _weaponCharacteristicsData.PercentageVampirism / 100f;
            _health.Heal(vampirism);
        }
    }
}
