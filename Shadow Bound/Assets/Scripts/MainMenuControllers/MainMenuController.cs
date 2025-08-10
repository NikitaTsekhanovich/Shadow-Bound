using GameControllers.Models.Configs.Items;
using GlobalSystems;
using MusicSystem;
using SaveSystems;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace MainMenuControllers
{
    public class MainMenuController : MonoBehaviour
    {
        [SerializeField] private WeaponConfig _katanaConfig;
        [SerializeField] private WeaponConfig _magicCardConfig;
        [SerializeField] private WeaponConfig _bowConfig;
        [SerializeField] private Image _musicImage;
        [SerializeField] private Image _soundImage;
        
        [Inject] private SceneDataLoader _sceneDataLoader;
        [Inject] private SaveSystem _saveSystem;
        [Inject] private MusicController _musicController;
        [Inject] private MusicSwitcher _musicSwitcher;

        private void Awake()
        {
            _musicController.CheckMusicState(_musicImage);
            _musicController.CheckSoundEffectsState(_soundImage);
            _musicSwitcher.PlayMenuBackgroundMusic();
        }

        public void ClickOpenPlayerMenuNinja()
        {
            _saveSystem.InitializeFile(SaveSystem.SaveFileNameNinja, _katanaConfig);
            _sceneDataLoader.SetCharacterGameType(CharacterGameTypes.Regular);
            _sceneDataLoader.ChangeScene("PlayerMenu");
        }
        
        public void ClickOpenPlayerMenuOnmyoji()
        {
            _saveSystem.InitializeFile(SaveSystem.SaveFileNameOnmyoji, _magicCardConfig);
            _sceneDataLoader.SetCharacterGameType(CharacterGameTypes.Mage);
            _sceneDataLoader.ChangeScene("PlayerMenu");
        }
        
        public void ClickOpenPlayerMenuArcher()
        {
            _saveSystem.InitializeFile(SaveSystem.SaveFileNameArcher, _bowConfig);
            _sceneDataLoader.SetCharacterGameType(CharacterGameTypes.Archer);
            _sceneDataLoader.ChangeScene("PlayerMenu");
        }

        public void ClickChangeMusicState(Image image)
        {
            _musicController.ChangeMusicState(image);
        }

        public void ClickChangeSoundState(Image image)
        {
            _musicController.ChangeSoundEffectsState(image);
        }
        
        public void ClickButton()
        {
            _musicSwitcher.ClickSound.Play();
        }
    }
}
