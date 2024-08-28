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
        
        public bool IsMoveForwardPressed { get; private set; }
        public bool IsMoveBackPressed { get; private set; }
        public bool IsMoveRightPressed { get; private set; }
        public bool IsMoveLeftPressed { get; private set; }
        public float RotateHorizontalAmount { get; private set; }
        public float RotateVerticalAmount { get; private set; }
        public event Action ActionKeyDown;
        public event Action RotateItemClockwise;
        public event Action RotateItemCounterclockwise;

        public void Tick()
        {
            IsMoveForwardPressed = UnityEngine.Input.GetKey(_inputConfig.MoveForwardKey);
            IsMoveBackPressed = UnityEngine.Input.GetKey(_inputConfig.MoveBackKey);
            IsMoveRightPressed = UnityEngine.Input.GetKey(_inputConfig.MoveRightKey);
            IsMoveLeftPressed = UnityEngine.Input.GetKey(_inputConfig.MoveLeftKey);
            RotateHorizontalAmount = UnityEngine.Input.GetAxis(_inputConfig.RotateHorizontalAxis);
            RotateVerticalAmount = -UnityEngine.Input.GetAxis(_inputConfig.RotateVerticalAxis);
            
            if (UnityEngine.Input.GetKeyDown(_inputConfig.ActionKey))
            {
                ActionKeyDown?.Invoke();
            }
            
            if (UnityEngine.Input.mouseScrollDelta.y > 0f)
            {
                RotateItemClockwise?.Invoke();
            }
            else if (UnityEngine.Input.mouseScrollDelta.y < 0f)
            {
                RotateItemCounterclockwise?.Invoke();
            }
        }
    }
}