using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Union
{
    [RequireComponent(typeof(CharacterController))]
    public sealed class CharacterController3D : MonoBehaviour
    {
        [SerializeField]
        CharacterController _characterController;

        [SerializeField]
        float _speed = 10;
        [SerializeField]
        float _jumpHeight = 10;

        [SerializeField]
        float _gravity = -9.81f;

        [SerializeField]
        Transform _groundChecker;
        [SerializeField]
        float _groundDistance = 0.4f;
        [SerializeField]
        LayerMask _groundMask;

        bool _isGrounded;

        Vector3 _move;
        Vector3 _force;

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        public void Move(float x, float z)
        {
            _move = transform.right * x + transform.forward * z;
        }

        public void Jump()
        {
            if (_isGrounded)
                _force.y = Mathf.Sqrt(_jumpHeight * -2f * _gravity); 
        }

        public void Attack() { }

        private void Update()
        {
            _characterController.Move(_speed * Time.deltaTime * _move);

            _isGrounded = Physics.CheckSphere(_groundChecker.position, _groundDistance, _groundMask);

            if (_isGrounded && _force.y < 0)
                _force.y = -2f;

            _force.y += _gravity * Time.deltaTime;

            _characterController.Move(_force * Time.deltaTime);
        }
    }
}
