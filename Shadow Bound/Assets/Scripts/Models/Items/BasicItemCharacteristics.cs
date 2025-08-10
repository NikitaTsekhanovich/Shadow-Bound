using System;
using GameControllers.Models.ItemCharacteristicsData;
using GameControllers.Models.ItemsEnums;
using Newtonsoft.Json;

namespace Models.Items
{
    [Serializable]
    public class BasicItemCharacteristics
    {
        [JsonProperty] public BasicItemCharacteristicsData BasicData { get; private set; }
        [JsonProperty] public int Quantity { get; private set; }
        
        public BasicItemCharacteristics(
            BasicItemCharacteristicsData basicItemCharacteristicsData,
            int quantity)
        {
            BasicData = new BasicItemCharacteristicsData(basicItemCharacteristicsData);
            Quantity = quantity;
        }
        
        [JsonConstructor]
        public BasicItemCharacteristics(
            LevelCharacteristicType levelCharacteristicType,
            ItemType itemType,
            ChangeType changeType,
            int quantity,
            string nameText,
            string descriptionText)
        {
            BasicData = new BasicItemCharacteristicsData(
                levelCharacteristicType,
                itemType,
                changeType,
                nameText,
                descriptionText);
            Quantity = quantity;
        }

        public virtual BasicItemCharacteristics CloneCharacteristics()
        {
            return new BasicItemCharacteristics(
                new BasicItemCharacteristicsData(BasicData),
                Quantity);
        }

        public virtual string GetInfoText()
        {
            return $"{BasicData.NameText}\n\n{BasicData.DescriptionText}";
        }

        public void UpdateQuantity(int quantity)
        {
            Quantity = quantity;
        }
        
        public void CalculateQuantity(int quantity)
        {
            Quantity += quantity;
        }
    }
}
