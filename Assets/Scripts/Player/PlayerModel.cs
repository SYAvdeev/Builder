using System;
using UnityEngine;

namespace Builder.Player
{
    public class PlayerModel : IPlayerModel
    {
        public IPlayerConfig Config { get; }

        public PlayerModel(IPlayerConfig config)
        {
            Config = config;
        }
        public Vector3 CurrentMovement { get; private set; } = Vector3.zero;
        void IPlayerModel.SetCurrentMovement(Vector3 movement)
        {
            if (CurrentMovement == movement)
            {
                return;
            }
            
            CurrentMovement = movement;
            CurrentMovementChanged?.Invoke(movement);
        }
        public event Action<Vector3> CurrentMovementChanged;
        public Vector2 CurrentRotation { get; private set; } = Vector2.zero;
        void IPlayerModel.AddCurrentRotation(Vector2 rotation)
        {
            if (Mathf.Approximately(rotation.x, 0f) && Mathf.Approximately(rotation.y, 0f))
            {
                return;
            }
            
            CurrentRotation += rotation;
            var currentRotation = CurrentRotation;
            currentRotation.x = Mathf.Clamp(CurrentRotation.x, -90f, 90f);
            CurrentRotation = currentRotation;
            CurrentRotationChanged?.Invoke(rotation);
        }
        public event Action<Vector2> CurrentRotationChanged;
        public PlayerState CurrentState { get; private set; } = PlayerState.Idle;
        void IPlayerModel.SetCurrentState(PlayerState playerState)
        {
            CurrentState = playerState;
        }

        public string ItemStandTypeName => "Player";
    }
}