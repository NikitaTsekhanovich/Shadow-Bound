using GameControllers.Controllers.Properties;
using GameControllers.Models;
using GameControllers.Models.ItemCharacteristicsData;
using Models.Items.BasicItemCharacteristicsTypes;
using UnityEngine;

namespace InventoriesControllers.ItemSlots
{
    public class UsableItemSlot : ItemSlot, IUsableItem
    {
        private readonly HealCharacteristicsData _healCharacteristicsData;
        
        private Ninja _ninja;
        
        public UsableItemSlot(
            Sprite itemIcon,
            HealCharacteristics healCharacteristics) :
            base(
                itemIcon,
                healCharacteristics)
        {
            _healCharacteristicsData = healCharacteristics.Data;
        }

        public void InitUnitUsingItem(Ninja ninja)
        {
            _ninja = ninja;
        }

        public void Use()
        {
            if (_ninja.TryHeal(_healCharacteristicsData.HealValue))
            {
                BasicItemCharacteristics.CalculateQuantity(-1);
            }
        }
    }
}
