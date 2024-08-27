using System.Threading;
using Builder.Items.ItemsCollection;
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
        private readonly IItemsCollectionController _itemsCollectionController;

        public GameSceneStarter(
            IPlayerService playerService,
            IPlayerController playerController,
            IGameplayService gameplayService,
            IItemsCollectionController itemsCollectionController)
        {
            _playerService = playerService;
            _playerController = playerController;
            _gameplayService = gameplayService;
            _itemsCollectionController = itemsCollectionController;
        }

        public async UniTask StartAsync(CancellationToken cancellation)
        {
            _playerService.Initialize();
            _playerController.Initialize();
            _itemsCollectionController.InitializeItems();

            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

            await _gameplayService.StartGameAsync();
        }
    }
}