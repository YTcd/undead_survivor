using UnityEngine;
using UnityEngine.InputSystem;

public class MobileJoystick : MonoBehaviour
{
    void Awake()
    {
        bool m_hasTouch = Touchscreen.current != null;
        gameObject.SetActive(m_hasTouch);
    }
}