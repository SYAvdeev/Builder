using System;

namespace Builder.Items
{
    public abstract class ItemModel : IItemModel
    {
        public float CurrentRotation { get; private set; }
        
        void IItemModel.SetCurrentRotation(float rotation)
        {
            CurrentRotation = rotation;
        }

        public ItemState CurrentState { get; private set; } = ItemState.Inactive;
        
        void IItemModel.SetCurrentState(ItemState itemState)
        {
            if (CurrentState == itemState)
            {
                return;
            }
            
            CurrentState = itemState;
            CurrentStateChanged?.Invoke(itemState);
        }

        public event Action<ItemState> CurrentStateChanged;

        public abstract string TypeName { get; }
    }
}