using UnityEngine;
using UnityEngine.EventSystems;

namespace KaranlikTaht.UI
{
    public sealed class RunHoldButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
    {
        [SerializeField] private KaranlikTaht.Player.ThirdPersonMotor motor;

        public void SetMotor(KaranlikTaht.Player.ThirdPersonMotor value) => motor = value;

        public void OnPointerDown(PointerEventData eventData)
        {
            if (motor != null) motor.SetRun(true);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (motor != null) motor.SetRun(false);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (motor != null) motor.SetRun(false);
        }
    }
}