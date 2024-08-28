using Builder.Items;
using UnityEngine;

namespace Builder.Player
{
    public class PlayerIdleState : IPlayerState
    {
        private readonly IPlayerController _playerController;

        public PlayerIdleState(IPlayerController playerController)
        {
            _playerController = playerController;
        }

        public void HandleRaycast(RaycastHit hitInfo, IPlayerConfig playerConfig, Transform cameraTransform)
        {
            if (hitInfo.collider)
            {
                var colliderGameObject = hitInfo.collider.gameObject;
                if (colliderGameObject.CompareTag(playerConfig.ItemTag) &&
                    colliderGameObject.TryGetComponent<ItemView>(out var itemView))
                {
                    if (_playerController.CurrentItemInFocus != null)
                    {
                        if (_playerController.CurrentItemInFocus == itemView.ItemController)
                        {
                            return;
                        }

                        _playerController.CurrentItemInFocus.RemoveFromFocus();
                    }

                    _playerController.SetCurrentItemInFocus(itemView.ItemController);
                    _playerController.CurrentItemInFocus!.SetInFocus();
                    return;
                }
            }

            _playerController.CurrentItemInFocus?.RemoveFromFocus();
            _playerController.SetCurrentItemInFocus(null);
        }
        
        public void OnActionTaken()
        {
            if (_playerController.CurrentItemInFocus != null && _playerController.CurrentItemInFocus.RequestDrag())
            {
                _playerController.SetBuildingState();
            }
        }
    }
}