using Factories.Properties;
using Factories.Types;
using GameControllers.Controllers;
using GameControllers.Controllers.Properties;
using GameControllers.General.Bootstrap.Data;
using GameControllers.Models;
using GameControllers.StateMachineBasic;
using GlobalSystems;
using LevelsSystem;
using Models.Items;
using Models.Items.BasicItemCharacteristicsTypes;
using SaveSystems;
using SaveSystems.DataTypes;
using UnityEngine;
using Zenject;
using Ninja = GameControllers.Models.Ninja;

namespace GameControllers.General.Bootstrap.States
{
    public class LoadState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly GameSystemsHandler _gameSystemsHandler;
        private readonly GameLoadData _gameLoadData;
        private readonly DiContainer _container;
        private readonly SaveSystem _saveSystem;
        private readonly LevelConfig _levelConfig;
        private readonly CharacterGameTypes _characterGameType;
        private readonly ICanGetItemSlot _itemSlotFactory;
        
        public LoadState(
            GameStateMachine gameStateMachine,
            GameSystemsHandler gameSystemsHandler,
            GameLoadData gameLoadData,
            DiContainer container,
            SaveSystem saveSystem,
            LevelConfig levelConfig,
            CharacterGameTypes characterGameType,
            ICanGetItemSlot itemSlotFactory)
        {
            _gameStateMachine = gameStateMachine;
            _gameSystemsHandler = gameSystemsHandler;
            _gameLoadData = gameLoadData;
            _container = container;
            _saveSystem = saveSystem;
            _levelConfig = levelConfig;
            _characterGameType = characterGameType;
            _itemSlotFactory = itemSlotFactory;
        }
        
        public void Enter()
        {
            var statusTextFactory = new DamageViewPoolFactory(_gameLoadData.StatusAttackTextPrefab, _gameLoadData.PreloadStatusPoolSize);
            var armorFactory = new ArmorFactory(_gameLoadData.ArmorPrefab, _gameLoadData.ArmorMaterials);
            var katanaFactory = new WeaponFactory(_gameLoadData.KatanaPrefab, _gameLoadData.WeaponMaterials);
            var magicCardFactory = new WeaponFactory(_gameLoadData.MagicCardPrefab, _gameLoadData.WeaponMaterials);
            var bowFactory = new WeaponFactory(_gameLoadData.BowPrefab, _gameLoadData.WeaponMaterials);
            var gameLevelHandler = new GameLevelHandler(_saveSystem, _levelConfig.Index, _gameLoadData.GameLevelHandlerView);
            
            InitGame();
            SpawnPlayer(armorFactory, katanaFactory, magicCardFactory, bowFactory, statusTextFactory, gameLevelHandler);
            LoadLevel(armorFactory, katanaFactory, magicCardFactory, bowFactory, statusTextFactory, gameLevelHandler);

            _gameStateMachine.EnterIn<LoopState>();
        }
        
        public void Exit()
        {
            
        }

        private void InitGame()
        {
            _gameLoadData.BackgroundImage.sprite = _levelConfig.Background;
        }

        private void SpawnPlayer(
            ArmorFactory armorFactory, 
            WeaponFactory katanaFactory, 
            WeaponFactory magicCardFactory,
            WeaponFactory bowFactory,
            ICanGetPoolEntity<StatusAttackText> statusTextPool,
            GameLevelHandler gameLevelHandler)
        {
            Ninja player = null;
            
            if (_characterGameType == CharacterGameTypes.Regular)
            {
                player = Object.Instantiate(
                    _gameLoadData.NinjaRegularPrefab,
                    _gameLoadData.PlayerSpawnPoint.position,
                    _gameLoadData.PlayerSpawnPoint.rotation);
                
                SpawnPlayerWeapon(player, katanaFactory);
            }
            else if (_characterGameType == CharacterGameTypes.Mage)
            {
                player = Object.Instantiate(
                    _gameLoadData.NinjaMagePrefab,
                    _gameLoadData.PlayerSpawnPoint.position,
                    _gameLoadData.PlayerSpawnPoint.rotation);
                
                SpawnPlayerWeapon(player, magicCardFactory);
            }
            else if (_characterGameType == CharacterGameTypes.Archer)
            {
                player = Object.Instantiate(
                    _gameLoadData.NinjaArcherPrefab,
                    _gameLoadData.PlayerSpawnPoint.position,
                    _gameLoadData.PlayerSpawnPoint.rotation);
                
                SpawnPlayerWeapon(player, bowFactory);
            }
            
            SpawnPlayerArmor(player, armorFactory);
            player.Initialize(statusTextPool, gameLevelHandler);

            _container.Inject(player);
        }

        private void SpawnPlayerArmor(Ninja player, ArmorFactory armorFactory)
        {
            var armorCharacteristics = 
                (ArmorCharacteristics)_saveSystem.GetData<InventorySaveData, BasicItemCharacteristics>(
                    InventorySaveData.GUIDEquipmentSlot2);
            
            if (armorCharacteristics == null) return;
            
            var armor = armorFactory.GetEntity(
                player.transform, 
                armorCharacteristics.ArmorCharacteristicsData, 
                armorCharacteristics.BasicData.LevelCharacteristicType);
            
            player.SetArmor(armor);
        }

        private void SpawnPlayerWeapon(Ninja player, WeaponFactory katanaFactory)
        {
            var weaponCharacteristics = 
                (WeaponCharacteristics)_saveSystem.GetData<InventorySaveData, BasicItemCharacteristics>(
                    InventorySaveData.GUIDEquipmentSlot1);
            
            var katana = katanaFactory.GetEntity(
                player.transform,
                weaponCharacteristics.WeaponCharacteristicsData, 
                weaponCharacteristics.BasicData.LevelCharacteristicType);
            
            player.SetWeapon(katana);
        }

        private void LoadLevel(
            ArmorFactory armorFactory, 
            WeaponFactory katanaFactory,
            WeaponFactory magicCardFactory,
            WeaponFactory bowFactory,
            ICanGetPoolEntity<StatusAttackText> statusTextPool,
            GameLevelHandler gameLevelHandler )
        {
            var levelLoader = Object.Instantiate(_levelConfig.LevelLoader);
            levelLoader.Load(
                armorFactory, 
                katanaFactory, 
                magicCardFactory, 
                bowFactory, 
                statusTextPool,
                gameLevelHandler, 
                _itemSlotFactory,
                _characterGameType);
        }
    }
}
