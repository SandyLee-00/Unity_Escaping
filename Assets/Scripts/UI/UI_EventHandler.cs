using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

/// <summary>
/// UI 이벤트 핸들러 Click, Pressed, PointerDown, PointerUp
/// </summary>
public class UI_EventHandler : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
{
    public Action OnClickHandler = null;
    public Action OnPointerDownHandler = null;
    public Action OnPressedHandler = null;
    public Action OnPointerUpHandler = null;

    bool _pressed = false;
    
    public void OnPointerClick(PointerEventData eventData)
    {
        OnClickHandler?.Invoke();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _pressed = true;
        OnPointerDownHandler?.Invoke();
    }

    private void Update()
    {
        if (_pressed)
        {
            OnPressedHandler?.Invoke();
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _pressed = false;
        OnPointerUpHandler?.Invoke();
    }
}
