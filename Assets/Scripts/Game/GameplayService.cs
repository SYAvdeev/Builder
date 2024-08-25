using System.Threading;
using Builder.Player;
using Cysharp.Threading.Tasks;
using Builder.UI;
using Builder.Utility;
using UnityEngine;

namespace Builder.Game
{
    public class GameplayService : IGameplayService
    {
        private readonly IPlayerService _playerService;
        private readonly IPlayerController _playerController;
        private readonly IUIService _uiService;
        
        private readonly UniTaskRestartable _updateTask;

        public GameplayService(
            IPlayerService playerService, 
            IPlayerController playerController,
            IUIService uiService)
        {
            _playerService = playerService;
            _playerController = playerController;
            _uiService = uiService;
            
            _updateTask = new UniTaskRestartable(UpdateRoutine);
        }

        public async UniTask StartGameAsync()
        {
            _updateTask.StartRoutine();
            
            await _uiService.HideScreen<LoadingScreen>(true);
        }

        public void Dispose()
        {
            _updateTask.Cancel();
        }

        private async UniTask UpdateRoutine(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                float deltaTime = Time.deltaTime;
                float fixedDeltaTime = Time.fixedDeltaTime;

                _playerService.Update(deltaTime);
                _playerController.FixedUpdate(fixedDeltaTime);
                
                await UniTask.Yield(cancellationToken);
            }
        }
    }
}