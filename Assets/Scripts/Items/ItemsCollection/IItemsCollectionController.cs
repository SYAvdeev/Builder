using System;

namespace Builder.Items.ItemsCollection
{
    public interface IItemsCollectionController : IDisposable
    {
        void InitializeItems();
    }
}