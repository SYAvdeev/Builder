using System;
using Builder.Items;
using Builder.Surfaces;
using UnityEngine;

namespace Builder.Player
{
    public class PlayerController : IPlayerController
    {
        private readonly PlayerView _playerView;
        private readonly IPlayerService _playerService;

        private IItemController _currentItemInFocus;

        public PlayerController(PlayerView playerView, IPlayerService playerService)
        {
            _playerView = playerView;
            _playerService = playerService;
        }

        public void Initialize()
        {
            _playerService.Model.CurrentRotationChanged += ModelOnCurrentRotationChanged;
            _playerService.ActionTaken += PlayerServiceOnActionTaken;
        }

        private void PlayerServiceOnActionTaken()
        {
            switch (_playerService.Model.CurrentState)
            {
                case PlayerState.Idle:
                    if (_currentItemInFocus != null && _currentItemInFocus.RequestDrag())
                    {
                        _playerService.Model.SetCurrentState(PlayerState.Building);
                    }
                    break;
                case PlayerState.Building:
                    if (_currentItemInFocus.RequestPut())
                    {
                        _playerService.Model.SetCurrentState(PlayerState.Idle);
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void ModelOnCurrentRotationChanged(Vector2 rotation)
        {
            Vector2 currentRotation = _playerService.Model.CurrentRotation;
            _playerView.transform.rotation = Quaternion.Euler(0f, currentRotation.y, 0f);
            _playerView.Camera.transform.rotation = Quaternion.Euler(currentRotation.x, currentRotation.y, 0f);
        }

        public void FixedUpdate(float fixedDeltaTime)
        {
            var modelCurrentMovement = _playerService.Model.CurrentMovement;

            var characterMovement = _playerView.transform.forward * modelCurrentMovement.x +
                                    _playerView.transform.right * modelCurrentMovement.z;
            
            _playerView.CharacterController.Move(characterMovement * fixedDeltaTime);
            
            var cameraTransform = _playerView.Camera.transform;
            var playerConfig = _playerService.Model.Config;
            
            Physics.Raycast(
                cameraTransform.position,
                cameraTransform.forward,
                out RaycastHit hitInfo,
                playerConfig.ItemHoldDistance,
                playerConfig.RaycastLayerMask);

            switch (_playerService.Model.CurrentState)
            {
                case PlayerState.Idle:

                    if (hitInfo.collider)
                    {
                        var colliderGameObject = hitInfo.collider.gameObject;
                        if (colliderGameObject.CompareTag(playerConfig.ItemTag) &&
                            colliderGameObject.TryGetComponent<ItemView>(out var itemView))
                        {
                            if (_currentItemInFocus != null)
                            {
                                if (_currentItemInFocus == itemView.ItemController)
                                {
                                    break;
                                }

                                _currentItemInFocus.RemoveFromFocus();
                            }

                            _currentItemInFocus = itemView.ItemController;
                            _currentItemInFocus.SetInFocus();
                            break;
                        }
                    }

                    _currentItemInFocus?.RemoveFromFocus();
                    _currentItemInFocus = null;

                    break;
                case PlayerState.Building:

                    Transform parent = _playerView.ItemParent;
                    Vector3 position = cameraTransform.position +
                                       (playerConfig.ItemHoldDistance * cameraTransform.forward);
                    bool isSurface = false;
                    
                    if (hitInfo.collider)
                    {
                        var colliderGameObject = hitInfo.collider.gameObject;
                        if (colliderGameObject.CompareTag(playerConfig.SurfaceTag) &&
                            colliderGameObject.TryGetComponent<SurfaceView>(out var surfaceView) &&
                            _currentItemInFocus.CanPutOnSurface(surfaceView.SurfaceType))
                        {
                            parent = surfaceView.ItemsParent;
                            position = hitInfo.point;
                            isSurface = true;
                        }
                    }

                    _currentItemInFocus.PutOnObject(parent, position, isSurface);
                    
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void Dispose()
        {
            _playerService.Model.CurrentRotationChanged -= ModelOnCurrentRotationChanged;
            _playerService.ActionTaken -= PlayerServiceOnActionTaken;
        }
    }
}