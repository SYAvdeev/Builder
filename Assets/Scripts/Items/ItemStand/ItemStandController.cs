using UnityEngine;

namespace Builder.Items.ItemStand
{
    public abstract class ItemStandController : IItemStandController
    {
        private readonly ItemStandView _itemStandView;
        private readonly IItemStandModel _itemStandModel;

        protected ItemStandController(ItemStandView itemStandView, IItemStandModel itemStandModel)
        {
            _itemStandView = itemStandView;
            _itemStandModel = itemStandModel;
        }

        public virtual void Initialize()
        {
            _itemStandView.SetItemStandController(this);
        }

        public abstract bool CanPutItem(IItemController itemController);

        public virtual void PutItem(IItemController itemController, Vector3 position)
        {
            itemController.ItemView.transform.SetParent(_itemStandView.ItemsParent);
            itemController.ItemView.transform.position = position;
            itemController.OnPutOnStand(_itemStandModel.ItemStandTypeName);
        }

        public abstract void RemoveCurrentItem();
    }
}