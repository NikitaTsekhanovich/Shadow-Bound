using System;
using GameControllers.Controllers.Properties;
using UnityEngine;

namespace GameControllers.Models
{
    public abstract class PoolEntity : MonoBehaviour, IPoolEntity
    {
        private Action<IPoolEntity> _returnAction;
        
        public bool IsActive => gameObject.activeSelf;
        
        public virtual void SpawnInit(Action<IPoolEntity> returnAction)
        {
            _returnAction = returnAction;
        }

        public virtual void ActiveInit(Vector3 startPosition, Quaternion startRotation)
        {
            transform.position = startPosition;
            transform.rotation = startRotation;
        }
        
        protected virtual void ReturnToPool()
        {
            _returnAction?.Invoke(this);
        }

        public void ChangeStateEntity(bool state)
        {
            gameObject.SetActive(state);
        }
    }
}
