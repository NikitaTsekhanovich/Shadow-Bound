using System;
using System.Collections.Generic;
using Factories.Properties;
using GameControllers.Controllers;
using GameControllers.Controllers.AnimatorsControllers;
using GameControllers.Controllers.Properties;
using GameControllers.Models.Enemies;
using GameControllers.Models.Equipment;
using GameControllers.Models.ItemsEnums;
using GameControllers.Views;
using GlobalSystems;
using LevelsSystem;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GameControllers.Models
{
    public class Enemy : Unit, ICanTakeDamage
    {
        [SerializeField] private HealthView _healthView;
        [SerializeField] private Transform _body;
        [SerializeField] private DamageTakerDetector _damageTakerDetector;
        [SerializeField] private Weapon _weapon;
        [SerializeField] private Collider _detectedCollider;
        [SerializeField] private AudioSource _takeDamageSound;

        private const int DelayDeath = 5;
        
        private Armor _armor;
        private List<DropData> _dropsData;
        private GameLevelHandler _gameLevelHandler;
        private ICanGetItemSlot _itemSlotFactory;
        private CharacterGameTypes _characterGameType;

        public event Action<ICanTakeDamage> Died;
        public Health Health { get; private set; }
        public int Experience { get; private set; }
        public GameEntityTypes GameEntityType => _gameEntityType;
        public Transform Transform => transform;
        public Transform Body => _body;
        public Weapon Weapon => _weapon;

        public virtual void Init(
            EnemyCharacteristicsData enemyCharacteristicsData,
            Armor armor,
            ICanGetPoolEntity<StatusAttackText> statusTextPool,
            List<DropData> dropsData,
            GameLevelHandler gameLevelHandler,
            ICanGetItemSlot itemSlotFactory,
            CharacterGameTypes characterGameType)
        {
            _gameLevelHandler = gameLevelHandler;
            _armor = armor;
            _dropsData = dropsData;
            Experience = enemyCharacteristicsData.Experience;
            _itemSlotFactory = itemSlotFactory;
            _characterGameType = characterGameType;
            Health = new Health(_healthView, enemyCharacteristicsData.Health, Die, statusTextPool, transform, _takeDamageSound, _armor);
            _weapon.InitDamageDealer(this, Health);
            
            var rigidbody = GetComponent<Rigidbody>();
            var animator = GetComponent<Animator>();
            
            var unitAnimator = new UnitAnimator(animator, enemyCharacteristicsData.AttackSpeed, enemyCharacteristicsData.Speed);
            var forwardMovement = new ForwardMovement(Vector3.left, rigidbody, enemyCharacteristicsData.Speed);
            
            InitStateMachine(unitAnimator, forwardMovement);
            
            _damageTakerDetector.OnDamageTakerDetected += DetectEnemy;
            _gameLevelHandler.OnLoseLevel += GoToIdleState;
        }

        private void OnDestroy()
        {
            _damageTakerDetector.OnDamageTakerDetected -= DetectEnemy;
            _gameLevelHandler.OnLoseLevel -= GoToIdleState;
        }

        private void DropItems()
        {
            foreach (var dropData in _dropsData)
            {
                var chance = Random.Range(0f, 100f);

                if (chance <= dropData.DropChance)
                {
                    var spawnPosition = new Vector3(
                        transform.position.x + Random.Range(-0.1f, 0.4f), 
                        transform.position.y + Random.Range(0f, 0.4f), 
                        transform.position.z);
                    
                    var itemType = dropData.ItemType;
                    
                    if (dropData.ItemType == ItemType.Katana || 
                        dropData.ItemType == ItemType.Bow || 
                        dropData.ItemType == ItemType.MagicCard)
                    {
                        if (_characterGameType == CharacterGameTypes.Regular)
                        {
                            itemType = ItemType.Katana;
                        }
                        else if (_characterGameType == CharacterGameTypes.Mage)
                        {
                            itemType = ItemType.MagicCard;
                        }
                        else if (_characterGameType == CharacterGameTypes.Archer)
                        {
                            itemType = ItemType.Bow;
                        }
                    }
                    
                    var itemCharacteristics = _itemSlotFactory.GetCharacteristics(
                        itemType, 
                        dropData.LevelCharacteristicType, 
                        dropData.Quantity);
                    
                    var newItemSlot = _itemSlotFactory.GetItemSlot(itemCharacteristics);
                    
                    var newItem = Instantiate(dropData.DropItemPrefab, spawnPosition, quaternion.identity);
                    newItem.Init(newItemSlot, _gameLevelHandler);
                }
            }
        }

        protected override void GoToIdleState(Transform ninjaTransform)
        {
            _damageTakerDetector.OnDamageTakerDetected -= DetectEnemy;
            base.GoToIdleState(ninjaTransform);
        }

        protected override void Die()
        {
            Destroy(_detectedCollider);
            DropItems();
            _gameLevelHandler.DecrementEnemies();
            _damageTakerDetector.OnDamageTakerDetected -= DetectEnemy;
            _gameLevelHandler.OnLoseLevel -= GoToIdleState;
            base.Die();
            _healthView.HideBar();
            Died?.Invoke(this);
            Destroy(gameObject, DelayDeath);
        }
        
        public void TakeDamage(float damage, ICanTakeDamage healthAttacker, bool isCriticalDamage)
        {
            Health.TakeDamage(damage, healthAttacker, isCriticalDamage);
        }
    }
}
