using System;

namespace Builder.Player
{
    public interface IPlayerController : IDisposable
    {
        void Initialize();
        void FixedUpdate(float fixedDeltaTime);
    }
}