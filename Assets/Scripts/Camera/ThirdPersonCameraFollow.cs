using UnityEngine;

namespace KaranlikTaht.CameraSystem
{
    public sealed class ThirdPersonCameraFollow : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private Vector3 offset = new Vector3(0f, 2.2f, -4.5f);
        [SerializeField] private float followSharpness = 10f;
        [SerializeField] private float lookHeight = 1.4f;

        private void LateUpdate()
        {
            if (target == null) return;

            Vector3 desiredPosition = target.position + target.rotation * offset;
            float blend = 1f - Mathf.Exp(-followSharpness * Time.deltaTime);
            transform.position = Vector3.Lerp(transform.position, desiredPosition, blend);

            Vector3 lookPoint = target.position + Vector3.up * lookHeight;
            Quaternion desiredRotation = Quaternion.LookRotation(lookPoint - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, blend);
        }

        public void SetTarget(Transform newTarget) => target = newTarget;
    }
}
