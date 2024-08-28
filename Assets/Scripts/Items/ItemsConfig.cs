using Builder.Utility;
using UnityEngine;

namespace Builder.Items
{
    [CreateAssetMenu(
        fileName = nameof(ItemsConfig),
        menuName = "Custom/Game/" + nameof(ItemsConfig),
        order = 1)]
    public class ItemsConfig : ConfigBase, IItemsConfig
    {
        [SerializeField] private Material _neutralMaterial;
        [SerializeField] private Material _allowedMaterial;
        [SerializeField] private Material _forbiddenMaterial;
        [SerializeField] private string _draggingItemsLayerName;
        [SerializeField] private string _inactiveItemsLayerName;
        [SerializeField] private string _playerItemStandName;
        [SerializeField] private float _rotationDelta;

        public Material NeutralMaterial => _neutralMaterial;

        public Material AllowedMaterial => _allowedMaterial;

        public Material ForbiddenMaterial => _forbiddenMaterial;

        public string InactiveItemsLayerName => _inactiveItemsLayerName;

        public string DraggingItemsLayerName => _draggingItemsLayerName;

        public string PlayerItemStandName => _playerItemStandName;

        public float RotationDelta => _rotationDelta;
    }
}