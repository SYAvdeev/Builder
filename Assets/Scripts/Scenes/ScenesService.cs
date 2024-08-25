using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace Builder.Scenes
{
    public class ScenesService : IScenesService
    {
        private readonly IScenesConfig _scenesConfig;
        private readonly IScenesModel _scenesModel;

        public ScenesService(IScenesConfig scenesConfig, IScenesModel scenesModel)
        {
            _scenesConfig = scenesConfig;
            _scenesModel = scenesModel;
        }

        public async UniTask LoadScene(int sceneIndex, bool unloadCurrentScene = true)
        {
            if (unloadCurrentScene)
            {
                await SceneManager.UnloadSceneAsync(_scenesModel.CurrentSceneIndex);
            }

            _scenesModel.CurrentSceneIndex = sceneIndex;
            await SceneManager.LoadSceneAsync(sceneIndex, LoadSceneMode.Additive);
        }
        
        public async UniTask LoadGameScene(bool unloadCurrentScene = true)
        {
            await LoadScene(_scenesConfig.GameSceneIndex, unloadCurrentScene);
        }
    }
}