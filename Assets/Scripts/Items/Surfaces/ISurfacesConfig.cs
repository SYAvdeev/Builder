using System.Collections.Generic;

namespace Builder.Items.Surfaces
{
    public interface ISurfacesConfig
    {
        public IReadOnlyCollection<ItemTypesForSurface> ItemTagsForSurfaces { get; }
    }
}