namespace Builder.Items
{
    public interface IItemModel
    {
        float CurrentRotation { get; }
        internal void SetCurrentRotation(float rotation);
        ItemState CurrentState { get; }
        internal void SetCurrentState(ItemState itemState);
        string TypeName { get; }
    }
}