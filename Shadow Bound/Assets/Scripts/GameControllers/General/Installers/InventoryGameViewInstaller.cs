using InventoriesControllers.InventoriesViews;
using Zenject;

namespace GameControllers.General.Installers
{
    public class InventoryGameViewInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container
                .Bind<InventoryGameView>()
                .FromComponentsInHierarchy()
                .AsSingle()
                .NonLazy();
        }
    }
}
