using System;
using GameControllers.Models.ItemsEnums;
using Newtonsoft.Json;
using UnityEngine;

namespace GameControllers.Models.ItemCharacteristicsData
{
    [Serializable]
    public class BasicItemCharacteristicsData 
    {
        [field: SerializeField] public LevelCharacteristicType LevelCharacteristicType { get; private set; }
        [field: SerializeField] public ItemType ItemType { get; private set; }
        [field: SerializeField] public ChangeType ChangeType { get; private set; }
        [field: SerializeField] public string NameText { get; private set; }
        [field: SerializeField] public string DescriptionText { get; private set; }
        
        public BasicItemCharacteristicsData(BasicItemCharacteristicsData data)
        {
            LevelCharacteristicType = data.LevelCharacteristicType;
            ItemType = data.ItemType;
            ChangeType = data.ChangeType;
            NameText = data.NameText;
            DescriptionText = data.DescriptionText;
        }
        
        [JsonConstructor]
        public BasicItemCharacteristicsData(
            LevelCharacteristicType levelCharacteristicType,
            ItemType itemType,
            ChangeType changeType,
            string nameText,
            string descriptionText)
        {
            LevelCharacteristicType = levelCharacteristicType;
            ItemType = itemType;
            ChangeType = changeType;
            NameText = nameText;
            DescriptionText = descriptionText;
        }
    }
}
