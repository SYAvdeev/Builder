using Builder.Items.ItemStand;
using UnityEngine;

namespace Builder.Items.Cube
{
    public class CubeView : ItemView
    {
        [SerializeField] private ItemStandView _itemStandView;

        public ItemStandView ItemStandView => _itemStandView;
    }
}