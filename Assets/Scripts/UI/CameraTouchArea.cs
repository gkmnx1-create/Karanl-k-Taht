using UnityEngine;
using UnityEngine.EventSystems;
using KaranlikTaht.CameraSystem;

namespace KaranlikTaht.UI
{
    public sealed class CameraTouchArea : MonoBehaviour, IDragHandler
    {
        [SerializeField] private ThirdPersonCameraFollow cameraFollow;

        public void OnDrag(PointerEventData eventData)
        {
            if (cameraFollow != null)
                cameraFollow.AddLookInput(eventData.delta);
        }

        public void SetCamera(ThirdPersonCameraFollow value) => cameraFollow = value;
    }
}