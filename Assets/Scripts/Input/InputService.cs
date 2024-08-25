using System;

namespace Builder.Input
{
    public class InputService : IInputService
    {
        private readonly IInputConfig _inputConfig;

        public InputService(IInputConfig inputConfig)
        {
            _inputConfig = inputConfig;
        }
        
        public bool IsMoveKeyPressed { get; private set; }
        public bool IsRotateClockwiseKeyPressed { get; private set; }
        public bool IsRotateCounterClockwiseKeyPressed { get; private set; }
        public event Action ShootKeyDown;
        public event Action NextWeaponKeyDown;
        public event Action PreviousWeaponKeyDown;
        
        public void Tick()
        {
            IsMoveKeyPressed = UnityEngine.Input.GetKey(_inputConfig.MoveKey);
            IsRotateClockwiseKeyPressed = UnityEngine.Input.GetKey(_inputConfig.RotateClockwiseKey);
            IsRotateCounterClockwiseKeyPressed = UnityEngine.Input.GetKey(_inputConfig.RotateCounterClockwiseKey);
            
            if (UnityEngine.Input.GetKeyDown(_inputConfig.ShootKey))
            {
                ShootKeyDown?.Invoke();
            }
            
            if (UnityEngine.Input.GetKeyDown(_inputConfig.NextWeaponKey))
            {
                NextWeaponKeyDown?.Invoke();
            }
            else if (UnityEngine.Input.GetKeyDown(_inputConfig.PreviousWeaponKey))
            {
                PreviousWeaponKeyDown?.Invoke();
            }
        }
    }
}