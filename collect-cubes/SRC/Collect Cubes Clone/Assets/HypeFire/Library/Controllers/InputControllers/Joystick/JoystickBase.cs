using UnityEngine;
using UnityEngine.EventSystems;

namespace HypeFire.Library.Controllers.InputControllers.Joystick
{
    /// <summary>
    /// Joystick yapıları için temel sınıftır.
    /// </summary>
    [RequireComponent(typeof(RectTransform))]
    public class JoystickBase : InputController, IPointerDownHandler, IDragHandler, IPointerUpHandler
    {
       
        #region fields
        private Camera m_camera;
        private Canvas m_canvas;
        private Vector2 input = Vector2.zero;
        protected float DeadZone { get; set; } = 0f;
        public bool SnapX { get; set; } = false;
        public bool SnapY { get; set; } = false;


        [Range(0f, 2f)]
        [SerializeField] private float handleRange = 1f;

        [field: SerializeField] private RectTransform Handle { get; set; } = null;
        [field: SerializeField] protected RectTransform HandleZone { get; set; } = null;
        [field: SerializeField] private RectTransform InputArea { get; set; } = null;
        [field: SerializeField] private AxisOptions AxisOptions { get; set; } = AxisOptions.Both;



        public float Horizontal => input.x;
        public float Vertical => input.y;
        public Vector2 Direction => new Vector2(Horizontal, Vertical);


        public float HandleRange
        {
            get => handleRange;
            set => handleRange = Mathf.Abs(value);
        }
        #endregion

        public JoystickBase()
        {

        }

        #region  Functions
        protected override void Awake()
        {
            base.Awake();
            InputArea = GetComponent<RectTransform>();
            m_canvas = GetComponentInParent<Canvas>();

            if (m_canvas == null)
                Debug.LogError("The Canvas is not found");

            MakeItReadyForUse();
        }

        protected void MakeItReadyForUse()
        {
            if (HandleZone == null || Handle == null)
                FindUIs();

            if (HandleZone == null || Handle == null)
                return;

            Vector2 center = new Vector2(0.5f, 0.5f);
            HandleZone.pivot = center;
            Handle.anchorMin = center;
            Handle.anchorMax = center;
            Handle.pivot = center;
            Handle.anchoredPosition = Vector2.zero;
        }

        protected void FindUIs()
        {
            string msj = "Requirements not found.";

            if (HandleZone == null)
            {
                HandleZone = transform.childCount > 0 ? transform.GetChild(0)?.GetComponent<RectTransform>() : null;

                if (HandleZone == null)
                {
                    msj += "\nThe Handle Zone's rect transform is not FOUND!";
                    Debug.LogError(msj);
                    return;
                }
            }

            if (Handle == null)
            {
                Handle = HandleZone.childCount > 0 ? HandleZone.GetChild(0)?.GetComponent<RectTransform>() : null;

                if (Handle == null)
                {
                    msj += "\nThe Handle's rect transform is not FOUND!";
                    Debug.LogError(msj);
                }
            }
        }

        private void InputFormatting()
        {
            if (AxisOptions == AxisOptions.Horizontal)
                input = new Vector2(input.x, 0f);
            else if (AxisOptions == AxisOptions.Vertical)
                input = new Vector2(0f, input.y);
        }

        protected virtual void HandleInput(float magnitude, Vector2 normalized, Vector2 radius, Camera cam)
        {
            if (magnitude > DeadZone)
            {
                if (magnitude > 1)
                    input = normalized;
            }
            else
                input = Vector2.zero;
        }
        

        protected Vector2 ScreenPointToAnchoredPosition(Vector2 screenPosition)
        {

            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(InputArea, screenPosition, m_camera, out Vector2 localPoint))
            {
                Vector2 pivotOffset = InputArea.pivot * InputArea.sizeDelta;
                return localPoint - (HandleZone.anchorMax * InputArea.sizeDelta) + pivotOffset;
            }
            return Vector2.zero;
        }
        #endregion

        public virtual void OnPointerDown(PointerEventData eventData)
        => OnDrag(eventData);

        public void OnDrag(PointerEventData eventData)
        {
            m_camera = null;

            if (m_canvas.renderMode == RenderMode.ScreenSpaceCamera)
                m_camera = m_canvas.worldCamera;

            Vector2 position = RectTransformUtility.WorldToScreenPoint(m_camera, HandleZone.position);
            Vector2 radius = HandleZone.sizeDelta * .5f;

            input = (eventData.position - position) / (radius * m_canvas.scaleFactor);

            InputFormatting();

            HandleInput(input.magnitude, input.normalized, radius, m_camera);

            Handle.anchoredPosition = input * radius * HandleRange;

            listeners.Invoke(new InputResult(true, new Vector2(Horizontal,Vertical)));
        }

        public virtual void OnPointerUp(PointerEventData eventData)
        {
            input = Vector2.zero;
            Handle.anchoredPosition = Vector2.zero;
            listeners.Invoke(new InputResult(false, new Vector2(Horizontal,Vertical)));
        }


    }

    public enum AxisOptions { Both, Horizontal, Vertical }

}