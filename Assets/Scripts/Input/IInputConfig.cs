using UnityEngine;

namespace Builder.Input
{
    public interface IInputConfig
    {
        KeyCode MoveKey { get; }
        KeyCode RotateClockwiseKey { get; }
        KeyCode RotateCounterClockwiseKey { get; }
        KeyCode ShootKey { get; }
        KeyCode NextWeaponKey { get; }
        KeyCode PreviousWeaponKey { get; }
    }
}