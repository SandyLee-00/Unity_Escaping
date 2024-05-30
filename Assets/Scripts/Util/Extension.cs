using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;
using UnityEngine.XR;

/// <summary>
/// Extension 함수들 모음
/// </summary>
public static class Extension
{ 
    public static void BindUIEvent(this GameObject _gameObject, Action action, Define.UIEvent type = Define.UIEvent.Click)
    {
        UI_EventHandler ui_EventHandler = _gameObject.GetOrAddComponent<UI_EventHandler>();

        switch (type)
        {
            case Define.UIEvent.Click:
                ui_EventHandler.OnClickHandler -= action;
                ui_EventHandler.OnClickHandler += action;
                break;
            case Define.UIEvent.Pressed:
                ui_EventHandler.OnPressedHandler -= action;
                ui_EventHandler.OnPressedHandler += action;
                break;
            case Define.UIEvent.PointerDown:
                ui_EventHandler.OnPointerDownHandler -= action;
                ui_EventHandler.OnPointerDownHandler += action;
                break;
            case Define.UIEvent.PointerUp:
                ui_EventHandler.OnPointerUpHandler -= action;
                ui_EventHandler.OnPointerUpHandler += action;
                break;
        }
    }

    static System.Random _random = new System.Random();

    public static T GetRandom<T>(this IList<T> list)
    {
        int index = _random.Next(list.Count);
        return list[index];
    }

    public static bool IsValid(this GameObject _gameObject)
    {
        return (_gameObject != null) && _gameObject.activeSelf;
    }
}
