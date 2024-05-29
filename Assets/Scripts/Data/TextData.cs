using System.Collections.Generic;
using System;
using System.Xml.Serialization;

public class TextData
{
    [XmlAttribute]
    public int ID;
    [XmlAttribute]
    public string kor;
    [XmlAttribute]
    public string eng;
}

[Serializable, XmlRoot("ArrayOfTextData")]
public class TextDataLoader : ILoader<int, TextData>
{
    [XmlElement("TextData")]
    public List<TextData> _textData = new List<TextData>();

    public Dictionary<int, TextData> MakeDictionary()
    {
        Dictionary<int, TextData> dictionary = new Dictionary<int, TextData>();
        foreach (TextData textData in _textData)
        {
            dictionary.Add(textData.ID, textData);
        }
        return dictionary;
    }
}