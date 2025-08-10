using System.Collections.Generic;
using UnityEngine;

namespace EducationControllers
{
    public class Education : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _images = new();
        [SerializeField] private GameObject _educationBlock;
        
        private int _index = 1;

        private void Awake()
        {
            var educationOver = PlayerPrefs.GetInt("EducationOver") == 1;

            if (!educationOver)
            {
                PlayerPrefs.SetInt("EducationOver", 1);
                _educationBlock.SetActive(true);
            }
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0) && _educationBlock.activeInHierarchy)
            {
                if (_index < _images.Count)
                {
                    if (_index - 1 >= 0)
                        _images[_index - 1].SetActive(false);
            
                    _images[_index].SetActive(true);
                    _index++;
                }
                else
                {
                    _images[0].SetActive(true);
                    _images[_index - 1].SetActive(false);
                    _index = 1;
                    _educationBlock.SetActive(false);
                }
            }
        }

        public void ClickStartEducation()
        {
            _educationBlock.SetActive(true);
        }
    }
}