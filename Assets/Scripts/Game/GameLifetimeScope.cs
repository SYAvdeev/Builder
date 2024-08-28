using Builder.Items.Level;
using Builder.Player;
using UnityEngine;
using UnityEngine.Serialization;
using VContainer;
using VContainer.Unity;

namespace Builder.Game
{
    public class GameLifetimeScope : LifetimeScope
    {
        [SerializeField] private PlayerView _playerView;
        [FormerlySerializedAs("_itemsCollectionView")] [SerializeField] private LevelView LevelView;
        
        protected override void Configure(IContainerBuilder builder)
        {
            ConfigurePlayer(builder);
            ConfigureItems(builder);
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

        private void ConfigureItems(IContainerBuilder builder)
        {
            builder.RegisterInstance(LevelView);
            builder.Register<LevelController>(Lifetime.Scoped).As<ILevelController>();
        }

        private void ConfigureGameplay(IContainerBuilder builder)
        {
            builder.Register<GameplayService>(Lifetime.Scoped).As<IGameplayService>();
        }
    }
}
