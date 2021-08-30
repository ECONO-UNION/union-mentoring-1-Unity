using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;

using UnityEngine;

namespace Union.Util.Csv
{
    public class Reader
    {
        private string _csvPath;
        private List<Dictionary<string, string>> _datas;

        public Reader(string csvPath)
        {
            this._csvPath = csvPath;
        }

        public void Read()
        {
            const string BaseCsvPath = "Csv/";
            string fullCsvPath = BaseCsvPath + this._csvPath;
            TextAsset data = Resources.Load(fullCsvPath) as TextAsset;

            const string LineSplitChars = @"\r\n|\n\r|\n|\r";
            var lines = Regex.Split(data.text, LineSplitChars);
            if (lines.Length <= 1)
            {
                Debug.LogError("error : " + fullCsvPath + " line 길이 에러");
                return;
            }

            if (this._datas == null)
            {
                this._datas = new List<Dictionary<string, string>>();
            }
            else
            {
                this._datas.Clear();
            }

            const string SplitChars = @",(?=(?:[^""]*""[^""]*"")*(?![^""]*""))";
            char[] trimChars = { '\"' };
            var header = Regex.Split(lines[0], SplitChars);
            for (var i = 1; i < lines.Length; i++)
            {
                var values = Regex.Split(lines[i], SplitChars);

                if (values.Length == 0 || values[0] == "")
                {
                    continue;
                }

                var entry = new Dictionary<string, string>();
                for (var j = 0; j < header.Length && j < values.Length; j++)
                {
                    string value = values[j];
                    value = value.TrimStart(trimChars).TrimEnd(trimChars).Replace("\\", "");
                    value = value.Replace("<br>", "\n");
                    value = value.Replace("<c>", ",");

                    entry[header[j]] = value;
                }

                this._datas.Add(entry);
            }
        }

        public int GetInfoID(int index)
        {
            return int.Parse(this._datas[index]["infoID"]);
        }

        public T GetData<T>(string key, int infoID) where T : struct
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

            for (int i = 0; i < this._datas.Count; i++)
            {
                if (this._datas[i]["infoID"] != id)
                {
                    continue;
                }

                return this._datas[i];
            }

            return null;
        }
    }
}