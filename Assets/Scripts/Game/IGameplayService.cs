using System;
using Cysharp.Threading.Tasks;

namespace Builder.Game
{
    public interface IGameplayService : IDisposable
    {
        UniTask StartGameAsync();
    }
}