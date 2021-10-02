using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Generic;

using UnityEngine;

namespace Union.Util.Csv
{
    public static class Constants
    {
        public const int MinimumLineLengthOfCsv = 2;
    }

    public class Reader <T> where T : IData
    {
        private string _csvPath;
        private Dictionary<int, T> _datas;

        public Reader()
        {
            this._csvPath = Path.Combine("Csv", typeof(T).Name);
        }

        public Reader(string csvPath)
        {
            this._csvPath = RemoveUnnecessaryCsvPathStrings(csvPath);
        }

        private string RemoveUnnecessaryCsvPathStrings(string path)
        {
            string csvPath = path;

            csvPath = csvPath.Replace("Assets/Resources/", "");
            csvPath = csvPath.Replace(".csv", "");

            return csvPath;
        }

        public void Read()
        {
            string csvPath = this._csvPath;
            TextAsset data = Resources.Load<TextAsset>(csvPath);

            const string LineSplitChars = @"\r\n|\n\r|\n|\r";
            string[] csvRows = Regex.Split(data.text, LineSplitChars);
            if (csvRows.Length <= Constants.MinimumLineLengthOfCsv)
            {
                Debug.LogError("error : " + csvPath + " line 길이 에러");
                return;
            }

            ConvertLowsToDatas(csvRows);
        }

        public void StoreDatasInStorage()
        {
            Storage<T>.Instance.ChangeDatas(this._datas);
        }

        private void ConvertLowsToDatas(string[] lines)
        {
            const string SplitChars = @",(?=(?:[^""]*""[^""]*"")*(?![^""]*""))";
            char[] trimChars = { '\"' };

            if (this._datas == null)
            {
                this._datas = new Dictionary<int, T>();
            }

            string[] header = Regex.Split(lines[0], SplitChars);
            for (var i = Constants.MinimumLineLengthOfCsv; i < lines.Length; i++)
            {
                var values = Regex.Split(lines[i], SplitChars);

                if (values.Length == 0 || values[0] == "")
                {
                    continue;
                }

                int infoID = Int32.Parse(values[0]);

                object entry = Activator.CreateInstance(typeof(T));
                var propertyValues = entry.GetType().GetProperties();
                for (var j = 0; j < header.Length && j < values.Length && j < propertyValues.Length; j++)
                {
                    string value = values[j];
                    value = value.TrimStart(trimChars).TrimEnd(trimChars).Replace("\\", "");
                    value = value.Replace("<br>", "\n");
                    value = value.Replace("<c>", ",");

                    object convertValue = Convert.ChangeType(value, propertyValues[j].PropertyType);
                    propertyValues[j].SetValue(entry, convertValue);
                }

                this._datas.Add(infoID, (T)Convert.ChangeType(entry, typeof(T)));
            }
        }

        public T GetData(int infoID)
        {
            return this._datas[infoID];
        }
    }
}