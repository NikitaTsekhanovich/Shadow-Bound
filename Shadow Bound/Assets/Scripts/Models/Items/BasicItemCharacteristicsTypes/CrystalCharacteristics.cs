using GameControllers.Models.ItemCharacteristicsData;
using GameControllers.Models.ItemsEnums;
using Newtonsoft.Json;

namespace Models.Items.BasicItemCharacteristicsTypes
{
    public class CrystalCharacteristics : BasicItemCharacteristics
    {
        [JsonProperty] public CrystalCharacteristicsData Data { get; private set; }
        
        public CrystalCharacteristics(
            CrystalCharacteristicsData crystalCharacteristicsData,
            BasicItemCharacteristicsData basicItemCharacteristicsData,
            int quantity) : 
            base(basicItemCharacteristicsData,
                quantity)
        {
            Data = new CrystalCharacteristicsData(crystalCharacteristicsData);
        }
        
        [JsonConstructor]
        public CrystalCharacteristics(
            float improvementDamage,
            float improvementCriticalChance,
            float improvementCriticalDamage,
            float improvementPercentageVampirism,
            float improvementExtraHealth,
            float improvementEvasionProbability,
            float improvementDamageBlock,
            float improvementReflectedDamage,
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
            Data = new CrystalCharacteristicsData(
                improvementDamage,
                improvementCriticalChance,
                improvementCriticalDamage,
                improvementPercentageVampirism,
                improvementExtraHealth,
                improvementEvasionProbability,
                improvementDamageBlock,
                improvementReflectedDamage);
        }

        public override BasicItemCharacteristics CloneCharacteristics()
        {
            var basicItemCharacteristics = base.CloneCharacteristics();
            
            return new CrystalCharacteristics(
                Data, 
                basicItemCharacteristics.BasicData,
                Quantity);
        }

        public override string GetInfoText()
        {
            var basicInfoText = base.GetInfoText();
            
            return $"{basicInfoText}\n\n" +
                   $"Damage increase: {Data.ImprovementDamage}\n" +
                   $"Critical hit chance increase: {Data.ImprovementCriticalHitChance}%\n" +
                   $"Critical damage: {Data.ImprovementCriticalDamage}%\n" +
                   $"Attack vampirism increase: {Data.ImprovementPercentageVampirism}%\n" +
                   $"Increase armor health: {Data.ImprovementExtraHealth}\n" +
                   $"Improvement evasion probability: {Data.ImprovementEvasionProbability}%\n" +
                   $"Improvement damage block: {Data.ImprovementDamageBlock}%\n" +
                   $"Improvement reflected damage: {Data.ImprovementReflectedDamage}%";
        }
    }
}
