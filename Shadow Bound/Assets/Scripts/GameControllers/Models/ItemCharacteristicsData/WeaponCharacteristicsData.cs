using System;
using Newtonsoft.Json;
using UnityEngine;

namespace GameControllers.Models.ItemCharacteristicsData
{
    [Serializable]
    public class WeaponCharacteristicsData
    {
        [field: SerializeField] public float Damage { get; private set; }
        [field: SerializeField, Range(0f, 100f)] public float CriticalHitChance { get; private set; }
        [field: SerializeField] public float CriticalDamage { get; private set; }
        [field: SerializeField] public float PercentageVampirism { get; private set; }

        public WeaponCharacteristicsData(WeaponCharacteristicsData data)
        {
            Damage = data.Damage;
            CriticalHitChance = data.CriticalHitChance;
            CriticalDamage = data.CriticalDamage;
            PercentageVampirism = data.PercentageVampirism;
        }

        [JsonConstructor]
        public WeaponCharacteristicsData(
            float damage,
            float criticalHitChance,
            float criticalDamage,
            float percentageVampirism)
        {
            Damage = damage;
            CriticalHitChance = criticalHitChance;
            CriticalDamage = criticalDamage;
            PercentageVampirism = percentageVampirism;
        }

        public void ImproveCharacteristics(
            float improvedDamage,
            float improvedCriticalHitChance,
            float improvedCriticalDamage,
            float improvedPercentageVampirism)
        {
            Damage += improvedDamage;
            CriticalHitChance += improvedCriticalHitChance;
            CriticalDamage += improvedCriticalDamage;
            PercentageVampirism += improvedPercentageVampirism;
        }
    }
}
