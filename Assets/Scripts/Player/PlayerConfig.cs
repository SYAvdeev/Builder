using System.Collections.Generic;
using Builder.Utility;
using UnityEngine;

namespace Builder.Player
{
    [CreateAssetMenu(
        fileName = nameof(PlayerConfig),
        menuName = "Custom/" + nameof(PlayerConfig),
        order = 0)]
    public class PlayerConfig : ConfigBase, IPlayerConfig
    {
        [SerializeField] private float _moveVelocity;
        [SerializeField] private float _rotateSensitivity;
        [SerializeField] private List<string> _itemTags;

        public float MoveVelocity => _moveVelocity;
        public float RotateSensitivity => _rotateSensitivity;
        public IReadOnlyCollection<string> ItemTags => _itemTags;
    }
}