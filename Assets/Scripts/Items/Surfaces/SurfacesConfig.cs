using System.Collections.Generic;
using Builder.Utility;
using UnityEngine;

namespace Builder.Items.Surfaces
{
    [CreateAssetMenu(
        fileName = nameof(SurfacesConfig),
        menuName = "Custom/Game/" + nameof(SurfacesConfig),
        order = 2)]
    public class SurfacesConfig : ConfigBase, ISurfacesConfig
    {
        [SerializeField] private List<ItemTypesForSurface> _itemTagsForSurfaces;

        public IReadOnlyCollection<ItemTypesForSurface> ItemTagsForSurfaces => _itemTagsForSurfaces;
    }
}