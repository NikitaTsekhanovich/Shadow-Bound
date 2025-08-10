using DG.Tweening;
using GlobalSystems;
using MusicSystem;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace GameControllers.Views
{
    public class GameLevelHandlerView : MonoBehaviour
    {
        [SerializeField] private GameObject _loseBlock;
        [SerializeField] private GameObject _winBlock;
        [Header("Sounds images")]
        [SerializeField] private Image[] _soundImages;
        [SerializeField] private Image[] _effectImages;
        
        [Inject] private SceneDataLoader _sceneDataLoader;
        [Inject] private MusicSwitcher _musicSwitcher;
        [Inject] private MusicController _musicController;

        private void Start()
        {
            UpdateMusicButtons();
        }

        private void OnDestroy()
        {
            _musicSwitcher.LoseSound.Stop();
            _musicSwitcher.WinSound.Stop();
        }

        private void UpdateMusicButtons()
        {
            foreach (var soundImage in _soundImages)
            {
                _musicController.CheckMusicState(soundImage);
            }
            
            foreach (var effectImage in _effectImages)
            {
                _musicController.CheckSoundEffectsState(effectImage);
            }
        }

        public void ShowLoseBlock()
        {
            UpdateMusicButtons();
            
            _musicSwitcher.GameBackgroundMusic.Stop();
            _musicSwitcher.LoseSound.Play();
            _loseBlock.SetActive(true);
        }

        public void ShowWinBlock()
        {
            UpdateMusicButtons();
            
            _musicSwitcher.GameBackgroundMusic.Stop();
            _musicSwitcher.WinSound.Play();
            DOTween.Sequence()
                .AppendInterval(1f)
                .AppendCallback(() => _winBlock.SetActive(true));
        }
        
        public void ClickPause()
        {
            
        }

        public void ClickResume()
        {
            
        }

        public void ClickRestart()
        {
            _sceneDataLoader.ChangeScene("Game");
        }

        public void ClickBackToMenu()
        {
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
    }
}
