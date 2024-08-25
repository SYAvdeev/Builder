using System;
using Cysharp.Threading.Tasks;

namespace Builder.UI
{
    public interface IUIService
    {
        UniTask<TScreen> ShowScreen<TScreen>(
            bool isImmediate,
            Action<TScreen> initializeCallback = null,
            Action<TScreen> beforeShowCallback = null) where TScreen : UIScreen;

        UniTask HideScreen<TScreen>(bool isImmediate, Action<TScreen> beforeHideCallback = null) where TScreen : UIScreen;
    }
}