using UnityEngine;
using UnityEngine.EventSystems;

namespace KaranlikTaht.Player
{
    public sealed class MobileJoystick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
    {
        [SerializeField] private RectTransform background;
        [SerializeField] private RectTransform handle;
        [SerializeField, Range(0.1f, 1f)] private float handleRange = 0.7f;

        public Vector2 Value { get; private set; }

        public void OnPointerDown(PointerEventData eventData) => UpdateJoystick(eventData);
        public void OnDrag(PointerEventData eventData) => UpdateJoystick(eventData);

        public void OnPointerUp(PointerEventData eventData)
        {
            Value = Vector2.zero;
            if (handle != null) handle.anchoredPosition = Vector2.zero;
        }

        private void UpdateJoystick(PointerEventData eventData)
        {
            if (background == null || handle == null) return;

            if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(
                background, eventData.position, eventData.pressEventCamera, out Vector2 localPoint))
                return;

            Rect rect = background.rect;
            Vector2 center = rect.center;
            float radius = Mathf.Min(rect.width, rect.height) * 0.5f;
            Vector2 offset = (localPoint - center) / Mathf.Max(radius, 1f);
            Value = Vector2.ClampMagnitude(offset, 1f);
            handle.anchoredPosition = Value * radius * handleRange;
        }
    }
}