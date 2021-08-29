using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;

using UnityEngine;

namespace Union.Util.Csv
{
    public class Reader
    {
        private static class Constants
        {
            public const string BaseCsvPath = "Csv/";

            public const string SplitChars = @",(?=(?:[^""]*""[^""]*"")*(?![^""]*""))";
            public const string LineSplitChars = @"\r\n|\n\r|\n|\r";
            public static char[] TrimChars = { '\"' };
        }

        private string _csvPath;
        private List<Dictionary<string, string>> _data;

        public Reader(string csvPath)
        {
            this._csvPath = csvPath;
        }

        public void Read()
        {
            string fullCsvPath = Constants.BaseCsvPath + this._csvPath;
            TextAsset data = Resources.Load(fullCsvPath) as TextAsset;
            var lines = Regex.Split(data.text, Constants.LineSplitChars);

            if (lines.Length <= 1)
            {
                Debug.LogError("error : " + fullCsvPath + " line 길이 에러");
                return;
            }

            this._data = new List<Dictionary<string, string>>();

            var header = Regex.Split(lines[0], Constants.SplitChars);
            for (var i = 1; i < lines.Length; i++)
            {
                var values = Regex.Split(lines[i], Constants.SplitChars);

                if (values.Length == 0 || values[0] == "")
                {
                    continue;
                }

                var entry = new Dictionary<string, string>();
                for (var j = 0; j < header.Length && j < values.Length; j++)
                {
                    string value = values[j];
                    value = value.TrimStart(Constants.TrimChars).TrimEnd(Constants.TrimChars).Replace("\\", "");
                    value = value.Replace("<br>", "\n");
                    value = value.Replace("<c>", ",");

                    entry[header[j]] = value;
                }

                this._data.Add(entry);
            }
        }

        public int GetInfoID(int index)
        {
            return int.Parse(this._data[index]["infoID"]);
        }

        public T GetData<T>(string key, int infoID)
        {
            Dictionary<string, string> data = FindData(infoID);

            if (data == null)
                return default(T);

            return (T)Convert.ChangeType(data[key].ToString(), typeof(T));
        }

        public List<T> GetDataList<T>(string key, int infoID)
        {
            Dictionary<string, string> data = FindData(infoID);

            if (data == null)
                return null;

            string[] trimmedData = data[key].Trim('[', ']').Split(',');
            List<T> result = new List<T>();

            for (int i = 0; i < trimmedData.Length; i++)
            {
                result.Add((T)Convert.ChangeType(trimmedData[i], typeof(T)));
            }

            return result;
        }

        private Dictionary<string, string> FindData(int infoID)
        {
            string id = infoID.ToString();

            for (int i = 0; i < this._data.Count; i++)
            {
                if (this._data[i]["infoID"] != id)
                {
                    continue;
                }

                return this._data[i];
            }

            return null;
        }
    }
}