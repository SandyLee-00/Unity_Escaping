using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 공통적으로 사용되는 상수들을 정의
/// </summary>
public class Define
{
    public enum UIEvent
    {
        Click,
        Pressed,
        PointerDown,
        PointerUp,
    }

    public enum Sound
    {
        Bgm = 0,
        Effect,
        Max,
    }

    public enum Scene
    {
        Unknown,
        Title,
        Game,
    }
}
