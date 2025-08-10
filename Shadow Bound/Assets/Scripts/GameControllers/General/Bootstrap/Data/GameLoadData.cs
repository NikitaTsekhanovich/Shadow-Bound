using System;
using GameControllers.Models;
using GameControllers.Models.Equipment;
using GameControllers.Models.Ninjas;
using GameControllers.Views;
using UnityEngine;

namespace GameControllers.General.Bootstrap.Data
{
    [Serializable]
    public struct GameLoadData
    {
        [field: Header("Game controllers")]
        [field: SerializeField] public GameLevelHandlerView GameLevelHandlerView { get; private set; }
        
        [field: Header("Game Load Data")]
        [field: SerializeField] public SpriteRenderer BackgroundImage { get; private set; }
        
        [field: Header("Status Attack Text Data")]
        [field: SerializeField] public StatusAttackText StatusAttackTextPrefab { get; private set; }
        [field: SerializeField] public int PreloadStatusPoolSize { get; private set; }
        
        [field: Header("Ninja data")]
        [field: SerializeField] public Transform PlayerSpawnPoint { get; private set; }
        [field: SerializeField] public NinjaRegular NinjaRegularPrefab { get; private set; }
        [field: SerializeField] public NinjaMage NinjaMagePrefab { get; private set; }
        [field: SerializeField] public NinjaArcher NinjaArcherPrefab { get; private set; }
        
        [field: Header("Weapons")]
        [field: SerializeField] public Katana KatanaPrefab { get; private set; }
        [field: SerializeField] public MagicCard MagicCardPrefab { get; private set; }
        [field: SerializeField] public Bow BowPrefab { get; private set; }
        [field: SerializeField] public Material[] WeaponMaterials { get; private set; }
        
        [field: Header("Armors")]
        [field: SerializeField] public Armor ArmorPrefab { get; private set; }
        [field: SerializeField] public Material[] ArmorMaterials { get; private set; }
    }
}
