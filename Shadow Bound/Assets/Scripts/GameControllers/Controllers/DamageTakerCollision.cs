using System;
using GameControllers.Controllers.Properties;
using UnityEngine;

namespace GameControllers.Controllers
{
    public class DamageTakerCollision : MonoBehaviour
    {
        public event Action<ICanTakeDamage> OnDamageTakerCollision;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out ICanTakeDamage takerDamage))
            {
                OnDamageTakerCollision?.Invoke(takerDamage);
            }
        }
    }
}
