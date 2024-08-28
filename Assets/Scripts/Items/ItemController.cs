using System.Collections.Generic;
using Builder.Items.ItemStand;
using UnityEngine;

namespace Builder.Items
{
    public abstract class ItemController : IItemController
    {
        protected readonly IItemsConfig _itemsConfig;

        private readonly IList<GameObject> _gameObjectsInCollision = new List<GameObject>();

        public IItemModel ItemModel { get; }
        public ItemView ItemView { get; }

        protected ItemController(ItemView itemView, IItemModel itemModel, IItemsConfig itemsConfig)
        {
            ItemView = itemView;
            ItemModel = itemModel;
            _itemsConfig = itemsConfig;
        }

        public virtual void Initialize()
        {
            ItemView.SetItemController(this);
            ItemView.CollisionEnter += ItemViewOnCollisionEnter;
            ItemView.CollisionExit += ItemViewOnCollisionExit;
            ItemModel.CurrentStateChanged += ItemModelOnCurrentStateChanged;
        }

        private void ItemModelOnCurrentStateChanged(ItemState itemState)
        {
            if (ItemModel.CurrentState != ItemState.DraggingByPlayer &&
                ItemModel.CurrentState != ItemState.DraggingOnStand)
            {
                return;
            }
            
            UpdateDraggingColor();
        }

        private void ItemViewOnCollisionEnter(GameObject gameObject)
        {
            if (ItemModel.CurrentState is ItemState.DraggingByPlayer or ItemState.DraggingOnStand)
            {
                _gameObjectsInCollision.Add(gameObject);
            }
        }

        private void ItemViewOnCollisionExit(GameObject gameObject)
        {
            if (ItemModel.CurrentState is ItemState.DraggingByPlayer or ItemState.DraggingOnStand)
            {
                _gameObjectsInCollision.Remove(gameObject);
            }
        }

        private void UpdateDraggingColor()
        {
            if (ItemView.MeshRenderer.sharedMaterial != _itemsConfig.ItemInFocusMaterial)
            {
                return;
            }
            
            bool allowedToPut = AllowedToPut();

            ItemView.MeshRenderer.sharedMaterial.color = allowedToPut ?
                _itemsConfig.AllowedColor : _itemsConfig.ForbiddenColor;
        }

        private bool AllowedToPut()
        {
            bool allowedToPut = false;
            if (ItemModel.CurrentState == ItemState.DraggingOnStand)
            {
                if (_gameObjectsInCollision.Count == 0)
                {
                    allowedToPut = true;
                }
                else if (_gameObjectsInCollision.Count == 1 &&
                         _gameObjectsInCollision[0].TryGetComponent<ItemStandView>(out var itemStandView))
                {
                    if (itemStandView.ItemsParent == ItemView.transform.parent)
                    {
                        allowedToPut = true;
                    }
                }
            }

            return allowedToPut;
        }

        public void OnPutOnStand(string standTypeName)
        {
            ItemView.transform.localRotation = Quaternion.Euler(0f, ItemModel.CurrentRotation, 0f);
            ItemView.transform.localPosition -= ItemView.InstallationPivot.localPosition;

            bool isPlayer = standTypeName == _itemsConfig.PlayerItemStandName;

            ItemModel.SetCurrentState(isPlayer ? ItemState.DraggingByPlayer : ItemState.DraggingOnStand);
        }

        public virtual void SetInFocus()
        {
            var inFocusMaterial = _itemsConfig.ItemInFocusMaterial;
            ItemView.MeshRenderer.sharedMaterial = inFocusMaterial;
            inFocusMaterial.color = _itemsConfig.AllowedColor;
            ItemModel.SetCurrentState(ItemState.InFocus);
        }

        public void RemoveFromFocus()
        {
            var material = _itemsConfig.ItemsMaterial;
            ItemView.MeshRenderer.sharedMaterial = material;
            ItemModel.SetCurrentState(ItemState.Inactive);
        }

        public virtual bool RequestDrag()
        {
            if (ItemModel.CurrentState != ItemState.InFocus)
            {
                return false;
            }

            ItemView.gameObject.layer = LayerMask.NameToLayer(_itemsConfig.DraggingItemsLayerName);
            ItemModel.SetCurrentState(ItemState.DraggingByPlayer);
            return true;
        }

        public bool RequestPut()
        {
            if (!AllowedToPut())
            {
                return false;
            }

            ItemView.gameObject.layer = LayerMask.NameToLayer(_itemsConfig.InactiveItemsLayerName);
            _gameObjectsInCollision.Clear();
            return true;
        }

        public void Dispose()
        {
            ItemView.CollisionEnter -= ItemViewOnCollisionEnter;
            ItemView.CollisionExit -= ItemViewOnCollisionExit;
            ItemModel.CurrentStateChanged += ItemModelOnCurrentStateChanged;
        }
    }
}