using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Builder.UI
{
    public class UIService : IUIService
    {
        private readonly IUIModel _uiModel;
        private readonly IUIConfig _uiConfig;
        private readonly UIViewRoot _uiViewRoot;
        private readonly IObjectResolver _objectResolver;

        public UIService(IUIModel uiModel, IUIConfig uiConfig, UIViewRoot uiViewRoot, IObjectResolver objectResolver)
        {
            _uiModel = uiModel;
            _uiConfig = uiConfig;
            _uiViewRoot = uiViewRoot;
            _objectResolver = objectResolver;
        }

        public async UniTask<TScreen> ShowScreen<TScreen>(
            bool isImmediate,
            Action<TScreen> initializeCallback = null,
            Action<TScreen> beforeShowCallback = null) where TScreen : UIScreen
        {
            string screenName = typeof(TScreen).FullName;
            if (_uiModel.CurrentOpenedScreens.TryGetValue(screenName, out var openedScreen))
            {
                return (TScreen)openedScreen;
            }

            if (!_uiModel.ScreenPool.TryGet(screenName, out IUIScreen screen))
            {
                TScreen screenPrefab = _uiConfig.GetUIPrefabByType<TScreen>();
                screen = _objectResolver.Instantiate(screenPrefab, _uiViewRoot.UIViewsParent);
                initializeCallback?.Invoke((TScreen)screen);
            }

            beforeShowCallback?.Invoke((TScreen)screen);
            await screen.Show(isImmediate);
            _uiModel.CurrentOpenedScreens.Add(screenName, screen);
            return (TScreen)screen;
        }

        public async UniTask HideScreen<TScreen>(bool isImmediate, Action<TScreen> beforeHideCallback = null) where TScreen : UIScreen
        {
            string screenName = typeof(TScreen).FullName;
            if (_uiModel.CurrentOpenedScreens.TryGetValue(screenName, out IUIScreen screen))
            {
                beforeHideCallback?.Invoke((TScreen)screen);
                await screen.Hide(isImmediate);
                _uiModel.CurrentOpenedScreens.Remove(screenName);
                _uiModel.ScreenPool.Add(screenName, screen);
            }
        }

        public static Vector2 WorldToRectTransformPoint(Vector3 position, RectTransform rectTransform)
        {
            Vector2 worldToScreenPoint = RectTransformUtility.WorldToScreenPoint(Camera.main, position);
            RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, worldToScreenPoint, null, out Vector2 localPoint);
            return localPoint;
        }
    }
}