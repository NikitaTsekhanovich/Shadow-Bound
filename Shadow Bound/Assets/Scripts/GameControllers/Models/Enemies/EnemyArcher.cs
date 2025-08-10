using System.Collections.Generic;
using Factories.Properties;
using GameControllers.Controllers;
using GameControllers.Controllers.Properties;
using GameControllers.Models.Equipment;
using GlobalSystems;
using LevelsSystem;
using UnityEngine;

namespace GameControllers.Models.Enemies
{
    public class EnemyArcher : Enemy
    {
        private Bow _bow;

        public override void Init(
            EnemyCharacteristicsData enemyCharacteristicsData, 
            Armor armor,
            ICanGetPoolEntity<StatusAttackText> statusTextPool, 
            List<DropData> dropsData,
            GameLevelHandler gameLevelHandler,
            ICanGetItemSlot itemSlotFactory,
            CharacterGameTypes characterGameType)
        {
            base.Init(enemyCharacteristicsData, armor, statusTextPool, dropsData, gameLevelHandler, itemSlotFactory, characterGameType);
            _bow = (Bow)Weapon;
            _bow.InitDirection(Vector3.left);
        }
        
        public void Shoot()
        {
            _bow.Shoot();
        }
    }
}
