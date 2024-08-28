using System;
using UnityEngine;

namespace Builder.Items
{
    public abstract class ItemModel : IItemModel
    {
        public float CurrentRotation { get; private set; }
        
        void IItemModel.SetCurrentRotation(float rotation)
        {
            if (Mathf.Approximately(CurrentRotation, rotation))
            {
                return;
            }
            
            CurrentRotation = rotation;
            CurrentRotationChanged?.Invoke(rotation);
        }

        public event Action<float> CurrentRotationChanged;

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

        public abstract string ItemTypeName { get; }
    }
}