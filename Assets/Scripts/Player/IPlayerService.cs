using System;

namespace Builder.Player
{
    public interface IPlayerService : IDisposable
    {
        event Action ActionTaken;
        void Initialize();
        IPlayerModel Model { get; }
        void Update(float deltaTime);
    }
}