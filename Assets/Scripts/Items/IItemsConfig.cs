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
        string PlayerItemStandName { get; }
        float RotationDelta { get; }
    }
}