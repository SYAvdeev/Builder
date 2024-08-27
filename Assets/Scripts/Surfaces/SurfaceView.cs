using UnityEngine;

namespace Builder.Surfaces
{
    public class SurfaceView : MonoBehaviour
    {
        [SerializeField] private SurfaceType _surfaceType;
        [SerializeField] private Transform _itemsParent;

        public SurfaceType SurfaceType => _surfaceType;
        public Transform ItemsParent => _itemsParent;
    }
}