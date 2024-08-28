using System;
using VContainer.Unity;

namespace Builder.Input
{
    public interface IInputService : ITickable
    {
        bool IsMoveForwardPressed { get; }
        bool IsMoveBackPressed { get; }
        bool IsMoveRightPressed { get; }
        bool IsMoveLeftPressed { get; }
        float RotateHorizontalAmount { get; }
        float RotateVerticalAmount { get; }
        event Action ActionKeyDown;
        event Action RotateItemClockwise;
        event Action RotateItemCounterclockwise;
    }
}