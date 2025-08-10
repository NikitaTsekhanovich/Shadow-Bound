using System;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace GameControllers.Models
{
    public class StatusAttackText : PoolEntity
    {
        [SerializeField] private TMP_Text _damageText;
        
        private Sequence _sequenceText;
        
        private const float TimeLife = 1f;

        public override void ActiveInit(Vector3 startPosition, Quaternion startRotation)
        {
            base.ActiveInit(startPosition, Quaternion.identity);
            _damageText.text = "";
            _damageText.color = Color.white;
        }

        private void OnDestroy()
        {
            _sequenceText.Kill();
        }

        public void InitText(string status, bool isCritical, bool isHeal = false)
        {
            transform.localScale = Vector3.zero;
            
            if (isCritical)
                _damageText.color = Color.red;
            else if (isHeal)
                _damageText.color = Color.green;
            
            _damageText.text = status;

            _sequenceText = DOTween.Sequence()
                .AppendCallback(()  =>
                {
                    transform.DOScale(Vector3.one, 0.95f);
                    transform.DOShakePosition(
                        0.95f, 
                        new Vector3(0.2f, 0.2f, -0.2f),
                        2,
                        45f);
                })
                .AppendInterval(TimeLife)
                .AppendCallback(ReturnToPool);
        }
    }
}
