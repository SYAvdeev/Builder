using System.Threading;
using Builder.Items.Level;
using Builder.Player;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer.Unity;

namespace Builder.Game
{
    public class GameSceneStarter : IAsyncStartable
    {
        private readonly IPlayerService _playerService;
        private readonly IPlayerController _playerController;
        private readonly IGameplayService _gameplayService;
        private readonly ILevelController _levelController;

        public GameSceneStarter(
            IPlayerService playerService,
            IPlayerController playerController,
            IGameplayService gameplayService,
            ILevelController levelController)
        {
            _playerService = playerService;
            _playerController = playerController;
            _gameplayService = gameplayService;
            _levelController = levelController;
        }

        public async UniTask StartAsync(CancellationToken cancellation)
        {
            _playerService.Initialize();
            _playerController.Initialize();
            _levelController.InitializeItems();

            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

            await _gameplayService.StartGameAsync();
        }
    }
}