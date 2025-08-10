using System;
using GameControllers.Models;
using UnityEngine;

namespace GameControllers.Controllers.Properties
{
    public interface ICanTakeDamage
    {
        public event Action<ICanTakeDamage> Died;
        public GameEntityTypes GameEntityType { get; }
        public Transform Transform { get; }
        public int Experience { get; }
        public Health Health { get; }
        public void TakeDamage(float damage, ICanTakeDamage healthAttacker, bool isCriticalDamage);
    }
}
