using GameControllers.Models.ItemCharacteristicsData;
using UnityEngine;

namespace GameControllers.Models.Configs.Items
{
    [CreateAssetMenu(fileName = "HealConfig", menuName = "Configs/Items/HealConfig")]
    public class HealConfig : ItemSlotConfig
    {
        [field: SerializeField] public HealCharacteristicsData HealCharacteristicsData { get; private set; }
    }
}
