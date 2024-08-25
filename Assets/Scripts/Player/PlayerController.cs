using UnityEngine;

namespace Builder.Player
{
    public class PlayerController : IPlayerController
    {
        private readonly PlayerView _playerView;
        private readonly IPlayerService _playerService;

        public PlayerController(PlayerView playerView, IPlayerService playerService)
        {
            _playerView = playerView;
            _playerService = playerService;
        }

        public void Initialize()
        {
            _playerService.Model.CurrentRotationChanged += ModelOnCurrentRotationChanged;
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
        }

        public void Dispose()
        {
            _playerService.Model.CurrentRotationChanged -= ModelOnCurrentRotationChanged;
        }
    }
}