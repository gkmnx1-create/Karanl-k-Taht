using UnityEngine;

namespace KaranlikTaht.Player
{
    [RequireComponent(typeof(CharacterController))]
    public sealed class ThirdPersonMotor : MonoBehaviour
    {
        [SerializeField] private Transform cameraTransform;
        [SerializeField] private float walkSpeed = 3.2f;
        [SerializeField] private float runSpeed = 5.4f;
        [SerializeField] private float rotationSharpness = 12f;
        [SerializeField] private float gravity = -20f;

        private CharacterController controller;
        private Vector2 moveInput;
        private float verticalVelocity;
        private bool runRequested;

        private void Awake()
        {
            controller = GetComponent<CharacterController>();
            if (cameraTransform == null && Camera.main != null)
                cameraTransform = Camera.main.transform;
        }

        // Mobile joystick and UI controls can call these methods.
        public void SetMoveInput(Vector2 value) => moveInput = Vector2.ClampMagnitude(value, 1f);
        public void SetRun(bool value) => runRequested = value;

        private void Update()
        {
#if UNITY_EDITOR || UNITY_STANDALONE
            if (moveInput.sqrMagnitude < 0.001f)
                moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            if (Input.GetKeyDown(KeyCode.LeftShift)) runRequested = true;
            if (Input.GetKeyUp(KeyCode.LeftShift)) runRequested = false;
#endif
            Vector3 forward = cameraTransform != null
                ? Vector3.ProjectOnPlane(cameraTransform.forward, Vector3.up).normalized
                : transform.forward;
            Vector3 right = cameraTransform != null
                ? Vector3.ProjectOnPlane(cameraTransform.right, Vector3.up).normalized
                : transform.right;

            Vector3 direction = forward * moveInput.y + right * moveInput.x;
            if (direction.sqrMagnitude > 1f) direction.Normalize();

            if (controller.isGrounded && verticalVelocity < 0f)
                verticalVelocity = -2f;
            verticalVelocity += gravity * Time.deltaTime;

            float speed = runRequested ? runSpeed : walkSpeed;
            Vector3 velocity = direction * speed;
            velocity.y = verticalVelocity;
            controller.Move(velocity * Time.deltaTime);

            if (direction.sqrMagnitude > 0.01f)
            {
                Quaternion target = Quaternion.LookRotation(direction, Vector3.up);
                transform.rotation = Quaternion.Slerp(
                    transform.rotation, target, rotationSharpness * Time.deltaTime);
            }

#if UNITY_EDITOR || UNITY_STANDALONE
            moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
#endif
        }
    }
}
