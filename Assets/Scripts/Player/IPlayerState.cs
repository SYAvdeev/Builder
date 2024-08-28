using UnityEngine;

namespace Builder.Player
{
    public interface IPlayerState
    {
        void HandleRaycast(RaycastHit hitInfo, IPlayerConfig playerConfig, Transform cameraTransform);
        void OnActionTaken();
    }
}