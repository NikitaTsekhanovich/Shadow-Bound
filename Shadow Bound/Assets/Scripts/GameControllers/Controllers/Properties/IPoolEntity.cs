using System;
using UnityEngine;

namespace GameControllers.Controllers.Properties
{
    public interface IPoolEntity
    {
        public void SpawnInit(Action<IPoolEntity> returnAction);
        public void ActiveInit(Vector3 startPosition, Quaternion startRotation);
        public void ChangeStateEntity(bool state);
    }
}
