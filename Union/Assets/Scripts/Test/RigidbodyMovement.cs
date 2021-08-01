using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Union
{
    public sealed class RigidbodyMovement : MonoBehaviour
    {
        [SerializeField]
        Rigidbody _rigidbody;
        [SerializeField]
        float _speed;

        // Update is called once per frame
        void Update()
        {
            var horizontal = Input.GetAxis("Horizontal");
            var vertical = Input.GetAxis("Vertical");

            var movement = transform.right * horizontal + transform.forward * vertical;

            _rigidbody.MovePosition(transform.position + _speed * Time.deltaTime * movement.normalized);
        }
    }

}
