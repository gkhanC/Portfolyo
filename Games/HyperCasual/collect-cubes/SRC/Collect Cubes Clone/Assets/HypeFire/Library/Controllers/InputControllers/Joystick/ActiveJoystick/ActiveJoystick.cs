using UnityEngine;
using UnityEngine.EventSystems;

namespace HypeFire.Library.Controllers.InputControllers.Joystick.ActiveJoystick
{
    /// <summary>
    /// Joyistick temelli, girdi doğrultusunda konumlana bilen kontrolcüdür.
    /// </summary>
    public class ActiveJoystick : JoystickBase
    {
        [SerializeField] private float threshold = 1f;

        private float Threshold
        {
            get => threshold;
            set => threshold = Mathf.Abs(value);
        }


        protected override void Awake()
        {
            base.Awake();
            HandleZone.gameObject.SetActive(false);
        }

        public override void OnPointerDown(PointerEventData eventData)
        {
            HandleZone.anchoredPosition = ScreenPointToAnchoredPosition(eventData.position);
            HandleZone.gameObject.SetActive(true);
            base.OnPointerDown(eventData);
        }

        public override void OnPointerUp(PointerEventData eventData)
        {
            HandleZone.gameObject.SetActive(false);
            base.OnPointerUp(eventData);
        }

        protected override void HandleInput(float magnitude, Vector2 normalized, Vector2 radius, Camera cam)
        {
            if (magnitude > Threshold)
            {
                Vector2 difference = normalized * (magnitude - Threshold) * radius;
                HandleZone.anchoredPosition += difference;
            }

            base.HandleInput(magnitude, normalized, radius, cam);
        }
    }
}