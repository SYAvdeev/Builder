using System.Linq;
using Builder.Items.ItemStand;

namespace Builder.Items.Surfaces
{
    public class SurfaceController : ItemStandController
    {
        private readonly SurfaceView _surfaceView;
        private readonly ISurfaceModel _surfaceModel;
        private readonly ISurfacesConfig _surfacesConfig;

        public SurfaceController(
            SurfaceView surfaceView,
            ISurfaceModel surfaceModel,
            ISurfacesConfig surfacesConfig) : base(surfaceView, surfaceModel)
        {
            _surfaceView = surfaceView;
            _surfaceModel = surfaceModel;
            _surfacesConfig = surfacesConfig;
        }

        public override bool CanPutItem(IItemController itemController)
        {
            bool SurfaceTypePredicate(ItemTypesForSurface it) => it.SurfaceType == _surfaceView.SurfaceType;

            var itemTagsForSurface = _surfacesConfig.ItemTagsForSurfaces.First(SurfaceTypePredicate);
            return itemTagsForSurface.ItemTypes.Contains(itemController.ItemModel.ItemTypeName);
        }

        public override void RemoveCurrentItem() { }
    }
}