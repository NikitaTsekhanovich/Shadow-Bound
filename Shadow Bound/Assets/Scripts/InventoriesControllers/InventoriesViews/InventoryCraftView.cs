using System;
using System.Collections.Generic;
using InventoriesControllers.SlotsViews;
using UnityEngine;

namespace InventoriesControllers.InventoriesViews
{
    public class InventoryCraftView : MonoBehaviour
    {
        private Action _clickCraftOrUpgrade;
        
        [field: SerializeField] public SlotCraftView[] SlotCraftViews { get; private set; }
        [field: SerializeField] public SlotUpgradeView SlotUpgradeView { get; private set; }
        [field: SerializeField] public SlotEndCraftView SlotEndCraftView { get; private set; }

        public readonly List<ISlotView> SlotViews = new ();

        public void InitializeSlotViews()
        {
            foreach (var slotCraftView in SlotCraftViews)
                SlotViews.Add(slotCraftView);
            SlotViews.Add(SlotUpgradeView);
            SlotViews.Add(SlotEndCraftView);
        }

        public void SetCraftOrUpgradeAction(Action clickCraftOrUpgrade)
        {
            _clickCraftOrUpgrade = clickCraftOrUpgrade;
        }

        public void ClickCraftOrUpgrade()
        {
            _clickCraftOrUpgrade?.Invoke();
        }
    }
}
