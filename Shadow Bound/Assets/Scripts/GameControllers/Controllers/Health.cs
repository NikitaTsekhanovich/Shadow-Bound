using System;
using System.Globalization;
using GameControllers.Controllers.Properties;
using GameControllers.Models;
using GameControllers.Models.Equipment;
using GameControllers.Models.ItemCharacteristicsData;
using GameControllers.Views;
using UnityEngine;

namespace GameControllers.Controllers
{
    public class Health
    {
        private readonly HealthView _healthView;
        private readonly float _maxHealth;
        private readonly Action _died;
        private readonly ICanGetPoolEntity<StatusAttackText> _statusTextPool;
        private readonly Transform _transform;
        private readonly ArmorCharacteristicsData _armorCharacteristicsData;
        private readonly AudioSource _takeDamageSound;
        
        private float _currentHealth;
        
        public bool IsDead { get; private set; }
        
        public Health(
            HealthView healthView,
            float maxHealth,
            Action died, 
            ICanGetPoolEntity<StatusAttackText> statusTextPool,
            Transform transform,
            AudioSource takeDamageSound,
            Armor armor = null)
        {
            _healthView = healthView;
            _statusTextPool = statusTextPool;
            _transform = transform;
            _takeDamageSound = takeDamageSound;

            if (armor != null)
            {
                _armorCharacteristicsData = armor.ArmorCharacteristicsData;
                maxHealth += _armorCharacteristicsData.ExtraHealth;
            }
            
            _maxHealth = maxHealth;
            _died = died;
            
            _currentHealth = _maxHealth;
        }

        public void TakeDamage(float damage, ICanTakeDamage healthAttacker, bool isCriticalDamage)
        {
            if (IsDead) return;
            
            if (_armorCharacteristicsData != null)
            {
                if (CheckEvasionProbability())
                {
                    ShowDamageText("Miss!", false, _transform);
                    return;
                }

                CheckReflectedDamage(damage, healthAttacker);
                damage = BlockDamage(damage);
            }

            _takeDamageSound.Play();
            CalculateCurrentHealth(damage, isCriticalDamage);
        }

        public void Heal(float heal)
        {
            if (_currentHealth + heal >= _maxHealth)
            {
                _currentHealth = _maxHealth;
            }
            else
            {
                _currentHealth += heal;
            }
            
            ShowDamageText(heal.ToString(CultureInfo.InvariantCulture), false, _transform, true);
            _healthView.UpdateHealth(_currentHealth / _maxHealth);
        }

        private void CalculateCurrentHealth(float currentDamage, bool isCriticalDamage)
        {
            _currentHealth -= currentDamage;

            if (_currentHealth <= 0)
            {
                IsDead = true;
                _currentHealth = 0;
                _died?.Invoke();
            }
            
            ShowDamageText(currentDamage.ToString(CultureInfo.InvariantCulture), isCriticalDamage, _transform);
            _healthView.UpdateHealth(_currentHealth / _maxHealth);
        }

        private bool CheckEvasionProbability()
        {
            if (_armorCharacteristicsData.EvasionProbability >= 100f)
            {
                Debug.LogWarning($"Too high probability of evasion: {_armorCharacteristicsData.EvasionProbability}");
            }
            
            var chance = UnityEngine.Random.Range(0f, 100f);
            return chance <= _armorCharacteristicsData.EvasionProbability;
        }

        private float BlockDamage(float damage)
        {
            var damageBlock = _armorCharacteristicsData.DamageBlock;
            
            if (damageBlock >= 100f)
            {
                Debug.LogWarning($"Too high damage block level: {damageBlock}");
                damageBlock = 99f;
            }
            
            return damage - damage * (damageBlock / 100f);
        }

        private void CheckReflectedDamage(float damage, ICanTakeDamage healthAttacker)
        {
            var chance = UnityEngine.Random.Range(0f, 100f);
            
            if (chance <= _armorCharacteristicsData.ReflectedDamage)
            {
                ShowDamageText(damage.ToString(CultureInfo.InvariantCulture), false, healthAttacker.Transform);
                healthAttacker.Health.CalculateCurrentHealth(damage, false);
            }
        }
        
        private void ShowDamageText(string status, bool isCritical, Transform targetTransform, bool isHeal = false)
        {
            var damageText = _statusTextPool.GetPoolEntity(targetTransform);
            damageText.InitText(status, isCritical, isHeal);
        }
    }
}
