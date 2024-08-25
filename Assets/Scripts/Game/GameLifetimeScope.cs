using Builder.Player;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Builder.Game
{
    public class GameLifetimeScope : LifetimeScope
    {
        [SerializeField] private PlayerView _playerView;
        
        protected override void Configure(IContainerBuilder builder)
        {
            ConfigurePlayer(builder);
            ConfigureGameplay(builder);
            builder.RegisterEntryPoint<GameSceneStarter>();
        }

        private void ConfigurePlayer(IContainerBuilder builder)
        {
            builder.RegisterInstance(_playerView);
            builder.Register<PlayerModel>(Lifetime.Scoped).As<IPlayerModel>();
            builder.Register<PlayerService>(Lifetime.Scoped).As<IPlayerService>();
            builder.Register<PlayerController>(Lifetime.Scoped).As<IPlayerController>();
        }

        private void ConfigureGameplay(IContainerBuilder builder)
        {
            builder.Register<GameplayService>(Lifetime.Scoped).As<IGameplayService>();
        }
    }
}
