using System;
using Builder.Items;
using Builder.Items.ItemStand;

namespace Builder.Player
{
    public interface IPlayerController : IItemStandController, IDisposable
    {
        void FixedUpdate(float fixedDeltaTime);
        IItemController CurrentItemInFocus { get; }
        internal void SetCurrentItemInFocus(IItemController itemController);
        IItemStandController CurrentItemStandInFocus { get; }
        internal void SetCurrentItemStandInFocus(IItemStandController currentItemStandInFocus);
        void SetIdleState();
        void SetBuildingState();
    }
}