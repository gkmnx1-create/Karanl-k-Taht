using UnityEngine;

namespace KaranlikTaht.Player
{
    [RequireComponent(typeof(CharacterController))]
    public sealed class ThirdPersonMotor : MonoBehaviour
    {
        [SerializeField] private Transform cameraTransform;
        [SerializeField] private MobileJoystick joystick;
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

        public void SetMoveInput(Vector2 value) => moveInput = Vector2.ClampMagnitude(value, 1f);
        public void SetRun(bool value) => runRequested = value;

        private void Update()
        {
            if (joystick != null)
                moveInput = joystick.Value;

#if UNITY_EDITOR || UNITY_STANDALONE
            Vector2 keyboard = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            if (keyboard.sqrMagnitude > 0.001f) moveInput = Vector2.ClampMagnitude(keyboard, 1f);
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

            if (controller.isGrounded && verticalVelocity < 0f) verticalVelocity = -2f;
            verticalVelocity += gravity * Time.deltaTime;

            Vector3 velocity = direction * (runRequested ? runSpeed : walkSpeed);
            velocity.y = verticalVelocity;
            controller.Move(velocity * Time.deltaTime);

            if (direction.sqrMagnitude > 0.01f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation,
                    rotationSharpness * Time.deltaTime);
            }
        }
    }
}