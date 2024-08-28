using UnityEngine;

namespace Builder.Items.ItemStand
{
    public interface IItemStandController
    {
        void Initialize();
        bool CanPutItem(IItemController itemController);
        void PutItem(IItemController itemController, Vector3 position);
        void RemoveCurrentItem();
    }
}