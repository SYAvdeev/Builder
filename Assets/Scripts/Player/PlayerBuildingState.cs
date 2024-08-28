using Builder.Items.ItemStand;
using UnityEngine;

namespace Builder.Player
{
    public class PlayerBuildingState : IPlayerState
    {
        private readonly IPlayerController _playerController;

        public PlayerBuildingState(IPlayerController playerController)
        {
            _playerController = playerController;
        }

        public void HandleRaycast(RaycastHit hitInfo, IPlayerConfig playerConfig, Transform cameraTransform)
        {
            if (hitInfo.collider)
            {
                var colliderGameObject = hitInfo.collider.gameObject;
                if ((colliderGameObject.CompareTag(playerConfig.SurfaceTag) ||
                     colliderGameObject.CompareTag(playerConfig.ItemTag)))
                {
                    if (colliderGameObject.TryGetComponent<ItemStandView>(out var itemStandView))
                    {
                        if (_playerController.CurrentItemStandInFocus != null)
                        {
                            if (_playerController.CurrentItemStandInFocus == itemStandView.ItemStandController)
                            {
                                itemStandView.ItemStandController.PutItem(_playerController.CurrentItemInFocus, hitInfo.point);
                                return;
                            }
                                    
                            if (itemStandView.ItemStandController.CanPutItem(_playerController.CurrentItemInFocus))
                            {
                                _playerController.SetCurrentItemStandInFocus(itemStandView.ItemStandController);
                                itemStandView.ItemStandController.PutItem(_playerController.CurrentItemInFocus, hitInfo.point);
                                return;
                            }
                        }
                        else
                        {
                            if (itemStandView.ItemStandController.CanPutItem(_playerController.CurrentItemInFocus))
                            {
                                _playerController.SetCurrentItemStandInFocus(itemStandView.ItemStandController);
                                itemStandView.ItemStandController.PutItem(_playerController.CurrentItemInFocus, hitInfo.point);
                                return;
                            }
                        }
                    }
                }
            }

            _playerController.CurrentItemStandInFocus?.RemoveCurrentItem();
            _playerController.SetCurrentItemStandInFocus(null);
                    
            Vector3 position = cameraTransform.position +
                               (playerConfig.ItemHoldDistance * cameraTransform.forward);

            _playerController.PutItem(_playerController.CurrentItemInFocus, position);
        }
        
        public void OnActionTaken()
        {
            if (_playerController.CurrentItemInFocus.RequestPut())
            {
                _playerController.SetIdleState();
            }
        }
    }
}