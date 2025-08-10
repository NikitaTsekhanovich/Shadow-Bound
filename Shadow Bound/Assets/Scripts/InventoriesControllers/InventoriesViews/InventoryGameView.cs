using InventoriesControllers.SlotsViews;
using UnityEngine;

namespace InventoriesControllers.InventoriesViews
{
    public class InventoryGameView : MonoBehaviour
    {
        [field: SerializeField] public SlotGameView[] SlotViews { get; private set; }
    }
}
