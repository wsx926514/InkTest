using UnityEngine;
using System.IO;
using System.Collections.Generic;
using Ink.Runtime;

public class InkManager : MonoBehaviour
{
    private Story story;

 
    public (string speaker, string content) GetNextLine()
    {
        string speaker = "";
        string content = "";

        if (story == null || !story.canContinue)
        {
            return (speaker, content);
        }

        string line = story.Continue().Trim();

        // 使用自訂常量分隔（冒號+空格）
        if (line.Contains(Constants.COLON))
        {
            var parts = line.Split(new[] { Constants.COLON }, 2, System.StringSplitOptions.None); 
            speaker = parts[0].Trim();
            content = parts[1].Trim();
        }
        else if (story.currentTags.Count > 0)
        {
            speaker = story.currentTags[0].Trim();
            content = line;
        }
        else
        {
            content = line;
        }

        return (speaker, content);
    }
}
