using Factories.Properties;
using GameControllers.Controllers;
using GameControllers.Models;
using GlobalSystems;
using InventoriesControllers.Inventories;
using InventoriesControllers.InventoriesViews;
using Models.Items;
using MusicSystem;
using SaveSystems;
using SaveSystems.DataTypes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace PlayerMenuControllers
{
    public class PlayerMenuController : MonoBehaviour
    {
        [SerializeField] private GameObject _inventoriesBlock;
        [Header("Inventories views")]
        [SerializeField] private InventoryPlayerMenuView _inventoryMainView;
        [SerializeField] private InventoryPlayerMenuView _inventoryPlayerView;
        [SerializeField] private InventoryCraftView _inventoryCraftView;
        [SerializeField] private InventoryEquipmentView _inventoryEquipmentView;
        [Header("Player views")]
        [SerializeField] private TMP_Text _lvlText;
        [SerializeField] private Image _lvlBarImage;
        [Header("Player models data")] 
        [SerializeField] private Transform _spawnPointModel;
        [SerializeField] private NinjaMenuModel _ninjaRegularPrefab;
        [SerializeField] private NinjaMenuModel _ninjaMagePrefab;
        [SerializeField] private NinjaMenuModel _ninjaArcherPrefab;
        [SerializeField] private Material[] _weaponMaterials;
        [SerializeField] private Material[] _armorMaterials;

        [Inject] private SaveSystem _saveSystem;
        [Inject] private SceneDataLoader _sceneDataLoader;
        [Inject] private ICanGetItemSlotMenuView _itemSlotMenuViewFactory;
        [Inject] private ICanGetItemSlot _itemSlotFactory;
        [Inject] private MusicSwitcher _musicSwitcher;
        
        private InventoryMainMenu _inventoryMain;
        private InventoryMainMenu _inventoryPlayer;
        private InventoryCraft _inventoryCraft;
        private InventoryEquipment _inventoryEquipment;
        private NinjaMenuModel _currentNinjaModel;
        
        private void Start()
        {
            _musicSwitcher.PlayMenuBackgroundMusic();
            SpawnModels();
            InitInventories();
            UpdateExperienceView(); 
        }

        private void OnDestroy()
        {
            SaveInventory();
        }

        private void SpawnModels()
        {
            if (_sceneDataLoader.CharacterGameType == CharacterGameTypes.Regular)
            {
                _currentNinjaModel = Instantiate(_ninjaRegularPrefab, _spawnPointModel);
            }
            else if (_sceneDataLoader.CharacterGameType == CharacterGameTypes.Mage)
            {
                _currentNinjaModel = Instantiate(_ninjaMagePrefab, _spawnPointModel);
            }
            else if (_sceneDataLoader.CharacterGameType == CharacterGameTypes.Archer)
            {
                _currentNinjaModel = Instantiate(_ninjaArcherPrefab, _spawnPointModel);
            }
            
            var weaponCharacteristics = _saveSystem.GetData<InventorySaveData, BasicItemCharacteristics>(
                InventorySaveData.GUIDEquipmentSlot1);
            
            var armorCharacteristics = _saveSystem.GetData<InventorySaveData, BasicItemCharacteristics>(
                InventorySaveData.GUIDEquipmentSlot2);

            var armorLevel = -1;
                
            if (armorCharacteristics != null)
            {
                armorLevel = (int)armorCharacteristics.BasicData.LevelCharacteristicType;
            }

            InitModelMaterials((int)weaponCharacteristics.BasicData.LevelCharacteristicType, armorLevel);
        }

        private void InitModelMaterials(int weaponLevel, int armorLevel)
        {
            var weaponMaterial = _weaponMaterials[weaponLevel];
            Material armorMaterial = null;

            if (armorLevel != -1)
            {
                armorMaterial = _armorMaterials[armorLevel];
            }
            
            _currentNinjaModel.InitMaterials(weaponMaterial, armorMaterial);
        }

        private void UpdateExperienceView()
        {
            var savedExperience = _saveSystem.GetData<PlayerSaveData, int>(PlayerSaveData.GUIDExperience);
            var savedLevel = _saveSystem.GetData<PlayerSaveData, int>(PlayerSaveData.GUIDLevel);
            
            var maximumExperience = savedLevel * ExperienceController.MaxExperienceMultiplier;
            
            _lvlText.text = $"Lvl: {savedLevel}\n{savedExperience}/{maximumExperience}";
            _lvlBarImage.fillAmount = savedExperience / (float)maximumExperience;
        }

        private void SaveInventory()
        {
            _inventoryMain.SaveSlotsData();
            _inventoryPlayer.SaveSlotsData();
            _inventoryCraft.SaveSlotsData();
            _inventoryEquipment.SaveSlotsData();
            _saveSystem.WriteToJson();
        }

        private void InitInventories()
        {
            _inventoryMain = new InventoryMainMenu(
                _itemSlotMenuViewFactory,
                _inventoryMainView,
                _saveSystem,
                InventorySaveData.GUIDMainInventorySlot);

            _inventoryPlayer = new InventoryMainMenu(
                _itemSlotMenuViewFactory,
                _inventoryPlayerView,
                _saveSystem,
                InventorySaveData.GUIDPlayerInventorySlot);
            
            _inventoryCraftView.InitializeSlotViews();
            _inventoryCraft = new InventoryCraft(
                _itemSlotMenuViewFactory,
                _inventoryCraftView,
                _itemSlotFactory,
                _saveSystem,
                InventorySaveData.GUIDCraftInventorySlot,
                CheckWeaponSlot,
                _musicSwitcher.CraftSound,
                _musicSwitcher.UpgradeSound);

            _inventoryEquipment = new InventoryEquipment(
                _itemSlotMenuViewFactory,
                _inventoryEquipmentView,
                _saveSystem,
                InventorySaveData.GUIDEquipmentSlot);
        }
        
        private void OnApplicationPause(bool pauseStatus)
        {
            if (pauseStatus)
            {
                _saveSystem.WriteToJson();
            }
        }

        private bool CheckWeaponSlot() => _inventoryEquipment.CheckChosenWeapon();

        public void ClickCloseInventory()
        {
            if (CheckWeaponSlot())
            {
                _inventoriesBlock.SetActive(false);
                _spawnPointModel.gameObject.SetActive(true);
                
                InitModelMaterials(_inventoryEquipment.WeaponLevel, _inventoryEquipment.ArmorLevel);
            }
        }

        public void ClickButton()
        {
            _musicSwitcher.ClickSound.Play();
        }

        public void ClickBackToMenu()
        {
            _sceneDataLoader.ChangeScene("MainMenu");
        }
    }
}
