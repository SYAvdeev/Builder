using System;

namespace Builder.Items.Level
{
    public interface ILevelController : IDisposable
    {
        void InitializeItems();
    }
}