using UnityEngine;
using TMPro;

public class Exceptions: MonoBehaviour
{
    //imports pause
    Pause Pause; 
    public bool handleExceptions = true;
    public Log mostRecentLog = new Log();

    //class for the log
    public class Log
    {
        public string logString
        {
            get; set;
        }
        public string stackTrace
        {
            get; set;
        }
    }


    void Awake()
    {
        // every log message runs through HandleException
        Application.logMessageReceived += HandleException;

        // find our pause script
        Pause = GameObject.Find("Pause").GetComponent<Pause>(); 

        mostRecentLog.logString = "error";
        mostRecentLog.stackTrace = "error";
    }

    //dumps the log to log.txt
    public void UserDump()
    {
        Logging.Log("dump at " + Time.realtimeSinceStartup.ToString());
        Logging.DumpLogs();
    }

    //handles exceptions and dumps logs when needed
    void HandleException(string logString, string stackTrace, LogType type)
    {   
        //handling exceptions
        if (type == LogType.Exception && handleExceptions)
        {
            Logging.Log("crash at " + Time.realtimeSinceStartup.ToString());

            // dump logs
            Logging.DumpLogs(); 

            mostRecentLog.logString = logString;
            mostRecentLog.stackTrace = stackTrace;

            // pause the game 
            Pause.PauseCrashed();

            // the string we use for crashinfo
            string bug = "an exception has occured!\nlocation:\n" + mostRecentLog.stackTrace + "\nissue:\n" + mostRecentLog.logString;

            // resume the game
            Pause.ResumeCrashed();

            
        }
    }

}
