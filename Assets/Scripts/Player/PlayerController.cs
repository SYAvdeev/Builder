using System;
using Builder.Items;
using Builder.Items.ItemStand;
using UnityEngine;

namespace Builder.Player
{
    public class PlayerController : ItemStandController, IPlayerController
    {
        private readonly PlayerView _playerView;
        private readonly IPlayerService _playerService;

        private IItemController _currentItemInFocus;
        private IItemStandController _currentItemStandInFocus;

        public PlayerController(PlayerView playerView, IPlayerService playerService) :
            base(playerView, playerService.Model)
        {
            _playerView = playerView;
            _playerService = playerService;
        }

        public override void Initialize()
        {
            base.Initialize();
            _playerService.Model.CurrentRotationChanged += ModelOnCurrentRotationChanged;
            _playerService.ActionTaken += PlayerServiceOnActionTaken;
            _playerService.ItemRotatedClockwise += PlayerServiceOnItemRotatedClockwise;
            _playerService.ItemRotatedCounterclockwise += PlayerServiceOnItemRotatedCounterclockwise;
        }

        private void PlayerServiceOnItemRotatedClockwise()
        {
            _currentItemInFocus?.Rotate(true);
        }

        private void PlayerServiceOnItemRotatedCounterclockwise()
        {
            _currentItemInFocus?.Rotate(false);
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
            MoveCharacter(fixedDeltaTime);
            Raycast();
        }

        private void Raycast()
        {
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

                    if (hitInfo.collider)
                    {
                        var colliderGameObject = hitInfo.collider.gameObject;
                        if ((colliderGameObject.CompareTag(playerConfig.SurfaceTag) ||
                             colliderGameObject.CompareTag(playerConfig.ItemTag)))
                        {
                            if (colliderGameObject.TryGetComponent<ItemStandView>(out var itemStandView))
                            {
                                if (_currentItemStandInFocus != null)
                                {
                                    if (_currentItemStandInFocus == itemStandView.ItemStandController)
                                    {
                                        itemStandView.ItemStandController.PutItem(_currentItemInFocus, hitInfo.point);
                                        break;
                                    }
                                    
                                    if (itemStandView.ItemStandController.CanPutItem(_currentItemInFocus))
                                    {
                                        _currentItemStandInFocus = itemStandView.ItemStandController;
                                        itemStandView.ItemStandController.PutItem(_currentItemInFocus, hitInfo.point);
                                        break;
                                    }
                                }
                                else
                                {
                                    if (itemStandView.ItemStandController.CanPutItem(_currentItemInFocus))
                                    {
                                        _currentItemStandInFocus = itemStandView.ItemStandController;
                                        itemStandView.ItemStandController.PutItem(_currentItemInFocus, hitInfo.point);
                                        break;
                                    }
                                }
                            }
                        }
                    }

                    _currentItemStandInFocus?.RemoveCurrentItem();
                    _currentItemStandInFocus = null;
                    
                    Vector3 position = cameraTransform.position +
                                       (playerConfig.ItemHoldDistance * cameraTransform.forward);
                    
                    PutItem(_currentItemInFocus, position);
                    
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void MoveCharacter(float fixedDeltaTime)
        {
            var modelCurrentMovement = _playerService.Model.CurrentMovement;

            var characterMovement = _playerView.transform.forward * modelCurrentMovement.x +
                                    _playerView.transform.right * modelCurrentMovement.z;
            
            _playerView.CharacterController.Move(characterMovement * fixedDeltaTime);
        }

        public override bool CanPutItem(IItemController itemController) => _currentItemInFocus == null;

        public override void PutItem(IItemController itemController, Vector3 position)
        {
            base.PutItem(itemController, position);
            _currentItemInFocus = itemController;
        }

        public override void RemoveCurrentItem()
        {
            _currentItemInFocus = null;
        }

        public void Dispose()
        {
            _playerService.Model.CurrentRotationChanged -= ModelOnCurrentRotationChanged;
            _playerService.ActionTaken -= PlayerServiceOnActionTaken;
            _playerService.ItemRotatedClockwise -= PlayerServiceOnItemRotatedClockwise;
            _playerService.ItemRotatedCounterclockwise -= PlayerServiceOnItemRotatedCounterclockwise;
        }
    }
}