using System;
using Factories.Properties;
using GameControllers.Controllers;
using GameControllers.Controllers.AnimatorsControllers;
using GameControllers.Controllers.Properties;
using GameControllers.Models.Configs;
using GameControllers.Models.Equipment;
using GameControllers.Views;
using InventoriesControllers.Inventories;
using InventoriesControllers.InventoriesViews;
using SaveSystems;
using SaveSystems.DataTypes;
using UnityEngine;
using Zenject;

namespace GameControllers.Models
{
    public class Ninja : Unit, ICanTakeDamage
    {
        [Header("Controllers")]
        [SerializeField] private DamageTakerDetector _damageTakerDetector;
        [SerializeField] private CollectibleItemDetector _collectibleItemDetector;
        [SerializeField] private HealthView _healthView;
        [SerializeField] private Collider _detectorCollider;
        [SerializeField] private NinjaConfig _ninjaConfig;
        [SerializeField] private Transform _weaponParent;
        [SerializeField] private Transform _armorParent;
        [Header("Sounds")]
        [SerializeField] private AudioSource _healSound;
        [SerializeField] private AudioSource _takeDamageSound;
        
        private InventoryGame _inventoryGame;
        private SaveSystem _saveSystem;
        private Health _health;
        private ExperienceController _experienceController;
        private Armor _armor;
        private GameLevelHandler _gameLevelHandler;
        
        protected Weapon Weapon;

        public GameEntityTypes GameEntityType => _gameEntityType;
        public Transform Transform => transform;
        public int Experience => 0;
        public event Action<ICanTakeDamage> Died;
        public Health Health => _health;

        [Inject]
        private void Constructor(
            InventoryGameView inventoryGameView,
            SaveSystem saveSystem,
            ICanGetItemSlotGameView itemSlotMenuViewFactory)
        {
            _saveSystem = saveSystem;

            _experienceController = new ExperienceController(_saveSystem);

            _inventoryGame = new InventoryGame(
                itemSlotMenuViewFactory,
                inventoryGameView,
                _saveSystem,
                InventorySaveData.GUIDPlayerInventorySlot,
                this);
            
            var rigidbody = GetComponent<Rigidbody>();
            var animator = GetComponent<Animator>();
            
            var attackSpeed = _ninjaConfig.AttackSpeed + Mathf.Clamp((_experienceController.CurrentLevel - 1) / 10f, 0f, 3.1f);
            var moveSpeed = _ninjaConfig.MoveSpeed + Mathf.Clamp((_experienceController.CurrentLevel - 1) / 10f, 0f, 5f);
            var unitAnimator = new UnitAnimator(animator, attackSpeed, moveSpeed);
            var forwardMovement = new ForwardMovement(Vector3.right, rigidbody, _ninjaConfig.MoveSpeed);
            
            InitStateMachine(unitAnimator, forwardMovement);
            
            _damageTakerDetector.OnDamageTakerDetected += DetectEnemy;
            _damageTakerDetector.OnDieDamageTaker += TakeExperience;
            _collectibleItemDetector.OnCollectibleItemDetected += CollectItem;
        }

        private void OnDestroy()
        {
            Unsubscribe();
            _inventoryGame.SaveSlotsData();
            _saveSystem.WriteToJson();
        }

        private void CollectItem(ICollectibleItem collectibleItem)
        {
            if (_inventoryGame.TryCollectItem(collectibleItem))
            {
                collectibleItem.Collect();
            }
        }

        private void TakeExperience(int experience)
        {
            _experienceController.TakeExperience(experience);
        }

        private void Unsubscribe()
        {
            _gameLevelHandler.OnWinLevel -= GoToIdleState;
            _damageTakerDetector.OnDamageTakerDetected -= DetectEnemy;
            _damageTakerDetector.OnDieDamageTaker -= TakeExperience;
            _collectibleItemDetector.OnCollectibleItemDetected -= CollectItem;
        }
        
        protected override void Die()
        {
            _gameLevelHandler.DiePlayer();
            base.Die();
            Died?.Invoke(this);
            Destroy(_detectorCollider);
            Unsubscribe();
        }

        protected override void GoToIdleState(Transform ninjaTransform)
        {
            _damageTakerDetector.OnDamageTakerDetected -= DetectEnemy;
            Destroy(_detectorCollider);
            base.GoToIdleState(ninjaTransform);
        }

        public void Initialize(
            ICanGetPoolEntity<StatusAttackText> statusTextPool,
            GameLevelHandler gameLevelHandler)
        {
            _health = new Health(_healthView, _ninjaConfig.MaxHealth, Die, statusTextPool, transform, _takeDamageSound, _armor);
            Weapon.InitDamageDealer(this, _health);
            _gameLevelHandler = gameLevelHandler;
            _gameLevelHandler.InitNinjaTransform(transform);
            _gameLevelHandler.OnWinLevel += GoToIdleState;
        }

        public virtual void SetWeapon(Weapon weapon)
        {
            Weapon = weapon;
            Weapon.transform.SetParent(_weaponParent);
            Weapon.transform.localPosition = Vector3.zero;
        }

        public void SetArmor(Armor armor)
        {
            _armor = armor;
            _armor.transform.SetParent(_armorParent);
            _armor.transform.localScale = Vector3.one;
            _armor.transform.localPosition = Vector3.zero;
            _armor.transform.localRotation = Quaternion.identity;
        }
        
        public void TakeDamage(float damage, ICanTakeDamage healthAttacker, bool isCriticalDamage)
        {
            _health.TakeDamage(damage, healthAttacker, isCriticalDamage);
        }

        public bool TryHeal(float heal)
        {
            if (_health.IsDead) return false;
            
            _healSound.Play();
            _health.Heal(heal);
            return true;
        }
    }
}
