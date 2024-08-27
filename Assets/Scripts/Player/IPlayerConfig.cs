using UnityEngine;

namespace Builder.Player
{
    public interface IPlayerConfig
    {
        float MoveVelocity { get; }
        float RotateSensitivity { get; }
        string ItemTag { get; }
        string SurfaceTag { get; }
        float ItemHoldDistance { get; }
        LayerMask RaycastLayerMask { get; }
    }
}