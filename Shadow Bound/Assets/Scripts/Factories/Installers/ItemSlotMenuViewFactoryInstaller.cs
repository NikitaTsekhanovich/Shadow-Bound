using Factories.Properties;
using Factories.Types;
using InventoriesControllers;
using InventoriesControllers.ItemSlotViews;
using UnityEngine;
using Zenject;

namespace Factories.Installers
{
    public class ItemSlotMenuViewFactoryInstaller : MonoInstaller
    {
        [SerializeField] private ItemSlotMenuView _itemSlotMenuViewPrefab;
        [SerializeField] private Canvas _canvas;
        [SerializeField] private Transform _itemSlotBuffer;
        [SerializeField] private DescriptionItemController _descriptionItemController;

        [Inject] private ICanGetItemSlot _itemSlotFactory;
        
        public override void InstallBindings()
        {
            Container
                .Bind<ICanGetItemSlotMenuView>()
                .To<ItemSlotMenuViewFactory>()
                .AsSingle()
                .WithArguments(
                    _itemSlotMenuViewPrefab,
                    _canvas,
                    _itemSlotBuffer,
                    _itemSlotFactory,
                    _descriptionItemController)
                .NonLazy();
        }
    }
}
