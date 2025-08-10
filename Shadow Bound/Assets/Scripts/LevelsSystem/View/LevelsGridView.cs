using System.Collections.Generic;
using Factories.Properties;
using GlobalSystems;
using SaveSystems;
using SaveSystems.DataTypes;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace LevelsSystem.View
{
    public class LevelsGridView : MonoBehaviour
    {
        [SerializeField] private LevelView _levelViewPrefab;
        [SerializeField] private GameObject _levelViewEmptyPrefab;
        [SerializeField] private GameObject _gridPrefab;
        [SerializeField] private Image _chosenDot;
        [SerializeField] private Image _unchosenDot;
        [SerializeField] private Button _nextGridButton;
        [SerializeField] private Button _previousGridButton;
        [SerializeField] private Transform _girdContainer;
        [SerializeField] private Transform _dotsContainer;
        [SerializeField] private Sprite _blueBackground;
        [SerializeField] private Sprite _pinkBackground;
        [Header("Description level block")]
        [SerializeField] private LevelLoader[] _levelLoaders;
        [SerializeField] private DropItemView _dropItemViewPrefab;
        [SerializeField] private RectTransform _dropItemContainer;
        [SerializeField] private GridLayoutGroup _gridLayoutGroup;
        [SerializeField] private Transform _descriptionBlock;
        
        [Inject] private LevelConfigsContainer _levelConfigsContainer;
        [Inject] private SceneDataLoader _sceneDataLoader;
        [Inject] private SaveSystem _saveSystem;
        [Inject] private ICanGetItemSlot _itemSlotFactory;

        private readonly List<GameObject> _grids = new ();
        private readonly List<Image> _dots = new ();
        
        private int _currentGridIndex;
        private float _heightDropItem;
        
        private void Start()
        {
            InitDescriptionSizeBlock();
            InitLevelsItems();
            
            if (_currentGridIndex + 1 >= _grids.Count)
                _nextGridButton.interactable = false;
        }

        public void ClickNextGrid()
        {
            if (_currentGridIndex + 1 < _grids.Count)
            {
                _previousGridButton.interactable = true;
                
                ChangeGrid(1);
                
                if (_currentGridIndex + 1 >= _grids.Count)
                    _nextGridButton.interactable = false;
            }
        }

        public void ClickPreviousGrid()
        {
            if (_currentGridIndex - 1 >= 0)
            {
                _nextGridButton.interactable = true;

                ChangeGrid(-1);
                
                if (_currentGridIndex - 1 < 0)
                    _previousGridButton.interactable = false;
            }
        }

        private void InitDescriptionSizeBlock()
        {
            _heightDropItem = _dropItemContainer.sizeDelta.y / DropItemView.SizeFactor;
            _gridLayoutGroup.cellSize = new Vector2(_dropItemContainer.rect.width, _heightDropItem);
        }

        private void ChangeGrid(int offset)
        {
            _grids[_currentGridIndex].SetActive(false);
            _dots[_currentGridIndex].sprite = _unchosenDot.sprite;

            _currentGridIndex += offset;
                
            _grids[_currentGridIndex].SetActive(true);
            _dots[_currentGridIndex].sprite = _chosenDot.sprite;
        }

        private void InitLevelsItems()
        {
            var countItems = 0;
            var currentGrid = Instantiate(_gridPrefab, _girdContainer);
            _grids.Add(currentGrid);
            
            var chosenDot = Instantiate(_chosenDot, _dotsContainer);
            _dots.Add(chosenDot);
            
            foreach (var levelConfig in _levelConfigsContainer.LevelConfigs)
            {
                if (levelConfig.Index % 6 == 0 && levelConfig.Index != 0)
                {
                    currentGrid = GetNextGridBlock();
                }
                
                var background = _blueBackground;
                
                if (levelConfig.Index % 2 == 0)
                {
                    background = _pinkBackground;
                }

                if (countItems < 3)
                {
                    SpawnLevelView(currentGrid, levelConfig, background);
                    SpawnLevelViewEmpty(currentGrid);
                }
                else
                {
                    SpawnLevelViewEmpty(currentGrid);
                    SpawnLevelView(currentGrid, levelConfig, background);
                }
                
                countItems++;
                
                if (countItems >= 6)
                    countItems = 0;
            }
        }

        private void SpawnLevelView(GameObject currentGrid, LevelConfig levelConfig, Sprite background)
        {
            var isOpen = _saveSystem.GetData<LevelsSaveData, bool>(
                $"{LevelsSaveData.GUIDLevelState}{levelConfig.Index}");
            
            var levelView = Instantiate(
                _levelViewPrefab, 
                currentGrid.transform.position,
                Quaternion.identity,
                currentGrid.transform);

            levelView.Init(
                levelConfig.Index, 
                background, 
                Play, 
                OpenLevelDescription,
                _levelLoaders[levelConfig.Index].SpawnPoints, 
                isOpen);
        }

        private void SpawnLevelViewEmpty(GameObject currentGrid)
        {
            Instantiate(
                _levelViewEmptyPrefab, 
                currentGrid.transform.position, 
                Quaternion.identity,
                currentGrid.transform);
        }

        private GameObject GetNextGridBlock()
        {
            var currentGrid = Instantiate(_gridPrefab, _girdContainer);
            currentGrid.SetActive(false);
            _grids.Add(currentGrid);
                    
            var unchosenDot = Instantiate(_unchosenDot, _dotsContainer);
            _dots.Add(unchosenDot);
            
            return currentGrid;
        }

        private void OpenLevelDescription(List<DropData> dropsData)
        {
            ClearDropsItemsContainer();
            
            var heightContainer = dropsData.Count * _heightDropItem;
            var widthContainer = _dropItemContainer.sizeDelta.x;
            _dropItemContainer.sizeDelta = new Vector2(widthContainer, heightContainer);
            
            foreach (var dropData in dropsData)
            {
                var dropItemView = Instantiate(_dropItemViewPrefab, _dropItemContainer);
                var config = _itemSlotFactory.GetItemSlotConfig(dropData.ItemType, dropData.LevelCharacteristicType);
                var description = $"Drop probability: {dropData.DropChance}%\nQuantity: {dropData.Quantity}";
                dropItemView.Init(config.ItemIcon, description);
            }
            
            _descriptionBlock.gameObject.SetActive(true);
        }

        private void ClearDropsItemsContainer()
        {
            for (var i = _dropItemContainer.childCount - 1; i >= 0; i--)
            {
                Destroy(_dropItemContainer.GetChild(i).gameObject);
            }
        }

        private void Play(int index)
        {
            _levelConfigsContainer.SetLevelIndex(index);
            _sceneDataLoader.ChangeScene("Game");
        }
    }
}
