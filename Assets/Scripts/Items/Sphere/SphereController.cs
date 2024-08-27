namespace Builder.Items.Sphere
{
    public class SphereController : ItemController
    {
        public SphereController(SphereView itemView, ISphereModel itemModel, IItemsConfig itemsConfig) :
            base(itemView, itemModel, itemsConfig)
        {
        }
    }
}