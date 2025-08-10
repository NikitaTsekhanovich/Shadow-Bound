using Factories.Properties;
using Factories.Types;
using InventoriesControllers.ItemSlotViews;
using UnityEngine;
using Zenject;

namespace Factories.Installers
{
    public class ItemSlotGameViewFactoryInstaller : MonoInstaller
    {
        [SerializeField] private ItemSlotGameView _itemSlotGameViewPrefab;
        
        [Inject] private ICanGetItemSlot _itemSlotFactory;
        
        public override void InstallBindings()
        {
            Container
                .Bind<ICanGetItemSlotGameView>()
                .To<ItemSlotGameViewFactory>()
                .AsSingle()
                .WithArguments(
                    _itemSlotGameViewPrefab,
                    _itemSlotFactory)
                .NonLazy();
        }
    }
}
