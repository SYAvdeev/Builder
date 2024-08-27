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
            CurrentState = itemState;
        }

        public abstract string TypeName { get; }
    }
}