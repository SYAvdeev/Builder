using System;

namespace Builder.Items
{
    public interface IItemController : IDisposable
    {
        public void Initialize();
        public void OnPutOnStand(string standTypeName);
        public void SetInFocus();
        public void RemoveFromFocus();
        bool RequestDrag();
        bool RequestPut();
        void Rotate(bool isClockwise);
        IItemModel ItemModel { get; }
        ItemView ItemView { get; }
    }
}