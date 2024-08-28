using System.Collections.Generic;
using Builder.Items.Surfaces;
using UnityEngine;

namespace Builder.Items.Level
{
    public class LevelView : MonoBehaviour
    {
        [SerializeField] private List<ItemView> _itemViews;
        [SerializeField] private List<SurfaceView> _surfaceViews;

        public IEnumerable<ItemView> ItemViews => _itemViews;
        public List<SurfaceView> SurfaceViews => _surfaceViews;
    }
}