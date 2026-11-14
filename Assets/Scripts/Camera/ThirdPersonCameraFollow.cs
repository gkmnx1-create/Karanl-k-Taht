using UnityEngine;

namespace KaranlikTaht.CameraSystem
{
    public sealed class ThirdPersonCameraFollow : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private float distance = 4.5f;
        [SerializeField] private float targetHeight = 1.4f;
        [SerializeField] private float followSharpness = 12f;
        [SerializeField] private float lookSensitivity = 0.15f;
        [SerializeField] private float minPitch = -25f;
        [SerializeField] private float maxPitch = 65f;

        private float yaw;
        private float pitch = 12f;
        private Vector2 pendingLook;

        private void Start()
        {
            if (target != null) yaw = target.eulerAngles.y;
        }

        // Called by the right-side touch-drag area.
        public void AddLookInput(Vector2 screenDelta) => pendingLook += screenDelta;

        private void LateUpdate()
        {
            if (target == null) return;

            yaw += pendingLook.x * lookSensitivity;
            pitch = Mathf.Clamp(pitch - pendingLook.y * lookSensitivity, minPitch, maxPitch);
            pendingLook = Vector2.zero;

            Quaternion orbit = Quaternion.Euler(pitch, yaw, 0f);
            Vector3 focus = target.position + Vector3.up * targetHeight;
            Vector3 desiredPosition = focus + orbit * new Vector3(0f, 0f, -distance);
            float blend = 1f - Mathf.Exp(-followSharpness * Time.deltaTime);

            transform.position = Vector3.Lerp(transform.position, desiredPosition, blend);
            Quaternion desiredRotation = Quaternion.LookRotation(focus - transform.position, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, blend);
        }

        public void SetTarget(Transform newTarget)
        {
            target = newTarget;
            if (target != null) yaw = target.eulerAngles.y;
        }
    }
}