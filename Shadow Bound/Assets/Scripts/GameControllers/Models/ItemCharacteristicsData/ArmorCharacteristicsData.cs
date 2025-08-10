using System;
using Newtonsoft.Json;
using UnityEngine;

namespace GameControllers.Models.ItemCharacteristicsData
{
    [Serializable]
    public class ArmorCharacteristicsData
    {
        [field: SerializeField] public float ExtraHealth { get; private set; }
        [field: SerializeField, Range(0f, 100f)] public float EvasionProbability { get; private set; }
        [field: SerializeField, Range(0f, 100f)] public float DamageBlock { get; private set; }
        [field: SerializeField, Range(0f, 100f)] public float ReflectedDamage { get; private set; }
        
        public ArmorCharacteristicsData(ArmorCharacteristicsData data)
        {
            ExtraHealth = data.ExtraHealth;
            EvasionProbability = data.EvasionProbability;
            DamageBlock = data.DamageBlock;
            ReflectedDamage = data.ReflectedDamage;
        }

        [JsonConstructor]
        public ArmorCharacteristicsData(
            float extraHealth,
            float evasionProbability,
            float damageBlock,
            float reflectedDamage)
        {
            ExtraHealth = extraHealth;
            EvasionProbability = evasionProbability;
            DamageBlock = damageBlock;
            ReflectedDamage = reflectedDamage;
        }
        
        public void ImproveCharacteristics(
            float improvedExtraHealth,
            float improvedEvasionProbability,
            float improvedDamageBlock,
            float improvedReflectedDamage)
        {
            ExtraHealth += improvedExtraHealth;
            EvasionProbability += improvedEvasionProbability;
            DamageBlock += improvedDamageBlock;
            ReflectedDamage += improvedReflectedDamage;
        }
    }
}
