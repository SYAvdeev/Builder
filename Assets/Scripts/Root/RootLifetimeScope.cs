using Builder.Scenes;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Builder.Root
{
    public class RootLifetimeScope : LifetimeScope
    {
        [SerializeField] private ConfigsRepository _configsRepository;

        protected override void Configure(IContainerBuilder builder)
        {
            _configsRepository.Configure(builder);
            ConfigureScenes(builder);
        }
        
        private void ConfigureScenes(IContainerBuilder builder)
        {
            builder.Register<ScenesModel>(Lifetime.Singleton).As<IScenesModel>();
            builder.Register<ScenesService>(Lifetime.Singleton).As<IScenesService>();
        }
    }
}