using GameControllers.Models.ItemCharacteristicsData;
using UnityEngine;

namespace GameControllers.Models.Configs.Items
{
    [CreateAssetMenu(fileName = "WeaponConfig", menuName = "Configs/Items/WeaponConfig")]
    public class WeaponConfig : ItemSlotConfig
    {
        [field: SerializeField] public WeaponCharacteristicsData WeaponCharacteristicsData { get; private set; }
    }
}
