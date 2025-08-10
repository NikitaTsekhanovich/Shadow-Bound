using DG.Tweening;
using GameControllers.Controllers;
using GameControllers.Controllers.Properties;
using InventoriesControllers;
using UnityEngine;

namespace GameControllers.Models
{
    public class Item : MonoBehaviour, ICollectibleItem
    {
        [SerializeField] private Vector3 _scale;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        
        private GameLevelHandler _gameLevelHandler;
        
        public ItemSlot ItemSlot { get; private set; }

        private void OnDestroy()
        {
            transform.DOKill();
            _gameLevelHandler.OnWinLevel -= MoveToNinja;
        }

        public void Collect()
        {
            Destroy(gameObject);
        }

        public void Init(ItemSlot itemSlot, GameLevelHandler gameLevelHandler)
        {
            ItemSlot = itemSlot;
            _gameLevelHandler = gameLevelHandler;
            _spriteRenderer.sprite = ItemSlot.ItemIcon;
            transform.DOScale(_scale, 1f);
            transform.DOShakeRotation(1f, new Vector3(0.3f, 0f, 0.3f));

            _gameLevelHandler.OnWinLevel += MoveToNinja;
        }

        private void MoveToNinja(Transform ninjaTransform)
        {
            transform.DOMove(ninjaTransform.position, 1f);
        }
    }
}
