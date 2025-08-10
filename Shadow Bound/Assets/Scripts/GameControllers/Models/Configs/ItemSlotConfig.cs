using GameControllers.Models.ItemCharacteristicsData;
using UnityEngine;

namespace GameControllers.Models.Configs
{
    public class ItemSlotConfig : ScriptableObject
    {
        [field: SerializeField] public Sprite ItemIcon { get; private set; }
        [field: SerializeField] public BasicItemCharacteristicsData BasicItemCharacteristicsData { get; private set; }
    }
}
