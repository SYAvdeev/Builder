using System.Collections.Generic;
using UnityEngine;

namespace Builder.Items.ItemsCollection
{
    public class ItemsCollectionView : MonoBehaviour
    {
        [SerializeField] private List<ItemView> _itemViews;

        public IEnumerable<ItemView> ItemViews => _itemViews;
    }
}