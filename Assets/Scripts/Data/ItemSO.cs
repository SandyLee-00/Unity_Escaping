using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 아이템 데이터
/// </summary>
[CreateAssetMenu(fileName = "ItemSO", menuName = "Data/ItemSO")]
public class ItemSO : ScriptableObject
{
    public int id;
    public string itemName;
    public string description;
}
