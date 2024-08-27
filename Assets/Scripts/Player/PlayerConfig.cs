using Builder.Utility;
using UnityEngine;

namespace Builder.Player
{
    [CreateAssetMenu(
        fileName = nameof(PlayerConfig),
        menuName = "Custom/Game/" + nameof(PlayerConfig),
        order = 0)]
    public class PlayerConfig : ConfigBase, IPlayerConfig
    {
        [SerializeField] private float _moveVelocity;
        [SerializeField] private float _rotateSensitivity;
        [SerializeField] private string _itemTag;
        [SerializeField] private float _itemHoldDistance;
        [SerializeField] private LayerMask _raycastLayerMask;
        [SerializeField] private string _surfaceTag;

        public float MoveVelocity => _moveVelocity;
        public float RotateSensitivity => _rotateSensitivity;
        public string ItemTag => _itemTag;
        public float ItemHoldDistance => _itemHoldDistance;
        public LayerMask RaycastLayerMask => _raycastLayerMask;
        public string SurfaceTag => _surfaceTag;
    }
}