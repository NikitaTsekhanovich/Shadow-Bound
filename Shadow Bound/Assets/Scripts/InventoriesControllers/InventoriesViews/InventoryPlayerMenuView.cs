using InventoriesControllers.SlotsViews;
using UnityEngine;

namespace InventoriesControllers.InventoriesViews
{
    public class InventoryPlayerMenuView : MonoBehaviour
    {
        [field: SerializeField] public SlotPlayerMenuView[] SlotViews { get; private set; }
    }
}
