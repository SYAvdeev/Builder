using System;

namespace Builder.Player
{
    public interface IPlayerService : IDisposable
    {
        void Initialize();
        IPlayerModel Model { get; }
        void Update(float deltaTime);
    }
}