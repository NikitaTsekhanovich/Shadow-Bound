using Factories.Properties;
using GameControllers.General.Bootstrap.Data;
using GlobalSystems;
using LevelsSystem;
using MusicSystem;
using SaveSystems;
using UnityEngine;
using Zenject;

namespace GameControllers.General.Bootstrap
{
    public class Game : MonoBehaviour
    {
        [SerializeField] private GameLoadData _gameLoadData;
        
        [Inject] private DiContainer _container;
        [Inject] private SaveSystem _saveSystem;
        [Inject] private LevelConfigsContainer _levelConfigsContainer;
        [Inject] private SceneDataLoader _sceneDataLoader;
        [Inject] private ICanGetItemSlot _itemSlotFactory;
        [Inject] private MusicSwitcher _musicSwitcher;
        
        private GameStateMachine _gameStateMachine;
        
        private void Awake()
        {
            _gameStateMachine = new GameStateMachine(
                _gameLoadData,
                _container, 
                _saveSystem,
                _levelConfigsContainer.GetCurrentLevelConfig(),
                _sceneDataLoader.CharacterGameType,
                _itemSlotFactory);
            
            _musicSwitcher.PlayGameBackgroundMusic();
        }

        private void Update()
        {
            _gameStateMachine?.UpdateSystem();
        }

        private void FixedUpdate()
        {
            _gameStateMachine?.FixedUpdateSystem();
        }
        
        private void OnApplicationPause(bool pauseStatus)
        {
            if (pauseStatus)
            {
                _saveSystem.WriteToJson();
            }
        }
        
        public void ClickButton()
        {
            _musicSwitcher.ClickSound.Play();
        }
    }
}
