using UnityEngine;

namespace Builder.UI
{
    public class UIViewRoot : MonoBehaviour
    {
        [SerializeField] private Transform _uiViewsParent;

        public Transform UIViewsParent => _uiViewsParent;
    }
}