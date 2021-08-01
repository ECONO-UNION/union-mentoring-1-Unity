using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Union
{
    public sealed class CameraController : MonoBehaviour
    {
        [SerializeField]
        Transform _playerBody;

        [SerializeField]
        float _mouseSensitivity = 100f;

        [SerializeField]
        float maxLookAngle = 50f;


        private float yaw = 0.0f;
        private float pitch = 0.0f;

        public void UpdateRotation(float mouseX, float mouseY)
        {
            //_xRotation -= mouseY * _mouseSensitivity * Time.deltaTime;
            //_xRotation = Mathf.Clamp(_xRotation, -90f, 90f);

            //transform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);
            //_playerBody.Rotate(_mouseSensitivity * mouseX * Time.deltaTime * Vector3.up);

            yaw = _playerBody.transform.localEulerAngles.y + mouseX * _mouseSensitivity;
            pitch -= _mouseSensitivity * mouseY;

            pitch = Mathf.Clamp(pitch, -maxLookAngle, maxLookAngle);

            _playerBody.transform.localEulerAngles = new Vector3(0, yaw, 0);
            transform.localEulerAngles = new Vector3(pitch, 0, 0);
        }
    }

}
