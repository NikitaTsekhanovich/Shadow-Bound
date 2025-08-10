using GameControllers.Models;

namespace GameControllers.Controllers.Properties
{
    public interface IUsableItem
    {
        public void InitUnitUsingItem(Ninja ninja);
        public void Use();
    }
}
