using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Lean;
using Lean.Gui;

public class JoystickZrea : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private LeanJoystick _joystick;

    private Vector2 _joystickSize
    {
        get
        {
            var rt = _joystick.GetComponent<RectTransform>();
            float width = rt.sizeDelta.x;
            float height = rt.sizeDelta.y;
            return new Vector2(height, width);
        }
    }

    internal void Awake()
    {
        _joystick = FindObjectOfType<LeanJoystick>();
    }

    internal void Start()
    {
        HideJoystick();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        var position = eventData.position;
        position.x = Mathf.Clamp(position.x, _joystickSize.x * 0.5f, Screen.width - _joystickSize.x * 0.5f);
        position.y = Mathf.Clamp(position.y, _joystickSize.y * 0.5f, Screen.height - _joystickSize.y * 0.5f);
        // _joystick.transform.position = position;
        ShowJoystick();
        // _joystick.OnPointerDown(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        // _joystick.OnPointerUp(eventData);
        if (Input.touchCount <= 0) HideJoystick();
    }

    private void HideJoystick()
    {
        // _joystick.gameObject.SetActive(false);
    }

    private void ShowJoystick()
    {
        // _joystick.gameObject.SetActive(true);
    }
}
