using UnityEngine;

public abstract class BaseScene : MonoBehaviour
{
    public Define.Scene SceneType = Define.Scene.Unknown;

    protected bool isInitalized = false;

    private void Start()
    {
        Init();
    }

    protected virtual bool Init()
    {
        if (isInitalized)
        {
            return false;
        }

        isInitalized = true;

        GameObject go = GameObject.Find("EventSystem");
        if (go == null)
        {
            Managers.Resource.Instantiate("UI/EventSystem");
        }

        return true;
    }
    public virtual void Clear() { }
}
