using System;

namespace Builder.Items
{
    public interface IItemModel
    {
        float CurrentRotation { get; }
        internal void SetCurrentRotation(float rotation);
        event Action<float> CurrentRotationChanged;
        ItemState CurrentState { get; }
        internal void SetCurrentState(ItemState itemState);
        event Action<ItemState> CurrentStateChanged;
        string ItemTypeName { get; }
    }
}