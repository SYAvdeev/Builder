namespace Builder.Items.Cube
{
    public class CubeModel : ItemModel, ICubeModel
    {
        public override string ItemTypeName => "Cube";
        public string ItemStandTypeName => ItemTypeName;
    }
}