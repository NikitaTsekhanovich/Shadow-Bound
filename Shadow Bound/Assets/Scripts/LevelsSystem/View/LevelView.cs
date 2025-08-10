using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LevelsSystem.View
{
    public class LevelView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _levelText;
        [SerializeField] private Image _backgroundImage;
        
        private readonly List<DropData> _dropsData = new ();

        private Button _button;
        private int _index;
        private Action<int> _clickPlay;
        private Action<List<DropData>> _clickOpenDescription;

        public void Init(
            int index, 
            Sprite background, 
            Action<int> play, 
            Action<List<DropData>> openDescription,
            EnemySpawnPoint[] enemySpawnPoints,
            bool isOpen)
        {
            CreateDropsData(enemySpawnPoints);
            _button = GetComponent<Button>();
            _button.interactable = isOpen;
            _backgroundImage.sprite = background;
            _index = index;
            _clickPlay = play;
            _clickOpenDescription = openDescription;
            
            _levelText.text = $"{_index + 1}\nLevel";
        }

        private void CreateDropsData(EnemySpawnPoint[] enemySpawnPoints)
        {
            foreach (var enemySpawnPoint in enemySpawnPoints)
            {
                foreach (var dropData in enemySpawnPoint.DropsData)
                {
                    _dropsData.Add(dropData);
                }
            }
        }

        public void ClickPlay()
        {
            _clickPlay?.Invoke(_index);
        }

        public void ClickInfo()
        {
            _clickOpenDescription?.Invoke(_dropsData);
        }
    }
}
