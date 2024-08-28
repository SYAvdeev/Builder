namespace Builder.Items.Surfaces
{
    public class SurfaceModel : ISurfaceModel
    {
        public string ItemStandTypeName { get; }

        public SurfaceModel(SurfaceType surfaceType)
        {
            ItemStandTypeName = surfaceType.ToString();
        }
    }
}