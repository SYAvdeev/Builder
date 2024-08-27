using System;
using UnityEngine;

namespace Builder.Items
{
    public abstract class ItemView : MonoBehaviour
    {
        [SerializeField] private Transform _installationPivot;
        [SerializeField] private MeshRenderer _meshRenderer;
        
        public IItemController ItemController { get; private set; }

        public Transform InstallationPivot => _installationPivot;
        public MeshRenderer MeshRenderer => _meshRenderer;

        public event Action<GameObject> CollisionEnter;
        public event Action<GameObject> CollisionExit;

        internal void SetItemController(IItemController itemController)
        {
            ItemController = itemController;
        }

        private void OnCollisionEnter(Collision other)
        {
            CollisionEnter?.Invoke(other.gameObject);
        }

        private void OnCollisionExit(Collision other)
        {
            CollisionExit?.Invoke(other.gameObject);
        }
    }
}