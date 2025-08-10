using System;
using Newtonsoft.Json;
using UnityEngine;

namespace GameControllers.Models.ItemCharacteristicsData
{
    [Serializable]
    public class HealCharacteristicsData 
    {
        [field: SerializeField] public float HealValue { get; private set; }

        public HealCharacteristicsData(HealCharacteristicsData data)
        {
            HealValue = data.HealValue;
        }
        
        [JsonConstructor]
        public HealCharacteristicsData(float healValue)
        {
            HealValue = healValue;
        }
    }
}
