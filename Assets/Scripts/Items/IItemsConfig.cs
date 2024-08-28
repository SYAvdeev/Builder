using UnityEngine;

namespace Builder.Items
{
    public interface IItemsConfig
    {
        Material NeutralMaterial { get; }
        Material AllowedMaterial { get; }
        Material ForbiddenMaterial { get; }
        string InactiveItemsLayerName { get; }
        string DraggingItemsLayerName { get; }
        string PlayerItemStandName { get; }
        float RotationDelta { get; }
    }
}