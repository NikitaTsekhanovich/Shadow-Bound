using GameControllers.Models.ItemCharacteristicsData;
using GameControllers.Models.ItemsEnums;
using Newtonsoft.Json;
using UnityEngine;

namespace Models.Items.BasicItemCharacteristicsTypes
{
    public class HealCharacteristics : BasicItemCharacteristics
    {
        [JsonProperty] public HealCharacteristicsData Data { get; private set; }
        
        public HealCharacteristics(
            HealCharacteristicsData healCharacteristicsData,
            BasicItemCharacteristicsData basicItemCharacteristicsData,
            int quantity) : 
            base(basicItemCharacteristicsData,
                quantity)
        {
            Data = new HealCharacteristicsData(healCharacteristicsData);
        }
        
        [JsonConstructor]
        public HealCharacteristics(
            float healValue,
            LevelCharacteristicType levelCharacteristicType,
            ItemType itemType,
            ChangeType changeType,
            int quantity,
            string nameText,
            string descriptionText) : 
            base(levelCharacteristicType,
                itemType,
                changeType,
                quantity,
                nameText,
                descriptionText)
        {
            Data = new HealCharacteristicsData(healValue);
        }

        public override BasicItemCharacteristics CloneCharacteristics()
        {
            var basicItemCharacteristics = base.CloneCharacteristics();
            
            return new HealCharacteristics(
                Data, 
                basicItemCharacteristics.BasicData,
                Quantity);
        }
        
        public override string GetInfoText()
        {
            var basicInfoText = base.GetInfoText();
            
            return $"{basicInfoText}\n\n" +
                   $"Health replenishment: {Data.HealValue}";
        }
    }
}
