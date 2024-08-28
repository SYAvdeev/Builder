using System;

namespace Builder.Player
{
    public interface IPlayerService : IDisposable
    {
        event Action ActionTaken;
        event Action ItemRotatedClockwise;
        event Action ItemRotatedCounterclockwise;
        void Initialize();
        IPlayerModel Model { get; }
        void Update();
    }
}