using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Builder.UI
{
    public abstract class UIScreen : MonoBehaviour, IUIScreen
    {
        public event Action<IUIScreen> ShowStarted;
        public event Action<IUIScreen> ShowFinished;
        public event Action<IUIScreen> HideStarted;
        public event Action<IUIScreen> HideFinished;

        public async UniTask Show(bool isImmediate)
        {
            ShowStarted?.Invoke(this);
            await ShowInternal(isImmediate);
            ShowFinished?.Invoke(this);
        }

        public async UniTask Hide(bool isImmediate)
        {
            HideStarted?.Invoke(this);
            await HideInternal(isImmediate);
            HideFinished?.Invoke(this);
        }
        
        protected abstract UniTask ShowInternal(bool isImmediate);
        protected abstract UniTask HideInternal(bool isImmediate);
    }
}