using Factories.Properties;
using Factories.Types;
using GameControllers.Models.Configs;
using UnityEngine;
using Zenject;

namespace GlobalInstallers
{
    public class ItemSlotFactoryInstaller : MonoInstaller
    {
        [SerializeField] private ItemSlotConfig[] _itemSlotConfigs;
        
        public override void InstallBindings()
        {
            Container
                .Bind<ICanGetItemSlot>()
                .To<ItemSlotFactory>()
                .AsSingle()
                .WithArguments(_itemSlotConfigs)
                .NonLazy();
        }
    }
}
