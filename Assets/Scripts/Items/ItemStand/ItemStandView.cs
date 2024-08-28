using UnityEngine;

namespace Builder.Items.ItemStand
{
    public class ItemStandView : MonoBehaviour
    {
        [SerializeField] private Transform _itemsParent;

        public Transform ItemsParent => _itemsParent;
        public IItemStandController ItemStandController { get; private set; }

        internal void SetItemStandController(IItemStandController itemStandController)
        {
            ItemStandController = itemStandController;
        }
    }
}