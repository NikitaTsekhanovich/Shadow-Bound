using Factories.Properties;
using GameControllers.Models.ItemsEnums;
using InventoriesControllers.InventoriesViews;
using InventoriesControllers.SlotsViews;
using Models.Items;
using SaveSystems;

namespace InventoriesControllers.Inventories
{
    public class InventoryEquipment : Inventory
    {
        private readonly ICanGetItemSlotMenuView _itemSlotMenuViewFactory;
        private readonly SlotEquipmentView _slotWeaponView;
        private readonly SlotEquipmentView _slotArmorView;

        public int WeaponLevel
        {
            get
            {
                var itemSlot = _slotWeaponView.GetCurrentItemSlot();
                return (int)itemSlot.BasicItemCharacteristics.BasicData.LevelCharacteristicType;
            }
        }

        public int ArmorLevel
        {
            get
            {
                var itemSlot = _slotArmorView.GetCurrentItemSlot();
                return itemSlot == null ? -1 : (int)itemSlot.BasicItemCharacteristics.BasicData.LevelCharacteristicType;
            }
        }

        public InventoryEquipment(
            ICanGetItemSlotMenuView itemSlotMenuViewFactory,
            InventoryEquipmentView inventoryEquipmentView,
            SaveSystem saveSystem, 
            string guidSlot) : 
            base(inventoryEquipmentView.SlotEquipmentViews,
                saveSystem, 
                guidSlot)
        {
            _slotWeaponView = inventoryEquipmentView.SlotEquipmentViews[0];
            _slotArmorView = inventoryEquipmentView.SlotEquipmentViews[1];
            _itemSlotMenuViewFactory = itemSlotMenuViewFactory;
            
            CreateSlots();
        }

        protected override ItemSlotView GetItemSlotView(
            ISlotView slotView, 
            BasicItemCharacteristics basicItemCharacteristics)
        {
            return _itemSlotMenuViewFactory.GetItemSlotMenuView(
                slotView.Transform,
                basicItemCharacteristics,
                (SlotCatchingItemView)slotView);
        }

        public bool CheckChosenWeapon()
        {
            if (_slotWeaponView.GetCurrentItemSlot() == null)
            {
                _slotWeaponView.ShowWarningVoidSlot();
                return false;
            }
            
            return true;
        }
    }
}
