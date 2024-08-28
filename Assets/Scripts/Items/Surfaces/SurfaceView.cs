using Builder.Items.ItemStand;
using UnityEngine;

namespace Builder.Items.Surfaces
{
    public class SurfaceView : ItemStandView
    {
        [SerializeField] private SurfaceType _surfaceType;

        public SurfaceType SurfaceType => _surfaceType;
    }
}