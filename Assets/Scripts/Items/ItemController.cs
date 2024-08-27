using System.Collections.Generic;
using System.Linq;
using Builder.Surfaces;
using UnityEngine;

namespace Builder.Items
{
    public abstract class ItemController : IItemController
    {
        private readonly ItemView _itemView;
        private readonly IItemsConfig _itemsConfig;

        private readonly IList<ItemView> _itemViewsInCollision = new List<ItemView>();

        public IItemModel ItemModel { get; }

        protected ItemController(ItemView itemView, IItemModel itemModel, IItemsConfig itemsConfig)
        {
            _itemView = itemView;
            ItemModel = itemModel;
            _itemsConfig = itemsConfig;
        }

        public void Initialize()
        {
            _itemView.SetItemController(this);
            _itemView.CollisionEnter += ItemViewOnCollisionEnter;
            _itemView.CollisionExit += ItemViewOnCollisionExit;
        }

        public bool CanPutOnSurface(SurfaceType surfaceType)
        {
            bool SurfaceTypePredicate(ItemTypesForSurface it) => it.SurfaceType == surfaceType;

            var itemTagsForSurface = _itemsConfig.ItemTagsForSurfaces.First(SurfaceTypePredicate);
            return itemTagsForSurface.ItemTypes.Contains(_itemView.ItemController.ItemModel.TypeName);
        }

        private void ItemViewOnCollisionEnter(GameObject gameObject)
        {
            if (ItemModel.CurrentState == ItemState.Dragging)
            {
                if (gameObject.CompareTag(_itemsConfig.ItemTag) &&
                    gameObject.TryGetComponent<ItemView>(out var itemView))
                {
                    if (!_itemViewsInCollision.Contains(itemView))
                    {
                        _itemViewsInCollision.Add(itemView);
                        UpdateDraggingColor();
                    }
                }
            }
        }

        private void ItemViewOnCollisionExit(GameObject gameObject)
        {
            if (ItemModel.CurrentState == ItemState.Dragging)
            {
                if (gameObject.CompareTag(_itemsConfig.ItemTag) &&
                    gameObject.TryGetComponent<ItemView>(out var itemView))
                {
                    _itemViewsInCollision.Remove(itemView);
                    UpdateDraggingColor();
                }
            }
        }

        private void UpdateDraggingColor()
        {
            _itemView.MeshRenderer.sharedMaterial.color = _itemViewsInCollision.Count > 0 ?
                _itemsConfig.ForbiddenColor : _itemsConfig.AllowedColor;
        }

        public void PutOnObject(Transform parent, Vector3 pivotPoint, bool isSurface)
        {
            _itemView.transform.SetParent(parent);
            _itemView.transform.localRotation = Quaternion.Euler(0f, ItemModel.CurrentRotation, 0f);
            _itemView.transform.position = pivotPoint - _itemView.InstallationPivot.localPosition;
            ItemModel.SetCurrentState(isSurface ? ItemState.DraggingOnSurface : ItemState.Dragging);
        }

        public void SetInFocus()
        {
            var inFocusMaterial = _itemsConfig.ItemInFocusMaterial;
            _itemView.MeshRenderer.sharedMaterial = inFocusMaterial;
            inFocusMaterial.color = _itemsConfig.AllowedColor;
            ItemModel.SetCurrentState(ItemState.InFocus);
        }

        public void RemoveFromFocus()
        {
            var material = _itemsConfig.ItemsMaterial;
            _itemView.MeshRenderer.sharedMaterial = material;
            ItemModel.SetCurrentState(ItemState.Inactive);
        }

        public virtual bool RequestDrag()
        {
            bool canDrag = ItemModel.CurrentState == ItemState.InFocus;

            if (canDrag)
            {
                _itemView.gameObject.layer = LayerMask.NameToLayer(_itemsConfig.DraggingItemsLayerName);
                ItemModel.SetCurrentState(ItemState.Dragging);
            }
            
            return canDrag;
        }

        public bool RequestPut()
        {
            bool canPut = ItemModel.CurrentState == ItemState.DraggingOnSurface && _itemViewsInCollision.Count == 0;
            if (canPut)
            {
                _itemView.gameObject.layer = LayerMask.NameToLayer(_itemsConfig.InactiveItemsLayerName);
                _itemViewsInCollision.Clear();
                return true;
            }

            return false;
        }

        

        public void Dispose()
        {
            _itemView.CollisionEnter -= ItemViewOnCollisionEnter;
            _itemView.CollisionExit -= ItemViewOnCollisionExit;
        }
    }
}