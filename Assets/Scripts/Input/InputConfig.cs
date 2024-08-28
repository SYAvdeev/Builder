using Builder.Utility;
using UnityEngine;

namespace Builder.Input
{
    [CreateAssetMenu(
        fileName = nameof(InputConfig), 
        menuName = "Custom/Input/" + nameof(InputConfig),
        order = 0)]
    public class InputConfig : ConfigBase, IInputConfig
    {
        [SerializeField] private KeyCode _moveForwardKey;
        [SerializeField] private KeyCode _moveBackKey;
        [SerializeField] private KeyCode _moveRightKey;
        [SerializeField] private KeyCode _moveLeftKey;
        [SerializeField] private KeyCode _actionKey;
        [SerializeField] private string _rotateHorizontalAxis;
        [SerializeField] private string _rotateVerticalAxis;

        public KeyCode MoveForwardKey => _moveForwardKey;

        public KeyCode MoveBackKey => _moveBackKey;

        public KeyCode MoveRightKey => _moveRightKey;

        public KeyCode MoveLeftKey => _moveLeftKey;

        public string RotateHorizontalAxis => _rotateHorizontalAxis;

        public string RotateVerticalAxis => _rotateVerticalAxis;

        public KeyCode ActionKey => _actionKey;
    }
}