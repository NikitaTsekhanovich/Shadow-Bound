using DG.Tweening;
using GlobalSystems;
using UnityEngine;

namespace MusicSystem
{
    public class MusicSwitcher : MonoBehaviour
    {
        [field: Header("Musics")]
        [field: SerializeField] public AudioSource MenuBackgroundMusic { get; private set; }
        [field: SerializeField] public AudioSource GameBackgroundMusic { get; private set; }
        
        [field: Header("Effects")]
        [field: SerializeField] public AudioSource ClickSound { get; private set; }
        [field: SerializeField] public AudioSource WinSound { get; private set; }
        [field: SerializeField] public AudioSource LoseSound { get; private set; }
        [field: SerializeField] public AudioSource CraftSound { get; private set; }
        [field: SerializeField] public AudioSource UpgradeSound { get; private set; }

        private void Start()
        {
            DontDestroyOnLoad(this);
        }

        public void PlayMenuBackgroundMusic()
        {
            if (MenuBackgroundMusic.isPlaying) return;
            
            DOTween.Sequence()
                .AppendInterval(LoadingScreenController.TimeLoadScene)
                .AppendCallback(() =>
                {
                    GameBackgroundMusic.Stop();
                    MenuBackgroundMusic.Play();
                });
        }

        public void PlayGameBackgroundMusic()
        {
            if (GameBackgroundMusic.isPlaying) return;
            
            DOTween.Sequence()
                .AppendInterval(LoadingScreenController.TimeLoadScene)
                .AppendCallback(() =>
                {
                    MenuBackgroundMusic.Stop();
                    GameBackgroundMusic.Play();
                });
        }
    }
}
