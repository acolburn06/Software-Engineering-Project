using System.Collections.Generic;
using UnityEngine;
using System.IO;

public static class Logging
{
    // list representing our log file
    public static List<string> logFile = new List<string>();

    //adds a given log string to our log list
    public static void Log(string logString)
    {
        logFile.Add(logString);
    }

    //dumps logs to log.txt
    public static void DumpLogs(bool clear = true)
    {
        if (!Directory.Exists(Application.dataPath + "/Debug/"))
        {
            Logging.Log("debug dir did not exist");
            Directory.CreateDirectory(Application.dataPath + "/Debug/");
        }

        if (clear)
        {
            File.WriteAllText(Application.dataPath + "/Debug/log.txt", string.Empty);
        }

        StreamWriter writer = new StreamWriter(Application.dataPath + "/Debug/log.txt", true);

        foreach (string logString in logFile)
        {
            writer.WriteLine(logString);
        }

        writer.Close();
    }
}