using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using Zenject;

namespace MusicSystem
{
    public class MusicController
    {
        private readonly AudioMixer _mixer;
        private readonly Sprite _musicOnImage;
        private readonly Sprite _musicOffImage;
        private readonly Sprite _effectsOnImage;
        private readonly Sprite _effectsOffImage;
        
        private const int VolumeOn = 0;
        private const int VolumeOff = -80;
        private const string MusicMixerName = "Music";
        private const string EffectsMixerName = "SoundEffects";
        private const string MusicStateKey = "MusicState";
        private const string EffectsStateKey = "EffectsState";
        
        private bool _musicOn;
        private bool _effectsOn;

        private MusicController(
            AudioMixer mixer, 
            Sprite musicOnImage, 
            Sprite musicOffImage, 
            Sprite effectsOnImage, 
            Sprite effectsOffImage)
        {
            _mixer = mixer;
            _musicOnImage = musicOnImage;
            _musicOffImage = musicOffImage;
            _effectsOnImage = effectsOnImage;
            _effectsOffImage = effectsOffImage;
        }
        
        [Inject]
        private void Construct()
        {
            LoadMusicData();
        }

        private void LoadMusicData()
        {
            _musicOn = PlayerPrefs.GetInt(MusicStateKey) == 0;
            _effectsOn = PlayerPrefs.GetInt(EffectsStateKey) == 0;
        }

        private void ChangeVolume(bool isOn, string mixerName, Image currentImage,
            Sprite onImage, Sprite offImage)
        {
            if (isOn)
            {
                _mixer.SetFloat(mixerName, VolumeOn);
                currentImage.sprite = onImage;
            }
            else
            {
                _mixer.SetFloat(mixerName, VolumeOff);
                currentImage.sprite = offImage;
            }
        }

        public void CheckMusicState(Image currentMusicImage)
        {
            ChangeVolume(_musicOn, MusicMixerName, currentMusicImage,
                _musicOnImage, _musicOffImage);
        }

        public void CheckSoundEffectsState(Image currentEffectsImage)
        {
            ChangeVolume(_effectsOn, EffectsMixerName, currentEffectsImage,
                _effectsOnImage, _effectsOffImage);
        }

        public void ChangeMusicState(Image currentMusicImage)
        {
            _musicOn = !_musicOn;
            CheckMusicState(currentMusicImage);
            PlayerPrefs.SetInt(MusicStateKey, _musicOn ? 0 : 1);
        }

        public void ChangeSoundEffectsState(Image currentEffectsImage)
        {
            _effectsOn = !_effectsOn;
            CheckSoundEffectsState(currentEffectsImage);
            PlayerPrefs.SetInt(EffectsStateKey, _effectsOn ? 0 : 1);
        }
    }
}
