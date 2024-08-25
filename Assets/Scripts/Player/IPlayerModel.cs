using System;
using UnityEngine;

namespace Builder.Player
{
    public interface IPlayerModel
    {
        IPlayerConfig Config { get; }
        Vector3 CurrentMovement { get; }
        internal void SetCurrentMovement(Vector3 movement);
        event Action<Vector3> CurrentMovementChanged;
        Vector2 CurrentRotation { get; }
        internal void AddCurrentRotation(Vector2 rotation);
        event Action<Vector2> CurrentRotationChanged;
    }
}