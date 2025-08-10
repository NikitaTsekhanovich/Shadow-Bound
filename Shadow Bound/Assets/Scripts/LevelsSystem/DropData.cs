using System;
using GameControllers.Models;
using GameControllers.Models.ItemsEnums;
using UnityEngine;

namespace LevelsSystem
{
    [Serializable]
    public struct DropData 
    {
        [field: SerializeField] public LevelCharacteristicType LevelCharacteristicType { get; private set; }
        [field: SerializeField] public ItemType ItemType { get; private set; }
        [field: SerializeField] public Item DropItemPrefab { get; private set; }
        [field: SerializeField] public int Quantity { get; private set; }
        [field: SerializeField,  Range(0, 100)] public float DropChance { get; private set; }
    }
}
