using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// LoadScene 을 enum인 SceneType으로 할 수 있게 한다, 필요없는 UI들을 닫아준다.
/// </summary>
public class SceneManagerEx
{
    private Define.Scene currentSceneType = Define.Scene.Unknown;

    public Define.Scene CurrentSceneType
    {
        get
        {
            if (currentSceneType != Define.Scene.Unknown)
            {
                return currentSceneType;
            }
            return CurrentSceneComponent.SceneType;
        }
        set { currentSceneType = value; }
    }

    public BaseScene CurrentSceneComponent { get { return GameObject.FindObjectOfType<BaseScene>(); } }

    /// <summary>
    /// 다음 씬 로드할 때 현재 씬 Component, 기존 Popup UI들을 닫아주고 씬을 로드한다.
    /// </summary>
    /// <param name="type"></param>
    public void LoadScene(Define.Scene type)
    {
        CurrentSceneComponent.Clear();

        currentSceneType = type;
        SceneManager.LoadScene(GetSceneName(type));
    }

    string GetSceneName(Define.Scene type)
    {
        string name = System.Enum.GetName(typeof(Define.Scene), type);
        return name;
    }
}
