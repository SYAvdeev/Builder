using Builder.Utility;
using UnityEngine;

namespace Builder.Scenes
{
    [CreateAssetMenu(fileName = nameof(ScenesConfig), menuName = "Custom/Scenes/" + nameof(ScenesConfig), order = 1)]
    public class ScenesConfig : ConfigBase, IScenesConfig
    {
        [SerializeField] private int _gameSceneIndex;

        public int GameSceneIndex => _gameSceneIndex;
    }
}