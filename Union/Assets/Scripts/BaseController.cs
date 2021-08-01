using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Union.Characters
{
    [RequireComponent(typeof(Rigidbody))]
    public class BaseController : MonoBehaviour
    {
        [System.Serializable]
        class Context
        {
            internal Vector3 originalScale;

            internal Rigidbody rigidBody;
            internal Transform transform;

            internal bool isGrounded;
            internal bool isCrouched;
            internal bool isSprinting;

            [SerializeField]
            internal float maxVelocityChange = 10f;
            [SerializeField]
            internal float walkSpeed =5f;
        }

        abstract class Action
        {
            protected Context context;

            protected Action(Context context) { this.context = context; }

            public abstract void Run();
        }

        abstract class AxisAction
        {
            protected Context context;

            protected AxisAction(Context context) { this.context = context; }

            public abstract void Update(float x, float y);
        }

        class Crouch : Action
        {
            public float crouchHeight = .75f;
            public float speedReduction = .5f;

            public Crouch(Context context) : base(context)
            {
                this.context = context;
            }

            public override void Run()
            {
                var originalScale = context.originalScale;

                // Toggle
                if (context.isCrouched)
                {
                    context.transform.localScale = new Vector3(originalScale.x, originalScale.y, originalScale.z);
                    context.walkSpeed /= speedReduction;

                    context.isCrouched = false;
                }
                else
                {
                    context.transform.localScale = new Vector3(originalScale.x, crouchHeight, originalScale.z);
                    context.walkSpeed *= speedReduction;

                    context.isCrouched = true;
                }
            }
        }

        class Jump : Action
        {
            public float jumpPower = 5f;
            public Jump(Context context) : base(context)
            {
                this.context = context;
            }
            public override void Run()
            {
                context.rigidBody.AddForce(0f, jumpPower, 0f, ForceMode.Impulse);
                context.isGrounded = false;
            }
        }

        class Walk : AxisAction
        {
            public bool playerCanMove = true;

            public Walk(Context context) : base(context)
            {
                this.context = context;
            }
            
            public override void Update(float x, float y)
            {
                context.isSprinting = false;

                var maxVelocityChange = context.maxVelocityChange;
                var walkSpeed = context.walkSpeed;

                var targetVelocity = new Vector3(x, 0, y);
                targetVelocity = context.transform.TransformDirection(targetVelocity) * walkSpeed;

                Vector3 velocity = context.rigidBody.velocity;
                Vector3 velocityChange = (targetVelocity - velocity);
                velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
                velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
                velocityChange.y = 0;

                context.rigidBody.AddForce(velocityChange, ForceMode.VelocityChange);
            }
        }

        class Sprint : AxisAction
        {
            public float sprintSpeed = 7f;

            public Sprint(Context context) : base(context)
            {
                this.context = context;
            }

            public override void Update(float x, float y)
            {
                var maxVelocityChange = context.maxVelocityChange;

                var targetVelocity = new Vector3(x, 0, y);
                targetVelocity = context.transform.TransformDirection(targetVelocity) * sprintSpeed;

                Vector3 velocity = context.rigidBody.velocity;
                Vector3 velocityChange = (targetVelocity - velocity);
                velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
                velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
                velocityChange.y = 0;

                context.rigidBody.AddForce(velocityChange, ForceMode.VelocityChange);
            }
        }

        [SerializeField]
        Rigidbody _rigidbody;

        [SerializeField]
        Context _context;

        AxisAction _walk;
        AxisAction _sprint;
        Action _crouch;
        Action _jump;

        private void Awake()
        {
            _context = new Context
            {
                rigidBody = _rigidbody,
                transform = transform,
                originalScale = transform.localScale
            };

            _walk = new Walk(_context);
            _sprint = new Sprint(_context);
            _crouch = new Crouch(_context);
            _jump = new Jump(_context);
        }

        void Update()
        {
            CheckGround();         
        }

        void CheckGround()
        {
            Vector3 origin = new Vector3(transform.position.x, transform.position.y - (transform.localScale.y * .5f), transform.position.z);
            Vector3 direction = transform.TransformDirection(Vector3.down);
            float distance = .75f;

            if (Physics.Raycast(origin, direction, out RaycastHit hit, distance))
                _context.isGrounded = true;
            else
                _context.isGrounded = false;
        }

        public void DoWalk(float x, float z) 
            => _walk.Update(x, z);

        public void DoSprint(float x, float z)
            => _sprint.Update(x, z);

        public void DoCrouch() 
            => _crouch.Run();

        public void DoJump() 
        {
            if (_context.isGrounded)
                _jump.Run();

            if (_context.isCrouched)
                _crouch.Run();
        }
    }

}

