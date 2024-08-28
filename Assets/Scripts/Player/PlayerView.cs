using Builder.Items.ItemStand;
using UnityEngine;

namespace Builder.Player
{
    public class PlayerView : ItemStandView
    {
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private Camera _camera;

        public CharacterController CharacterController => _characterController;
        public Camera Camera => _camera;
    }
}