using GameControllers.Models;
using GameControllers.Models.ItemCharacteristicsData;
using GameControllers.Models.ItemsEnums;
using UnityEngine;

namespace Factories.Types
{
    public class WeaponFactory
    {
        private readonly Weapon _weaponPrefab;
        private readonly Material[] _materials;
        
        public WeaponFactory(
            Weapon prefab, 
            Material[] materials)
        {
            _weaponPrefab = prefab;
            _materials = materials;
        }

        public Weapon GetEntity(
            Transform transformSpawn,
            WeaponCharacteristicsData weaponCharacteristicsData, 
            LevelCharacteristicType levelCharacteristicType)
        {
            var newWeapon = Object.Instantiate(_weaponPrefab, transformSpawn.position, transformSpawn.rotation, transformSpawn);

            InitWeaponCharacteristics(newWeapon, weaponCharacteristicsData, levelCharacteristicType);
            
            return newWeapon;
        }

        public void InitWeaponCharacteristics(
            Weapon weapon,
            WeaponCharacteristicsData weaponCharacteristicsData, 
            LevelCharacteristicType levelCharacteristicType)
        {
            weapon.InitCharacteristics(
                weaponCharacteristicsData, 
                _materials[(int)levelCharacteristicType]);
        }
    }
}
