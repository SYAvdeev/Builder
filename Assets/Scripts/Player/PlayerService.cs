﻿using Builder.Input;
using UnityEngine;

namespace Builder.Player
{
    public class PlayerService : IPlayerService
    {
        private readonly IPlayerModel _playerModel;
        private readonly IInputService _inputService;
        public IPlayerModel Model { get; }

        public PlayerService(IPlayerModel playerModel, IInputService inputService, IPlayerModel model)
        {
            _playerModel = playerModel;
            _inputService = inputService;
            Model = model;
        }

        public void Initialize()
        {
            _inputService.ActionKeyDown += InputServiceOnActionKeyDown;
        }

        public void Dispose()
        {
            _inputService.ActionKeyDown -= InputServiceOnActionKeyDown;
        }

        public void Update(float deltaTime)
        {
            float forwardMovementAmount = _playerModel.Config.MoveVelocity;
            float sideMovementAmount = _playerModel.Config.MoveVelocity;
            
            if (_inputService.IsMoveForwardPressed)
            {
                forwardMovementAmount *= 1f;
            }
            else if (_inputService.IsMoveBackPressed)
            {
                forwardMovementAmount *= -1f;
            }
            else
            {
                forwardMovementAmount *= 0f;
            }
            
            if (_inputService.IsMoveRightPressed)
            {
                sideMovementAmount *= 1f;
            }
            else if (_inputService.IsMoveLeftPressed)
            {
                sideMovementAmount *= -1f;
            }
            else
            {
                sideMovementAmount *= 0f;
            }

            float rotateSensitivity = Model.Config.RotateSensitivity;
            
            Model.SetCurrentMovement(new Vector3(forwardMovementAmount, 0f, sideMovementAmount));
            Model.AddCurrentRotation(new Vector2(
                _inputService.RotateVerticalAmount * rotateSensitivity,
                _inputService.RotateHorizontalAmount * rotateSensitivity));
        }

        private void InputServiceOnActionKeyDown()
        {
            
        }
    }
}