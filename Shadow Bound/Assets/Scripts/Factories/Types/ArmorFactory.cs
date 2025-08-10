using GameControllers.Models.Equipment;
using GameControllers.Models.ItemCharacteristicsData;
using GameControllers.Models.ItemsEnums;
using UnityEngine;

namespace Factories.Types
{
    public class ArmorFactory
    {
        private readonly Armor _armorPrefab;
        private readonly Material[] _materials;
        
        public ArmorFactory(Armor prefab, Material[] materials)
        {
            _armorPrefab = prefab;
            _materials = materials;
        }
        
        public Armor GetEntity(
            Transform transformSpawn, 
            ArmorCharacteristicsData armorCharacteristicsData,
            LevelCharacteristicType levelCharacteristicType)
        {
            var newArmor = Object.Instantiate(_armorPrefab, transformSpawn.position, transformSpawn.rotation, transformSpawn);
            
            newArmor.Init(
                armorCharacteristicsData, 
                transformSpawn,
                _materials[(int)levelCharacteristicType]);
            
            return newArmor;
        }
    }
}
