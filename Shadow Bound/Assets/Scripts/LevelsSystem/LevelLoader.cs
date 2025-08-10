using Factories.Properties;
using Factories.Types;
using GameControllers.Controllers;
using GameControllers.Controllers.Properties;
using GameControllers.Models;
using GlobalSystems;
using UnityEngine;

namespace LevelsSystem
{
    public class LevelLoader : MonoBehaviour
    {
        [SerializeField] private EnemySpawnPoint[] _spawnPoints;
        
        public EnemySpawnPoint[] SpawnPoints => _spawnPoints;

        public void Load(
            ArmorFactory armorFactory, 
            WeaponFactory katanaFactory,
            WeaponFactory magicCardFactory,
            WeaponFactory bowFactory,
            ICanGetPoolEntity<StatusAttackText> statusTextPool,
            GameLevelHandler gameLevelHandler,
            ICanGetItemSlot itemSlotFactory,
            CharacterGameTypes characterGameType)
        {
            foreach (var spawnPoint in _spawnPoints)
            {
                spawnPoint.SpawnEnemy(
                    armorFactory, 
                    katanaFactory, 
                    magicCardFactory, 
                    bowFactory, 
                    statusTextPool,
                    gameLevelHandler,
                    itemSlotFactory,
                    characterGameType);
            }
        }
    }
}
