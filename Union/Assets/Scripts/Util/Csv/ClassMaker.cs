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
        private string _csvClassDirectoryPath = Application.dataPath + "/Scripts/Util/Csv/Class";

        [MenuItem("Csv/Refresh All Csv Class from Csv Files")]
        private static void RefreshAllCsvClass()
        {
            ClassMaker classMaker = new ClassMaker();
            classMaker.DeletePreviouslyExistingClass();
            
            List<string> csvNames = classMaker.FindCsvNameList();
            foreach (string csvName in csvNames)
            {
                classMaker.CreateCsvClass(csvName);
            }

            AssetDatabase.Refresh();
        }

        private void DeletePreviouslyExistingClass()
        {
            try
            {
                DirectoryInfo csvClassDirectoryInfo = new DirectoryInfo(this._csvClassDirectoryPath);
                csvClassDirectoryInfo.Delete(true);

                Debug.Log("Log : csv 클래스 폴더를 비웠습니다.");
            }
            catch (Exception)
            {
                Debug.Log("error : csv 클래스 폴더에 파일이 없습니다.");
            }

        }

        private void CreateCsvClass(string csvName)
        {
            csvName = csvName.Replace(".csv", "");
            string assetCsvPath = "Csv/" + csvName;

            TextAsset csvData = Resources.Load(assetCsvPath) as TextAsset;
            const string LineSplitChars = @"\r\n|\n\r|\n|\r";
            string[] csvDataLines = Regex.Split(csvData.text, LineSplitChars);
            if (csvDataLines.Length <= Constants.MinimumLineLengthOfCsv)
            {
                Debug.LogError("error : " + assetCsvPath + " line 길이 에러");
                return;
            }

            CheckAndCreateCsvClassDirectory(this._csvClassDirectoryPath);

            string writePath = this._csvClassDirectoryPath + $"/{csvName}.cs";
            StringBuilder classStringBuilder = CreateClassStringBuilder(csvName, csvDataLines);
            File.WriteAllText(writePath, classStringBuilder.ToString());
        }

        private StringBuilder CreateClassStringBuilder(string className, string[] csvDataLines)
        {
            StringBuilder classStringBuilder = new StringBuilder();

            // Begin
            classStringBuilder.AppendLine("namespace Union.Util.Csv");
            classStringBuilder.AppendLine("{");
            classStringBuilder.AppendLine($"    public class {className} : IData");
            classStringBuilder.AppendLine("    {");

            // Variables
            const string SplitChars = @",(?=(?:[^""]*""[^""]*"")*(?![^""]*""))";
            char[] trimChars = { '\"' };
            string[] variableNames = Regex.Split(csvDataLines[0], SplitChars);
            string[] dataTypes = Regex.Split(csvDataLines[1], SplitChars);
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

            return classStringBuilder;
        }

        private void CheckAndCreateCsvClassDirectory(string csvClassDirectoryPath)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(csvClassDirectoryPath);

            if (directoryInfo.Exists == false)
            {
                directoryInfo.Create();
            }
        }

        public List<string> FindCsvNameList()
        {
            const string CsvDirectoryPath = "Assets/Resources/Csv";
            DirectoryInfo csvDirectory = new DirectoryInfo(CsvDirectoryPath);
            FileInfo[] csvFileInfos = csvDirectory.GetFiles();

            List<string> csvNames = new List<string>();
            foreach (var csv in csvFileInfos)
            {
                if (IsCsvFile(csv.Name) == true)
                {
                    csvNames.Add(csv.Name);
                }
            }

            return csvNames;
        }

        private bool IsCsvFile(string file)
        {
            return file.EndsWith(".csv");
        }

    }
}