using System.Collections.Generic;
using UnityEngine;

namespace Builder.Items
{
    public interface IItemsConfig
    {
        Color AllowedColor { get; }
        Color ForbiddenColor { get; }
        Material ItemsMaterial { get; }
        Material ItemInFocusMaterial { get; }
        string InactiveItemsLayerName { get; }
        string DraggingItemsLayerName { get; }
        string ItemTag { get; }
        public IReadOnlyCollection<ItemTypesForSurface> ItemTagsForSurfaces { get; }
    }
}