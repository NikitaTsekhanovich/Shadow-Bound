using System;
using GameControllers.Models.ItemCharacteristicsData;
using GameControllers.Models.ItemsEnums;
using Newtonsoft.Json;

namespace Models.Items.BasicItemCharacteristicsTypes
{
    [Serializable]
    public class WeaponCharacteristics : BasicItemCharacteristics
    {
        [JsonProperty] public WeaponCharacteristicsData WeaponCharacteristicsData { get; private set; } 

        public WeaponCharacteristics(
            WeaponCharacteristicsData weaponCharacteristics,
            BasicItemCharacteristicsData basicItemCharacteristics,
            int quantity) :
            base(basicItemCharacteristics, 
                quantity)
        { 
            WeaponCharacteristicsData = new WeaponCharacteristicsData(weaponCharacteristics);
        }
        
        [JsonConstructor]
        public WeaponCharacteristics(
            float damage,
            float criticalHitChance,
            float criticalDamage,
            float percentageVampirism,
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
            WeaponCharacteristicsData = new WeaponCharacteristicsData(
                damage,
                criticalHitChance,
                criticalDamage,
                percentageVampirism);
        }
        
        public override BasicItemCharacteristics CloneCharacteristics()
        {
            var basicItemCharacteristics = base.CloneCharacteristics();
            
            return new WeaponCharacteristics(
                WeaponCharacteristicsData, 
                basicItemCharacteristics.BasicData,
                Quantity);
        }
        
        public override string GetInfoText()
        {
            var basicInfoText = base.GetInfoText();
            
            return $"{basicInfoText}\n\n" +
                   $"Damage: {WeaponCharacteristicsData.Damage}\n\n" +
                   $"Critical hit chance: {WeaponCharacteristicsData.CriticalHitChance}%\n\n" +
                   $"Critical damage: {WeaponCharacteristicsData.CriticalDamage}%\n\n" +
                   $"Attack vampirism {WeaponCharacteristicsData.PercentageVampirism}%";
        }
    }
}
