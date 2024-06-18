using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Data;
using System.IO;

public class SaveManager : MonoBehaviour
{
    string dbName = "URI=file:Database.db";

     private void Awake()
    {
        if (!Directory.Exists(Application.streamingAssetsPath + "/Saves/"))
        {
            Directory.CreateDirectory(Application.streamingAssetsPath + "/Saves/");
        }
        DontDestroyOnLoad(gameObject);
    }

     // Start is called before the first frame update
    void Start()
    {
        if (!File.Exists("Database.db"))
            CreateDB();
    }

     // Update is called once per frame
    void Update()
    {
        // literally does nothing at the moment.
    }

    public void CreateDB()
        {
            // Create DB connection
            using (var Connection = new SqliteConnection(dbName))
            {
                Connection.Open();

                 // set up an object command to control db
                IDbCommand Command = Connection.CreateCommand();

                // Creating the Unit Table if it doesn't already exist
                Command.CommandText = "CREATE TABLE IF NOT EXISTS Scores (id INTEGER, Score TEXT);";
                Command.ExecuteReader();
                Command = Connection.CreateCommand();

                Connection.Close();
            }
        }

    public string Read()
    {
        Debug.Log("READ");
        using (SqliteConnection connection = new SqliteConnection(dbName))
        {
            connection.Open();
            SqliteCommand cmd = new SqliteCommand("SELECT MAX(Score) FROM Scores", connection);
            SqliteDataReader reader = cmd.ExecuteReader();
            return reader.GetValue(0).ToString();
        }
    }
    
    public void Write(string table, string column, string row)
    {
        Debug.Log("WRITE");
        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();

            //Setting up an object command to allow db control
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT " + column + " FROM " + table + " WHERE id = " + row + ";";
                command.ExecuteNonQuery();
            }
            connection.Close();
        }
    }

     public void DeleteAndRecreateDatabase()
    {
        File.Delete("Database.db");
        CreateDB();
    }
}