using System;
using GameControllers.Models.ItemCharacteristicsData;
using GameControllers.Models.ItemsEnums;
using Newtonsoft.Json;

namespace Models.Items.BasicItemCharacteristicsTypes
{
    [Serializable]
    public class ArmorCharacteristics : BasicItemCharacteristics
    {
        [JsonProperty] public ArmorCharacteristicsData ArmorCharacteristicsData { get; private set; } 

        public ArmorCharacteristics(
            ArmorCharacteristicsData armorCharacteristicsData,
            BasicItemCharacteristicsData basicItemCharacteristics,
            int quantity) :
            base(basicItemCharacteristics, 
                quantity)
        { 
            ArmorCharacteristicsData = new ArmorCharacteristicsData(armorCharacteristicsData);
        }
        
        [JsonConstructor]
        public ArmorCharacteristics(
            float extraHealth,
            float evasionProbability,
            float damageBlock,
            float reflectedDamage,
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
            ArmorCharacteristicsData = new ArmorCharacteristicsData(
                extraHealth,
                evasionProbability,
                damageBlock,
                reflectedDamage);
        }
        
        public override BasicItemCharacteristics CloneCharacteristics()
        {
            var basicItemCharacteristics = base.CloneCharacteristics();
            
            return new ArmorCharacteristics(
                ArmorCharacteristicsData, 
                basicItemCharacteristics.BasicData,
                Quantity);
        }
        
        public override string GetInfoText()
        {
            var basicInfoText = base.GetInfoText();
            
            return $"{basicInfoText}\n\n" +
                   $"Extra health: {ArmorCharacteristicsData.ExtraHealth}\n\n" +
                   $"Chance to dodge an attack: {ArmorCharacteristicsData.EvasionProbability}%\n\n" +
                   $"Damage block percentage: {ArmorCharacteristicsData.DamageBlock}%\n\n" +
                   $"Damage return probability: {ArmorCharacteristicsData.ReflectedDamage}%";
        }
    }
}
