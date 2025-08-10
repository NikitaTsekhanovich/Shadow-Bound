using System;
using GameControllers.Controllers.Properties;
using UnityEngine;

namespace GameControllers.Controllers
{
    public class CollectibleItemDetector : MonoBehaviour
    {
        public event Action<ICollectibleItem> OnCollectibleItemDetected;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out ICollectibleItem collectibleItem))
            {
                OnCollectibleItemDetected?.Invoke(collectibleItem);
            }
        }
    }
}
