using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

/// <summary>
/// </summary>
[Serializable]
public class GameData
{
    public string PlayerName;
    public int Money;
}

/// <summary>
/// </summary>
public class GameManager
{
    public void Init()
    {
        // TODO
        SavedGameData.PlayerName = "Player";
        SavedGameData.Money = 0;
    }

    #region 스탯


    #endregion

    #region 데이터 Save & Load	
    public GameData SavedGameData { get; set; }
    public string _path = Application.persistentDataPath + "/SaveData.json";

    /// <summary>
    /// 플레이 한 내용 저장하기
    /// </summary>
    public void SaveGame()
    {
        string jsonString = JsonUtility.ToJson(Managers.Game.SavedGameData);
        File.WriteAllText(_path, jsonString);
        Debug.Log("GameManager::SaveGame()");
    }

    /// <summary>
    /// Init 한 다음에 LoadGame을 호출하기
    /// SaveData.json에 저장된 데이터를 불러오기
    /// </summary>
    /// <returns></returns>
    public bool LoadGame()
    {
        if (File.Exists(_path) == false)
        {
            Debug.LogError("GameManager::LoadGame() File Not Found");
            return false;
        }

        string fileString = File.ReadAllText(_path);
        GameData data = JsonUtility.FromJson<GameData>(fileString);
        if (data == null)
        {
            Debug.LogError("GameManager::LoadGame() JsonUtility.FromJson<SavedGameData> Failed");
            return false;
        }

        Managers.Game.SavedGameData = data;
        Debug.Log("GameManager::LoadGame()");
        return true;
    }
    #endregion
}
