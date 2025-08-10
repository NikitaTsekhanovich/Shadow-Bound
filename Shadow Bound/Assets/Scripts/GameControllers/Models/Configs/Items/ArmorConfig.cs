using GameControllers.Models.ItemCharacteristicsData;
using UnityEngine;

namespace GameControllers.Models.Configs.Items
{
    [CreateAssetMenu(fileName = "ArmorConfig", menuName = "Configs/Items/ArmorConfig")]
    public class ArmorConfig : ItemSlotConfig
    {
        [field: SerializeField] public ArmorCharacteristicsData ArmorCharacteristicsData { get; private set; }
    }
}
