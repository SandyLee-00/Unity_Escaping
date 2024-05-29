using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.IO;
using System.Text;
using System.Xml.Serialization;

public class DataTransformer : EditorWindow
{
    #region SaveData 지우기
    [MenuItem("Tools/RemoveSaveData")]
    public static void RemoveSaveData()
    {
        string path = Application.persistentDataPath + "/SaveData.json";
        if (File.Exists(path))
        {
            File.Delete(path);
            Debug.Log("SaveFile Deleted");
        }
        else
        {
            Debug.Log("No SaveFile Detected");
        }
    }
    #endregion

    #region Excel -> XML 변환
    [MenuItem("Tools/ParseExcel")]
    public static void ParseExcel()
    {
        ParseTextData();
    }

    static void ParseTextData()
    {
        List<TextData> textDatas = new List<TextData>();

        #region ExcelData
        string[] lines = Resources.Load<TextAsset>($"Data/Excel/TextData").text.Split("\n");

        // 첫번째 라인까지 스킵
        for (int y = 1; y < lines.Length; y++)
        {
            string[] row = lines[y].Replace("\r", "").Split(',');
            if (row.Length == 0)
            {
                continue;
            }
            if (string.IsNullOrEmpty(row[0]))
            {
                continue;
            }

            textDatas.Add(new TextData()
            {
                ID = int.Parse(row[0]),
                kor = row[1],
                eng = row[2]
            });
        }
        #endregion

        string xmlString = ToXML(new TextDataLoader() { _textData = textDatas });
        File.WriteAllText($"{Application.dataPath}/Resources/Data/TextData.xml", xmlString);
        AssetDatabase.Refresh();
    }

    #endregion

    #region XML 유틸
    public sealed class ExtentedStringWriter : StringWriter
    {
        private readonly Encoding stringWriterEncoding;

        public ExtentedStringWriter(StringBuilder builder, Encoding desiredEncoding) : base(builder)
        {
            this.stringWriterEncoding = desiredEncoding;
        }

        public override Encoding Encoding
        {
            get
            {
                return this.stringWriterEncoding;
            }
        }
    }

    public static string ToXML<T>(T obj)
    {
        using (ExtentedStringWriter stringWriter = new ExtentedStringWriter(new StringBuilder(), Encoding.UTF8))
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            xmlSerializer.Serialize(stringWriter, obj);
            return stringWriter.ToString();
        }
    }
    #endregion
}
