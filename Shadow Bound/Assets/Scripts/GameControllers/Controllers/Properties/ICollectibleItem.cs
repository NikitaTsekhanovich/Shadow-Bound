using InventoriesControllers;

namespace GameControllers.Controllers.Properties
{
    public interface ICollectibleItem
    {
        public ItemSlot ItemSlot { get; }
        public void Collect();
    }
}
