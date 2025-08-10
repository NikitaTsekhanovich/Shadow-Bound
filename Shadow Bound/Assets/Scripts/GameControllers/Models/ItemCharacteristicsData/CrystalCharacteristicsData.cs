using System;
using Newtonsoft.Json;
using UnityEngine;

namespace GameControllers.Models.ItemCharacteristicsData
{
    [Serializable]
    public class CrystalCharacteristicsData 
    {
        [field: Header("Weapon")]
        [field: SerializeField] public float ImprovementDamage { get; private set; }
        [field: SerializeField] public float ImprovementCriticalHitChance { get; private set; }
        [field: SerializeField] public float ImprovementCriticalDamage { get; private set; }
        [field: SerializeField] public float ImprovementPercentageVampirism { get; private set; }
        
        [field: Header("Armor")]
        [field: SerializeField] public float ImprovementExtraHealth { get; private set; }
        [field: SerializeField] public float ImprovementEvasionProbability { get; private set; }
        [field: SerializeField] public float ImprovementDamageBlock { get; private set; }
        [field: SerializeField] public float ImprovementReflectedDamage { get; private set; }
        
        public CrystalCharacteristicsData(CrystalCharacteristicsData data)
        {
            ImprovementDamage = data.ImprovementDamage;
            ImprovementCriticalHitChance = data.ImprovementCriticalHitChance;
            ImprovementCriticalDamage = data.ImprovementCriticalDamage;
            ImprovementPercentageVampirism = data.ImprovementPercentageVampirism;
            ImprovementExtraHealth = data.ImprovementExtraHealth;
            ImprovementEvasionProbability = data.ImprovementEvasionProbability;
            ImprovementDamageBlock = data.ImprovementDamageBlock;
            ImprovementReflectedDamage = data.ImprovementReflectedDamage;
        }
        
        [JsonConstructor]
        public CrystalCharacteristicsData(
            float improvementDamage,
            float improvementCriticalHitChance,
            float improvementCriticalDamage,
            float improvementPercentageVampirism,
            float improvementExtraHealth,
            float improvementEvasionProbability,
            float improvementDamageBlock,
            float improvementReflectedDamage)
        {
            ImprovementDamage = improvementDamage;
            ImprovementCriticalHitChance = improvementCriticalHitChance;
            ImprovementCriticalDamage = improvementCriticalDamage;
            ImprovementPercentageVampirism = improvementPercentageVampirism;
            ImprovementExtraHealth = improvementExtraHealth;
            ImprovementEvasionProbability = improvementEvasionProbability;
            ImprovementDamageBlock = improvementDamageBlock;
            ImprovementReflectedDamage = improvementReflectedDamage;
        }
    }
}
