using System;
using Builder.Surfaces;
using UnityEngine;

namespace Builder.Items
{
    public interface IItemController : IDisposable
    {
        public void Initialize();
        public bool CanPutOnSurface(SurfaceType surfaceType);
        public void PutOnObject(Transform parent, Vector3 pivotPoint, bool isSurface);
        public void SetInFocus();
        public void RemoveFromFocus();
        bool RequestDrag();
        bool RequestPut();
        IItemModel ItemModel { get; }
    }
}