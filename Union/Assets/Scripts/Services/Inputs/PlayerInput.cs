using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Union.Services.Inputs
{
    //[RequireComponent(typeof(CharacterController3D))]
    public sealed class PlayerInput : MonoBehaviour
    {
        [SerializeField]
        CameraController _cameraController;
        [SerializeField]
        CharacterController3D _controller;
        [SerializeField]
        Characters.BaseController _baseController;

        PlayerTwoAxisAction _walk;
        PlayerTwoAxisAction _camera;

        PlayerAction _jump;
        PlayerAction _sprint;
        PlayerAction _crouch;

        List<IActionProvider> _providers;

        private void Awake()
        {
            _walk = new PlayerTwoAxisAction("Horizontal", "Vertical", 0.1f);
            _camera = new PlayerTwoAxisAction("Mouse X", "Mouse Y", 0f);

            _jump = new PlayerAction("Jump");
            _sprint = new PlayerAction("Sprint");
            _crouch = new PlayerAction("Crouch");

            _providers = new List<IActionProvider>()
            {
                _walk,
                _jump,
                _sprint,
                _crouch,
                _camera,
            };
        }

        private void Update()
        {
            foreach (var provider in _providers)
                provider.Update();

            var x = _camera.X;
            var y = _camera.Y;

            _cameraController.UpdateRotation(x, y);

            if (_jump.IsPressed)
                _baseController.DoJump();

            if (_crouch.IsPressed)
                _baseController.DoCrouch();
        }

        private void FixedUpdate()
        {
            float x = _walk.X;
            float y = _walk.Y;

            if (_sprint.WasPressed)
                _baseController.DoSprint(x, y);
            else
                _baseController.DoWalk(x, y);
        }
    }
}
