using System;
using UnityEngine;

namespace GameControllers.Models.Enemies
{
    [Serializable]
    public class EnemyCharacteristicsData
    {
        [field: SerializeField] public float Health { get; private set; }
        [field: SerializeField] public float Speed { get; private set; }
        [field: SerializeField] public int Experience { get; private set; }
        [field: SerializeField, Range(0.5f, 3.1f)] public float AttackSpeed  { get; private set; }
    }
}
