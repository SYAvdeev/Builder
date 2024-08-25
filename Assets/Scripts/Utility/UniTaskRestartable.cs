using System;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace Builder.Utility
{
    public class UniTaskRestartable
    {
        private readonly Func<CancellationToken, UniTask> _routine;
        
        private CancellationTokenSource _currentCancellationTokenSource;
        private UniTask? _currentSwitchTask;

        public UniTaskRestartable(Func<CancellationToken, UniTask> routine)
        {
            _routine = routine;
        }
        public void StartRoutine()
        {
            _currentCancellationTokenSource = new CancellationTokenSource();
            _currentSwitchTask = _routine.Invoke(_currentCancellationTokenSource.Token);
        }

        public void Cancel()
        {
            if (_currentSwitchTask is { Status: UniTaskStatus.Pending } && !_currentCancellationTokenSource.IsCancellationRequested)
            {
                _currentCancellationTokenSource.Cancel();
                _currentCancellationTokenSource.Dispose();
            }
        }
    
        public void Restart()
        {
            Cancel();
            StartRoutine();
        }
    }
}