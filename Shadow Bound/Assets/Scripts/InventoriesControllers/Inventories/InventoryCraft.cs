using System;
using Factories.Properties;
using GameControllers.Models.ItemsEnums;
using InventoriesControllers.InventoriesViews;
using InventoriesControllers.SlotsViews;
using Models.Items;
using Models.Items.BasicItemCharacteristicsTypes;
using SaveSystems;
using UnityEngine;

namespace InventoriesControllers.Inventories
{
    public class InventoryCraft : Inventory
    {
        private readonly ICanGetItemSlotMenuView _itemSlotMenuViewFactory;
        private readonly InventoryCraftView _inventoryCraftView;
        private readonly ICanGetItemSlot _itemSlotFactory;
        private readonly Func<bool> _checkWeapon;
        private readonly AudioSource _craftSound;
        private readonly AudioSource _upgradeSound;
        
        public InventoryCraft(
            ICanGetItemSlotMenuView itemSlotMenuViewFactory,
            InventoryCraftView inventoryCraftView,
            ICanGetItemSlot itemSlotFactory,
            SaveSystem saveSystem,
            string guidSlot,
            Func<bool> checkWeapon,
            AudioSource craftSound,
            AudioSource upgradeSound) :
            base(inventoryCraftView.SlotViews.ToArray(), 
                saveSystem, 
                guidSlot)
        {
            _itemSlotMenuViewFactory = itemSlotMenuViewFactory;
            _inventoryCraftView = inventoryCraftView;
            _itemSlotFactory = itemSlotFactory;
            _checkWeapon = checkWeapon;
            _craftSound = craftSound;
            _upgradeSound = upgradeSound;
            _inventoryCraftView.SetCraftOrUpgradeAction(CraftOrUpgrade);
            
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

        private void CraftOrUpgrade()
        {
            if (!CheckPossibilityCraft(out var craftType))
            {
                return;
            }

            if (TryUpgrade(craftType))
            {
                _upgradeSound.Play();
                foreach (var slotCraftView in _inventoryCraftView.SlotCraftViews)
                {
                    slotCraftView.DecreaseItemSlotQuantity(-1);
                }
            }
            else if (TryCraft())
            {
                _craftSound.Play();
                foreach (var slotCraftView in _inventoryCraftView.SlotCraftViews)
                {
                    slotCraftView.DecreaseItemSlotQuantity(-1);
                }
            }
        }

        private bool CheckPossibilityCraft(out ItemType craftType)
        {
            craftType = ItemType.None;
            var levelType = LevelCharacteristicType.Common;
            
            foreach (var slotCraftView in _inventoryCraftView.SlotCraftViews)
            {
                var currentItemSlot = slotCraftView.GetCurrentItemSlot();
                
                if (currentItemSlot != null && 
                    (craftType == ItemType.None || 
                     currentItemSlot.BasicItemCharacteristics.BasicData.ItemType == craftType) &&
                    (craftType == ItemType.None || 
                    levelType == currentItemSlot.BasicItemCharacteristics.BasicData.LevelCharacteristicType))
                {
                    craftType = currentItemSlot.BasicItemCharacteristics.BasicData.ItemType;
                    levelType = currentItemSlot.BasicItemCharacteristics.BasicData.LevelCharacteristicType;
                }
                else
                {
                    return false;
                }
            }
            
            return true;
        }

        private bool TryCraft()
        {
            var endItemSlot = _inventoryCraftView.SlotEndCraftView.GetCurrentItemSlot();
            var templateItemSlot = _inventoryCraftView.SlotCraftViews[0].GetCurrentItemSlot();

            if (endItemSlot == null)
            {
                return TryCreateItemSlot(templateItemSlot);
            }
            
            if ((int)endItemSlot.BasicItemCharacteristics.BasicData.LevelCharacteristicType ==
                (int)templateItemSlot.BasicItemCharacteristics.BasicData.LevelCharacteristicType + 1 &&
                endItemSlot.BasicItemCharacteristics.BasicData.ItemType ==
                templateItemSlot.BasicItemCharacteristics.BasicData.ItemType)
            {
                return TryConnectItemSlots(endItemSlot);
            }
            
            // Debug.Log("Дургой тип айтема");
            return false;
        }

        private bool TryConnectItemSlots(ItemSlot endItemSlot)
        {
            if (endItemSlot.TryIncreaseQuantity(1))
            {
                _inventoryCraftView.SlotEndCraftView.UpdateQuantityText();
                return true;
            }
                
            // Debug.Log("Нет места");
            return false;
        }

        private bool TryCreateItemSlot(ItemSlot templateItemSlot)
        {
            var newCharacteristics = _itemSlotFactory.GetNextLevelCharacteristics(
                templateItemSlot.BasicItemCharacteristics.BasicData.ItemType,
                templateItemSlot.BasicItemCharacteristics.BasicData.LevelCharacteristicType);

            if (newCharacteristics != null)
            {
                _inventoryCraftView.SlotEndCraftView.CreateItemSlotView(newCharacteristics);
                return true;
            }

            var itemType = templateItemSlot.BasicItemCharacteristics.BasicData.ItemType;
            
            if ((itemType == ItemType.Armor || itemType == ItemType.Bow || 
                itemType == ItemType.Katana || itemType == ItemType.MagicCard) &&
                _checkWeapon.Invoke())
            {
                var goldCrystalCharacteristics = _itemSlotFactory.GetCharacteristics(
                    ItemType.Crystal,
                    LevelCharacteristicType.Legendary,
                    6);
                _inventoryCraftView.SlotEndCraftView.CreateItemSlotView(goldCrystalCharacteristics);
                
                return true;
            }
                
            // Debug.Log("Нет следующего айтема");
            return false;
        }

        private bool TryUpgrade(ItemType craftType)
        {
            var currentItemSlot = _inventoryCraftView.SlotUpgradeView.GetCurrentItemSlot();

            if (currentItemSlot != null && craftType == ItemType.Crystal)
            {
                Upgrade(currentItemSlot);
                return true;
            }

            return false;
        }

        private void Upgrade(ItemSlot itemSlot)
        {
            var templateItemSlot = _inventoryCraftView.SlotCraftViews[0].GetCurrentItemSlot();
            var improvedCharacteristics = templateItemSlot.BasicItemCharacteristics as CrystalCharacteristics;

            if (itemSlot.BasicItemCharacteristics is WeaponCharacteristics weaponCharacteristics)
            {
                weaponCharacteristics.WeaponCharacteristicsData.ImproveCharacteristics(
                    improvedCharacteristics.Data.ImprovementDamage,
                    improvedCharacteristics.Data.ImprovementCriticalHitChance,
                    improvedCharacteristics.Data.ImprovementCriticalDamage,
                    improvedCharacteristics.Data.ImprovementPercentageVampirism);
            }
            else if (itemSlot.BasicItemCharacteristics is ArmorCharacteristics armorCharacteristics)
            {
                armorCharacteristics.ArmorCharacteristicsData.ImproveCharacteristics(
                    improvedCharacteristics.Data.ImprovementExtraHealth,
                    improvedCharacteristics.Data.ImprovementEvasionProbability,
                    improvedCharacteristics.Data.ImprovementDamageBlock,
                    improvedCharacteristics.Data.ImprovementReflectedDamage);
            }
        }
    }
}
