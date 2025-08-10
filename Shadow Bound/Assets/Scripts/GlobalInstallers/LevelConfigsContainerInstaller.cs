using LevelsSystem;
using UnityEngine;
using Zenject;

namespace GlobalInstallers
{
    public class LevelConfigsContainerInstaller : MonoInstaller
    {
        [SerializeField] private LevelConfig[] _levelConfigs;
        
        public override void InstallBindings()
        {
            Container
                .Bind<LevelConfigsContainer>()
                .AsSingle()
                .WithArguments(_levelConfigs)
                .NonLazy();
        }
    }
}
