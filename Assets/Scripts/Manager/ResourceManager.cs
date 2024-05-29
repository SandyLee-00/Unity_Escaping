using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Resource 관리
/// </summary>
public class ResourceManager
{
    /*
        /// <summary>
        /// TODO : 리소스 불러올 때 타입별 경로 체크해줘서 리소스 이름만으로 불러오기
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path"></param>
        /// <returns></returns>
        public T Load<T>(string path) where T : Object
        {

        }*/

    /// <summary>
    /// 경로만 받아서 바로 프리팹 생성하기
    /// </summary>
    /// <param name="path"></param>
    /// <param name="parent"></param>
    /// <returns></returns>
    public GameObject Instantiate(string path, Transform parent = null)
    {
        // path에 Prefabs/ 붙여주기
        if (path.Contains("Prefabs/") == false)
        {
            path = $"Prefabs/{path}";
        }

        GameObject prefab = Resources.Load<GameObject>($"{path}");
        if (prefab == null)
        {
            Debug.LogError($"ResourceManager::Instantiate() failed. path={path}");
            return null;
        }
        return Instantiate(prefab, parent);
    }

    /// <summary>
    /// Instantiate 할 때 Clone 이름 prefab 이름으로 바꾸기
    /// </summary>
    /// <param name="prefab"></param>
    /// <param name="parent"></param>
    /// <returns></returns>
    public GameObject Instantiate(GameObject prefab, Transform parent = null)
    {
        GameObject go = Object.Instantiate(prefab, parent);
        go.name = prefab.name;
        return go;
    }
}
