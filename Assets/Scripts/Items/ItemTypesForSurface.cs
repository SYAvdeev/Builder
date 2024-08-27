using System;
using System.Collections.Generic;
using Builder.Surfaces;

namespace Builder.Items
{
    [Serializable]
    public class ItemTypesForSurface
    {
        public SurfaceType SurfaceType;
        public List<string> ItemTypes;
    }
}