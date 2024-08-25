using System.Threading;
using Cysharp.Threading.Tasks;
using Builder.UI;
using Builder.Scenes;
using VContainer.Unity;

namespace Builder.Bootstrap
{
    public class BootstrapSceneStarter : IAsyncStartable
    {
        private readonly IUIService _uiService;
        private readonly IScenesService _scenesService;

        public BootstrapSceneStarter(IUIService uiService, IScenesService scenesService)
        {
            _uiService = uiService;
            _scenesService = scenesService;
        }

        public async UniTask StartAsync(CancellationToken cancellation)
        {
            LoadingScreen loadingScreen = await _uiService.ShowScreen<LoadingScreen>(true);

            loadingScreen.SetProgress(0.5f);

            await _scenesService.LoadGameScene(false);
        }
    }
}