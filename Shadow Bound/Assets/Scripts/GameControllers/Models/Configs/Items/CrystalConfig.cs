using GameControllers.Models.ItemCharacteristicsData;
using UnityEngine;

namespace GameControllers.Models.Configs.Items
{
    [CreateAssetMenu(fileName = "CrystalConfig", menuName = "Configs/Items/CrystalConfig")]
    public class CrystalConfig : ItemSlotConfig
    {
        [field: SerializeField] public CrystalCharacteristicsData CrystalCharacteristicsData { get; private set; }
    }
}
