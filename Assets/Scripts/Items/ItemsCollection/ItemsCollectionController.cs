using System;
using System.Collections.Generic;
using Builder.Items.Cube;
using Builder.Items.Sphere;

namespace Builder.Items.ItemsCollection
{
    public class ItemsCollectionController : IItemsCollectionController
    {
        private readonly ItemsCollectionView _itemsCollectionView;
        private readonly IItemsConfig _itemsConfig;

        private readonly IList<IItemController> _itemControllers = new List<IItemController>();

        public ItemsCollectionController(ItemsCollectionView itemsCollectionView, IItemsConfig itemsConfig)
        {
            _itemsCollectionView = itemsCollectionView;
            _itemsConfig = itemsConfig;
        }

        public void InitializeItems()
        {
            foreach (var itemView in _itemsCollectionView.ItemViews)
            {
                switch (itemView)
                {
                    case CubeView cubeView:
                        break;
                    case SphereView sphereView:
                        var sphereController = new SphereController(sphereView, new SphereModel(), _itemsConfig);
                        sphereController.Initialize();
                        _itemControllers.Add(sphereController);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(itemView));
                }
            }
        }

        public void Dispose()
        {
            foreach (var itemController in _itemControllers)
            {
                itemController.Dispose();
            }
        }
    }
}