using Builder.Utility;
using UnityEngine;
using VContainer;

namespace Builder.Root
{
    [CreateAssetMenu(fileName = nameof(ConfigsRepository), menuName = "Custom/" + nameof(ConfigsRepository))]
    public sealed class ConfigsRepository : ScriptableObject
    {
        [SerializeField] private ConfigBase[] _configs;

        public void Configure(IContainerBuilder builder)
        {
            foreach (ConfigBase configScriptableObject in _configs)
            {
                if (configScriptableObject == null) continue; // Check missing reference

                builder.RegisterInstance(configScriptableObject).AsImplementedInterfaces().As(configScriptableObject.GetType());
            }
        }
    }
}