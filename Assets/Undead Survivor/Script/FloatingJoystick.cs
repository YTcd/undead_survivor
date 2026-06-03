using UnityEngine;
using UnityEngine.EventSystems;

public class FloatingJoystick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    [SerializeField] RectTransform m_bg;
    [SerializeField] RectTransform m_knob;
    [SerializeField] float m_range = 80f;

    public Vector2 Direction { get; private set; }

    private RectTransform m_rectTransform;
    private Vector2 m_origin;
    private CanvasGroup m_bgGroup;

    void Awake()
    {
        m_rectTransform = GetComponent<RectTransform>();
        m_bgGroup = m_bg.GetComponent<CanvasGroup>();
        if (m_bgGroup == null)
            m_bgGroup = m_bg.gameObject.AddComponent<CanvasGroup>();

        m_bgGroup.alpha = 0;
        m_bgGroup.blocksRaycasts = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            m_rectTransform, eventData.position, eventData.pressEventCamera, out Vector2 localPos);

        m_bg.anchoredPosition = localPos;
        m_knob.anchoredPosition = Vector2.zero;
        m_bgGroup.alpha = 1;
        m_origin = eventData.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 delta = eventData.position - m_origin;
        float len = delta.magnitude;
        Direction = len > m_range ? delta / len : delta / m_range;
        m_knob.anchoredPosition = Direction * m_range;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Direction = Vector2.zero;
        m_knob.anchoredPosition = Vector2.zero;
        m_bgGroup.alpha = 0;
    }
}