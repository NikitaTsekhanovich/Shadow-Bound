using System.Collections.Generic;
using Factories.Properties;
using Factories.Types;
using GameControllers.Controllers;
using GameControllers.Controllers.Properties;
using GameControllers.Models;
using GameControllers.Models.Enemies;
using GameControllers.Models.Equipment;
using GameControllers.Models.ItemCharacteristicsData;
using GameControllers.Models.ItemsEnums;
using GlobalSystems;
using UnityEngine;

namespace LevelsSystem
{
    public class EnemySpawnPoint : MonoBehaviour
    {
        [SerializeField] private Enemy _enemyPrefab;
        [SerializeField] private EnemyCharacteristicsData _enemyCharacteristicsData;
        [Header("Armor data")]
        [SerializeField] private bool _hasArmor;
        [SerializeField] private ArmorCharacteristicsData _armorCharacteristicsData;
        [SerializeField] private LevelCharacteristicType _armorLevel;
        [Header("Weapon data")]
        [SerializeField] private WeaponCharacteristicsData _weaponCharacteristicsData;
        [SerializeField] private LevelCharacteristicType _weaponLevel;
        [Header("Drop data")]
        [SerializeField] private List<DropData> _dropsData;
        
        public DropData[] DropsData => _dropsData.ToArray();

        public void SpawnEnemy(
            ArmorFactory armorFactory,
            WeaponFactory katanaFactory,
            WeaponFactory magicCardFactory,
            WeaponFactory bowFactory,
            ICanGetPoolEntity<StatusAttackText> statusTextPool,
            GameLevelHandler gameLevelHandler,
            ICanGetItemSlot itemSlotFactory,
            CharacterGameTypes characterGameType)
        {
            var newEnemy = Instantiate(_enemyPrefab, transform.position, transform.rotation);

            Armor armor = null;
            if (_hasArmor)
            {
                armor = armorFactory.GetEntity(newEnemy.Body, _armorCharacteristicsData, _armorLevel);
            }

            if (_enemyPrefab as EnemyMage)
                magicCardFactory.InitWeaponCharacteristics(newEnemy.Weapon, _weaponCharacteristicsData, _weaponLevel);
            else if (_enemyPrefab as EnemyMelee)
                katanaFactory.InitWeaponCharacteristics(newEnemy.Weapon, _weaponCharacteristicsData, _weaponLevel);
            else if (_enemyPrefab as EnemyArcher)
                bowFactory.InitWeaponCharacteristics(newEnemy.Weapon, _weaponCharacteristicsData, _weaponLevel);
            
            gameLevelHandler.IncrementEnemies();
            newEnemy.Init(
                _enemyCharacteristicsData, 
                armor, 
                statusTextPool,
                _dropsData,
                gameLevelHandler,
                itemSlotFactory,
                characterGameType);
        }
    }
}
