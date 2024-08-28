using Builder.Items.ItemStand;
using UnityEngine;

namespace Builder.Items.Cube
{
    public class CubeController : ItemController, IItemStandController
    {
        private CubeController _currentInstalledCube;

        public CubeController(CubeView itemView, ICubeModel itemModel, IItemsConfig itemsConfig) 
            : base(itemView, itemModel, itemsConfig) { }

        private ICubeModel CubeModel => (ICubeModel)ItemModel;
        private CubeView CubeView => (CubeView)ItemView;

        private bool AllowedToDrag => _currentInstalledCube == null && ItemModel.CurrentState == ItemState.InFocus;

        public override void Initialize()
        {
            base.Initialize();
            CubeView.ItemStandView.SetItemStandController(this);
        }

        public override void SetInFocus()
        {
            var inFocusMaterial = _itemsConfig.ItemInFocusMaterial;
            ItemView.MeshRenderer.sharedMaterial = inFocusMaterial;
            ItemModel.SetCurrentState(ItemState.InFocus);
            inFocusMaterial.color = AllowedToDrag ? _itemsConfig.AllowedColor : _itemsConfig.ForbiddenColor;
        }

        public override bool RequestDrag()
        {
            if (!AllowedToDrag)
            {
                return false;
            }

            ItemView.gameObject.layer = LayerMask.NameToLayer(_itemsConfig.DraggingItemsLayerName);
            ItemModel.SetCurrentState(ItemState.DraggingByPlayer);
            return true;
        }

        public bool CanPutItem(IItemController itemController)
        {
            return itemController is CubeController && _currentInstalledCube == null;
        }

        public void PutItem(IItemController itemController, Vector3 position)
        {
            itemController.ItemView.transform.SetParent(CubeView.ItemStandView.ItemsParent);
            itemController.ItemView.transform.position = CubeView.ItemStandView.ItemsParent.position;
            itemController.OnPutOnStand(CubeModel.ItemStandTypeName);

            if (_currentInstalledCube == itemController)
            {
                return;
            }
            
            _currentInstalledCube = (CubeController)itemController;
            itemController.ItemModel.CurrentStateChanged += ItemModelOnCurrentStateChanged;
        }

        private void ItemModelOnCurrentStateChanged(ItemState itemState)
        {
            if (itemState == ItemState.DraggingByPlayer)
            {
                RemoveCurrentItem();
            }
        }

        public void RemoveCurrentItem()
        {
            if (_currentInstalledCube == null)
            {
                return;
            }
            
            _currentInstalledCube.ItemModel.CurrentStateChanged -= ItemModelOnCurrentStateChanged;
            _currentInstalledCube = null;
        }
    }
}