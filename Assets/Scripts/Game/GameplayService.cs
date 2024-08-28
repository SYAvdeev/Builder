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
        private readonly UniTaskRestartable _fixedUpdateTask;

        public GameplayService(
            IPlayerService playerService, 
            IPlayerController playerController,
            IUIService uiService)
        {
            _playerService = playerService;
            _playerController = playerController;
            _uiService = uiService;
            
            _updateTask = new UniTaskRestartable(UpdateRoutine);
            _fixedUpdateTask = new UniTaskRestartable(FixedUpdateRoutine);
        }

        public async UniTask StartGameAsync()
        {
            _updateTask.StartRoutine();
            _fixedUpdateTask.StartRoutine();
            
            await _uiService.HideScreen<LoadingScreen>(true);
        }

        public void Dispose()
        {
            _updateTask.Cancel();
            _fixedUpdateTask.Cancel();
        }

        private async UniTask UpdateRoutine(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                _playerService.Update();
                
                await UniTask.Yield(PlayerLoopTiming.Update, cancellationToken);
            }
        }

        private async UniTask FixedUpdateRoutine(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                float fixedDeltaTime = Time.fixedDeltaTime;
                _playerController.FixedUpdate(fixedDeltaTime);
                
                await UniTask.Yield(PlayerLoopTiming.FixedUpdate, cancellationToken);
            }
        }
    }
}