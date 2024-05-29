using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 팝업 UI는 여기서 상속받아서 사용하기
/// </summary>
public class UI_Popup : UI_Base
{
    public override bool Init()
    {
        if (base.Init() == false)
        {
            return false;
        }

        Managers.UI.SetCanvas(gameObject, true);
        
        return true;
    }
}
