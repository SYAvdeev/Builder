using System.Collections.Generic;
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
        [SerializeField] private Color _allowedColor;
        [SerializeField] private Color _forbiddenColor;
        [SerializeField] private Material _itemsMaterial;
        [SerializeField] private Material _itemInFocusMaterial;
        [SerializeField] private List<ItemTypesForSurface> _itemTagsForSurfaces;
        [SerializeField] private string _itemTag;
        [SerializeField] private string _draggingItemsLayerName;
        [SerializeField] private string _inactiveItemsLayerName;
        [SerializeField] private string _surfaceTag;

        public Color AllowedColor => _allowedColor;

        public Color ForbiddenColor => _forbiddenColor;

        public Material ItemsMaterial => _itemsMaterial;

        public Material ItemInFocusMaterial => _itemInFocusMaterial;

        public string InactiveItemsLayerName => _inactiveItemsLayerName;

        public string DraggingItemsLayerName => _draggingItemsLayerName;

        public string ItemTag => _itemTag;

        public string SurfaceTag => _surfaceTag;

        public IReadOnlyCollection<ItemTypesForSurface> ItemTagsForSurfaces => _itemTagsForSurfaces;
    }
}