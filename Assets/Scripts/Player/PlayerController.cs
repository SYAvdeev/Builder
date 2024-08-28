using Builder.Items;
using Builder.Items.ItemStand;
using UnityEngine;

namespace Builder.Player
{
    public class PlayerController : ItemStandController, IPlayerController
    {
        private readonly PlayerView _playerView;
        private readonly IPlayerService _playerService;
        private readonly PlayerIdleState _playerIdleState;
        private readonly PlayerBuildingState _playerBuildingState;

        private IPlayerState _currentState;
        
        public IItemController CurrentItemInFocus { get; private set; }
        public IItemStandController CurrentItemStandInFocus { get; private set; }

        public PlayerController(PlayerView playerView, IPlayerService playerService) 
            : base(playerView, playerService.Model)
        {
            _playerView = playerView;
            _playerService = playerService;
            _playerIdleState = new PlayerIdleState(this);
            _playerBuildingState = new PlayerBuildingState(this);
        }

        public override void Initialize()
        {
            base.Initialize();
            _playerService.Model.CurrentRotationChanged += ModelOnCurrentRotationChanged;
            _playerService.ActionTaken += PlayerServiceOnActionTaken;
            _playerService.ItemRotatedClockwise += PlayerServiceOnItemRotatedClockwise;
            _playerService.ItemRotatedCounterclockwise += PlayerServiceOnItemRotatedCounterclockwise;
            SetIdleState();
        }

        void IPlayerController.SetCurrentItemInFocus(IItemController itemController)
        {
            CurrentItemInFocus = itemController;
        }

        void IPlayerController.SetCurrentItemStandInFocus(IItemStandController currentItemStandInFocus)
        {
            CurrentItemStandInFocus = currentItemStandInFocus;
        }

        public void SetIdleState()
        {
            _currentState = _playerIdleState;
        }

        public void SetBuildingState()
        {
            _currentState = _playerBuildingState;
        }

        private void PlayerServiceOnItemRotatedClockwise()
        {
            CurrentItemInFocus?.Rotate(true);
        }

        private void PlayerServiceOnItemRotatedCounterclockwise()
        {
            CurrentItemInFocus?.Rotate(false);
        }

        private void PlayerServiceOnActionTaken()
        {
            _currentState.OnActionTaken();
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
        }

        public void Update()
        {
            _playerService.Update();
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
            
            _currentState.HandleRaycast(hitInfo, playerConfig, cameraTransform);
        }

        private void MoveCharacter(float fixedDeltaTime)
        {
            var modelCurrentMovement = _playerService.Model.CurrentMovement;

            var characterMovement = _playerView.transform.forward * modelCurrentMovement.x +
                                    _playerView.transform.right * modelCurrentMovement.z;
            
            _playerView.CharacterController.Move(characterMovement * fixedDeltaTime);
        }

        public override bool CanPutItem(IItemController itemController) => CurrentItemInFocus == null;

        public override void PutItem(IItemController itemController, Vector3 position)
        {
            base.PutItem(itemController, position);
            CurrentItemInFocus = itemController;
        }

        public override void RemoveCurrentItem()
        {
            CurrentItemInFocus = null;
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