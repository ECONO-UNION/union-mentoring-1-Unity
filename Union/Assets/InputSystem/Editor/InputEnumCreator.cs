using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace InputSystem.Editor
{
    public class InputEnumCreator
    {
        public void CreateEnumClass(InputAxis[] inputAxes, InputButton[] inputButtons)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("// Input System에 의해서 자동으로 생성되는 Enum입니다.");
            sb.AppendLine("namespace InputSystem");
            sb.AppendLine("{");
            sb.AppendLine("    public enum InputAxisName");
            sb.AppendLine("    {");
            foreach (InputAxis inputAxis in inputAxes)
            {
                sb.AppendLine($"        {inputAxis.name},");
            }
            sb.AppendLine("    }");
            sb.AppendLine();
            sb.AppendLine("    public enum InputButtonName");
            sb.AppendLine("    {");
            foreach (InputButton inputButton in inputButtons)
            {
                sb.AppendLine($"        {inputButton.name},");
            }
            sb.AppendLine("    }");
            sb.AppendLine("}");
            string path = Application.dataPath + "/InputSystem/InputNames.cs";
            File.WriteAllText(path, sb.ToString());
            AssetDatabase.Refresh();
        }
    }
}
