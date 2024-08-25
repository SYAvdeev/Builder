using System.Threading;
using Builder.Player;
using Cysharp.Threading.Tasks;
using VContainer.Unity;

namespace Builder.Game
{
    public class GameSceneStarter : IAsyncStartable
    {
        private readonly IPlayerService _playerService;
        private readonly IPlayerController _playerController;
        private readonly IGameplayService _gameplayService;

        public GameSceneStarter(
            IPlayerService playerService, 
            IPlayerController playerController,
            IGameplayService gameplayService)
        {
            _playerService = playerService;
            _playerController = playerController;
            _gameplayService = gameplayService;
        }

        public async UniTask StartAsync(CancellationToken cancellation)
        {
            _playerService.Initialize();
            _playerController.Initialize();

            await _gameplayService.StartGameAsync();
        }
    }
}