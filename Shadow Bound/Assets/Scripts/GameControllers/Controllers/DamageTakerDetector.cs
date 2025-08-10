using System;
using System.Collections.Generic;
using GameControllers.Controllers.Properties;
using GameControllers.Models;
using UnityEngine;

namespace GameControllers.Controllers
{
    public class DamageTakerDetector : MonoBehaviour
    {
        [SerializeField] private GameEntityTypes _gameEntityType;
        
        private readonly HashSet<ICanTakeDamage> _damageTakers = new ();
        
        public event Action<bool> OnDamageTakerDetected;
        public event Action<int> OnDieDamageTaker;
        
        private void OnTriggerEnter(Collider collision)
        {
            if (collision.TryGetComponent(out ICanTakeDamage damageTaker) && 
                _gameEntityType != damageTaker.GameEntityType && 
                !damageTaker.Health.IsDead)
            {
                _damageTakers.Add(damageTaker);
                OnDamageTakerDetected?.Invoke(true);
                damageTaker.Died += DieDamageTaker;
            }
        }

        private void OnTriggerExit(Collider collision)
        {
            if (collision.TryGetComponent(out ICanTakeDamage damageTaker) && 
                _gameEntityType != damageTaker.GameEntityType)
            {
                RemoveDamageTaker(damageTaker);
            }
        }

        private void RemoveDamageTaker(ICanTakeDamage damageTaker)
        {
            damageTaker.Died -= DieDamageTaker;
            _damageTakers.Remove(damageTaker);
            
            if (_damageTakers.Count <= 0)
                OnDamageTakerDetected?.Invoke(false);
        }

        private void DieDamageTaker(ICanTakeDamage damageTaker)
        {
            OnDieDamageTaker?.Invoke(damageTaker.Experience);
            RemoveDamageTaker(damageTaker);
        }
    }
}
