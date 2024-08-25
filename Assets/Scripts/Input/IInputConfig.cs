using UnityEngine;

namespace Builder.Input
{
    public interface IInputConfig
    {
        KeyCode MoveForwardKey { get; }
        KeyCode MoveBackKey { get; }
        KeyCode MoveRightKey { get; }
        KeyCode MoveLeftKey { get; }
        string RotateHorizontalAxis { get; }
        string RotateVerticalAxis { get; }
        float RotateSensitivity { get; }
        KeyCode ActionKey { get; }
    }
}