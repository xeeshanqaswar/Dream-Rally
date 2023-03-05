using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

namespace DitzeGames.MobileJoystick
{

    /// <summary>
    /// Put it on any Image UI Element
    /// </summary>
    public class Joystick : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
    {
        public bool horizontal;
        private Image m_Image;
        private Image m_HandleImage;
        
        protected RectTransform Background;
        protected bool Pressed;
        protected int PointerId;
        public RectTransform Handle;
        [Range(0f,2f)]
        public float HandleRange = 1f;

        [HideInInspector]
        public Vector2 InputVector = Vector2.zero;
        public Vector2 AxisNormalized { get { return InputVector.magnitude > 0.25f ? InputVector.normalized : (InputVector.magnitude < 0.01f ? Vector2.zero : InputVector * 4f); } }

        
        void Start()
        {
            if (Handle == null)
                Handle = transform.GetChild(0).GetComponent<RectTransform>();
            Background = GetComponent<RectTransform>();
            Background.pivot = new Vector2(0.5f, 0.5f);
            Pressed = false;

            m_Image = GetComponent<Image>();
            m_HandleImage = transform.GetChild(0).GetComponent<Image>();
        }

        void Update()
        {
            if (Pressed)
            {
                Vector2 direction = (PointerId >= 0 && PointerId < Input.touches.Length) ? Input.touches[PointerId].position - new Vector2(Background.position.x, Background.position.y) : new Vector2(Input.mousePosition.x, Input.mousePosition.y) - new Vector2(Background.position.x, Background.position.y);
                InputVector = (direction.magnitude > Background.sizeDelta.x / 2f) ? direction.normalized : direction / (Background.sizeDelta.x / 2f);

                if (horizontal)
                {
                    InputVector.y = 0f;
                    if (InputVector.x > 0)
                    {
                        Handle.rotation = Quaternion.Euler(new Vector3(0f, 0f, -90f));
                    }
                    else
                    {
                        Handle.rotation = Quaternion.Euler(new Vector3(0f, 0f, 90f));
                    }
                }
                else
                {
                    InputVector.x = 0f;
                    if (InputVector.y > 0)
                    {
                        Handle.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
                    }
                    else
                    {
                        Handle.rotation = Quaternion.Euler(new Vector3(0f, 0f, -180));
                    }
                }

                Handle.anchoredPosition = (InputVector * Background.sizeDelta.x / 2f) * HandleRange;
                
            }
            
            if (Handle.anchoredPosition.magnitude > 10f)
            {
                m_HandleImage.DOFade(1, 0.5f);
            }
            else
            {
                m_HandleImage.DOFade(0, 0.5f);
            }
            
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            Pressed = true;
            PointerId = eventData.pointerId;

            m_Image.DOFade(1f, 0.2f);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            Pressed = false;
            InputVector = Vector2.zero;
            Handle.anchoredPosition = Vector2.zero;

            m_Image.DOFade(0.4f, 0.2f);
        }
    }
}
