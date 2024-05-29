using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;

/// <summary>
/// 
/// </summary>
/// <typeparam name="Key"></typeparam>
/// <typeparam name="Value"></typeparam>
public interface ILoader<Key, Value>
{
    Dictionary<Key, Value> MakeDictionary();
}

/// <summary>
/// 
/// </summary>
public class DataManager
{
    public Dictionary<int, TextData> Texts { get; private set; }

    public void Init()
    {
        Texts = LoadXml<TextDataLoader, int, TextData>("TextData").MakeDictionary();
    }

    private Loader LoadXml<Loader, Key, Item>(string name) where Loader : ILoader<Key, Item>, new()
    {
        XmlSerializer serializer = new XmlSerializer(typeof(Loader));
        TextAsset textAsset = Resources.Load<TextAsset>($"Data/{name}");
        if (textAsset == null)
        {
            Debug.LogError($"DataManager::LoadXml() failed. name={name}");
            return default;
        }

        using (MemoryStream stream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(textAsset.text)))
        {
            return (Loader)serializer.Deserialize(stream);
        }
    }

    private Item LoadSingleXml<Item>(string name)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(Item));
        TextAsset textAsset = Resources.Load<TextAsset>($"Data/{name}");
        using (MemoryStream stream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(textAsset.text)))
        {
            return (Item)serializer.Deserialize(stream);
        }
    }
}

