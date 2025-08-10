using InventoriesControllers.SlotsViews;
using Models.Items;
using SaveSystems;
using SaveSystems.DataTypes;

namespace InventoriesControllers
{
    public abstract class Inventory
    {
        private readonly ISlotView[] _slotViews;
        private readonly SaveSystem _saveSystem;
        private readonly string _guidSlot;
        
        protected Inventory(
            ISlotView[] slotViews,
            SaveSystem saveSystem,
            string guidSlot)
        {
            _slotViews = slotViews;
            _saveSystem = saveSystem;
            _guidSlot = guidSlot;
        }

        public void SaveSlotsData()
        {
            for (var i = 0; i < _slotViews.Length; i++)
            {
                var itemSlot = _slotViews[i].GetCurrentItemSlot();
                _saveSystem.SaveData<InventorySaveData, BasicItemCharacteristics>(
                    itemSlot?.BasicItemCharacteristics, $"{_guidSlot}{i + 1}");
            }
        }
        
        protected abstract ItemSlotView GetItemSlotView(
            ISlotView slotView, 
            BasicItemCharacteristics basicItemCharacteristics);
        
        protected void CreateSlots()
        {
            for (var i = 0; i < _slotViews.Length; i++)
            {
                var basicItemCharacteristics = _saveSystem.GetData
                    <InventorySaveData, BasicItemCharacteristics>($"{_guidSlot}{i + 1}");
                
                if (basicItemCharacteristics != null)
                {
                    var itemSlotView = GetItemSlotView(_slotViews[i], basicItemCharacteristics);
                    _slotViews[i].Init(itemSlotView);
                }
                else
                {
                    _slotViews[i].Init(null);
                }
            }
        }
    }
}
