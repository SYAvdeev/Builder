namespace Builder.Items.Cube
{
    public class CubeModel : ItemModel, ICubeModel
    {
        public bool HasCubeInstalled { get; }
        public override string TypeName => "Cube";
    }
}