using System;
using System.Collections.Generic;
using Builder.Items.Cube;
using Builder.Items.Sphere;
using Builder.Items.Surfaces;

namespace Builder.Items.Level
{
    public class LevelController : ILevelController
    {
        private readonly LevelView _levelView;
        private readonly IItemsConfig _itemsConfig;
        private readonly ISurfacesConfig _surfacesConfig;

        private readonly IList<IItemController> _itemControllers = new List<IItemController>();

        public LevelController(LevelView levelView, IItemsConfig itemsConfig, ISurfacesConfig surfacesConfig)
        {
            _levelView = levelView;
            _itemsConfig = itemsConfig;
            _surfacesConfig = surfacesConfig;
        }

        public void InitializeItems()
        {
            foreach (var itemView in _levelView.ItemViews)
            {
                switch (itemView)
                {
                    case CubeView cubeView:
                        var cubeController = new CubeController(cubeView, new CubeModel(), _itemsConfig);
                        cubeController.Initialize();
                        _itemControllers.Add(cubeController);
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

            foreach (var surfaceView in _levelView.SurfaceViews)
            {
                var surfaceController = new SurfaceController(
                    surfaceView,
                    new SurfaceModel(surfaceView.SurfaceType),
                    _surfacesConfig);
                surfaceController.Initialize();
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