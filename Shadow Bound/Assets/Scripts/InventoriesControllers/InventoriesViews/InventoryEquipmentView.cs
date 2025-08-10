using InventoriesControllers.SlotsViews;
using UnityEngine;

namespace InventoriesControllers.InventoriesViews
{
    public class InventoryEquipmentView : MonoBehaviour
    {
        [field: SerializeField] public SlotEquipmentView[] SlotEquipmentViews { get; private set; }
    }
}
