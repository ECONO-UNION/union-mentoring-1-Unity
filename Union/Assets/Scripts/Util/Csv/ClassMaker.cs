using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

using UnityEngine;
using UnityEditor;

namespace Union.Util.Csv
{
    public class ClassMaker
    {
        [MenuItem("Csv/Refresh All Csv Class from Csv Files")]
        private static void RefreshAllCsvClass()
        {
            ClassMaker classMaker = new ClassMaker();

            List<string> csvPaths = classMaker.FindCsvPaths();
            foreach (string csvPath in csvPaths)
            {
                classMaker.CreateCsvClass(csvPath);
            }

            AssetDatabase.Refresh();
        }

        public void CreateCsvClass(string csvPath)
        {
            string assetCsvPath = csvPath;
            assetCsvPath = assetCsvPath.Replace("Assets/Resources/", "");
            assetCsvPath = assetCsvPath.Replace(".csv", "");

            TextAsset data = Resources.Load(assetCsvPath) as TextAsset;

            const string LineSplitChars = @"\r\n|\n\r|\n|\r";
            var lines = Regex.Split(data.text, LineSplitChars);
            if (lines.Length <= Constants.MinimumLineLengthOfCsv)
            {
                Debug.LogError("error : " + csvPath + " line 길이 에러");
                return;
            }

            StringBuilder classStringBuilder = new StringBuilder();
            string csvName = GetCsvName(csvPath);

            // Begin
            classStringBuilder.AppendLine("namespace Union.Util.Csv");
            classStringBuilder.AppendLine("{");
            classStringBuilder.AppendLine($"    public class {csvName}");
            classStringBuilder.AppendLine("    {");

            // Variables
            const string SplitChars = @",(?=(?:[^""]*""[^""]*"")*(?![^""]*""))";
            char[] trimChars = { '\"' };
            string[] variableNames = Regex.Split(lines[0], SplitChars);
            string[] dataTypes = Regex.Split(lines[1], SplitChars);
            for (uint i = 0; i < variableNames.Length && i < dataTypes.Length; i++)
            {
                string dataType = dataTypes[i];
                dataType = dataType.TrimStart(trimChars).TrimEnd(trimChars).Replace("\\", "");
                dataType = dataType.Replace("<br>", "\n");
                dataType = dataType.Replace("<c>", ",");

                classStringBuilder.AppendLine($"        public {dataType} {variableNames[i]} {{ get; set; }}");
            }

            // End
            classStringBuilder.AppendLine("    }");
            classStringBuilder.AppendLine();
            classStringBuilder.AppendLine("}");

            string csvClassDirectoryPath = Application.dataPath + "/Scripts/Util/Csv/Class";
            CheckAndCreateCsvClassDirectory(csvClassDirectoryPath);

            string writePath = csvClassDirectoryPath + $"/{csvName}.cs";
            File.WriteAllText(writePath, classStringBuilder.ToString());
        }

        private void CheckAndCreateCsvClassDirectory(string csvClassDirectoryPath)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(csvClassDirectoryPath);

            if (directoryInfo.Exists == false)
            {
                directoryInfo.Create();
            }
        }

        private string GetCsvName(string csvPath)
        {
            int lastIndexOfSlash = csvPath.LastIndexOf("/") + 1;
            int lastIndexOfCsv = csvPath.LastIndexOf(".csv");
            string csvName = csvPath.Substring(lastIndexOfSlash, lastIndexOfCsv - lastIndexOfSlash);

            return csvName;
        }

        public List<string> FindCsvPaths()
        {
            List<string> csvPaths = new List<string>();
            string[] filePaths = { "" };

            try
            {
                const string CsvDirectoryPath = "Assets/Resources/Csv";
                filePaths = Directory.GetFiles(CsvDirectoryPath, "*.*", SearchOption.AllDirectories);
            }
            catch (IOException ex) 
            {
                Debug.Log(ex.Message);
            }

            foreach (string filePath in filePaths)
            {
                if (IsCsvFile(filePath) == true)
                {
                    csvPaths.Add(filePath);
                }
            }

            return csvPaths;
        }

        private bool IsCsvFile(string file)
        {
            return file.EndsWith(".csv");
        }

    }
}