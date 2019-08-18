using System.Collections;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Data;
using Mono.Data.Sqlite;
using System.IO;
using UnityEngine.UI;

public class SceneSwitcher : MonoBehaviour
{
    public GameObject playGameUI;
    public GameObject rowSample;
    public GameObject rowParent;
    private List<GameObject> data;
    public GameObject NewPlayerUI;
    public InputField newName;
    public GameObject addNewPlayerButton;
    private string SceneName;

    private void Start()
    {
        playGameUI.SetActive(false);
        data = new List<GameObject>();
    }

    public void GotoSoftScene()
    {

        SceneName = "SoftColor";
        playGameUI.SetActive(true);
        string connection = "URI=file:" + Application.dataPath + "/StreamingAssets/User.db";
        Debug.Log(connection);
        IDbConnection dbcon = new Mono.Data.Sqlite.SqliteConnection(connection);
        dbcon.Open();

        IDbCommand cmnd_read = dbcon.CreateCommand();
        IDataReader reader;
        string query = "SELECT * FROM my_user";
        cmnd_read.CommandText = query;
        reader = cmnd_read.ExecuteReader();
        int index = 0;
        while (reader.Read())
        {
            //Debug.Log(reader[0].ToString());
            GameObject tempGO = Instantiate(rowSample) as GameObject;
            tempGO.transform.Find("Name").GetComponent<Text>().text = reader[1].ToString();
            tempGO.transform.Find("ID").GetComponent<Text>().text = reader[0].ToString();
            tempGO.transform.SetParent(rowParent.transform);
            tempGO.transform.GetComponent<PlayGame_Button_Control>().id = reader[0].ToString();
            tempGO.transform.GetComponent<PlayGame_Button_Control>().sceneName = "SoftColor";
            data.Add(tempGO);
        }
        dbcon.Close();
        reader.Close();
        cmnd_read.Dispose();
    }

    public void GotoColorfulScene()
    {
        SceneName = "Colorfull";
        playGameUI.SetActive(true);
        string connection = "URI=file:" + Application.dataPath + "/StreamingAssets/User.db";
        Debug.Log(connection);
        IDbConnection dbcon = new Mono.Data.Sqlite.SqliteConnection(connection);
        dbcon.Open();

        IDbCommand cmnd_read = dbcon.CreateCommand();
        IDataReader reader;
        string query = "SELECT * FROM my_user";
        cmnd_read.CommandText = query;
        reader = cmnd_read.ExecuteReader();
        int index = 0;
        while (reader.Read())
        {
            //Debug.Log(reader[0].ToString());
            GameObject tempGO = Instantiate(rowSample) as GameObject;
            tempGO.transform.Find("Name").GetComponent<Text>().text = reader[1].ToString();
            tempGO.transform.Find("ID").GetComponent<Text>().text = reader[0].ToString();
            tempGO.transform.GetComponent<PlayGame_Button_Control>().id = reader[0].ToString();
            tempGO.transform.GetComponent<PlayGame_Button_Control>().sceneName = "Colorfull";
            tempGO.transform.SetParent(rowParent.transform);
            data.Add(tempGO);
        }
        dbcon.Close();
        reader.Close();
        cmnd_read.Dispose();
    }

    public void close()
    {
        for (int i = 0; i < data.Count; i++)
        {
            Destroy(data[i]);
        }
        playGameUI.SetActive(false);
    }

    public void addPlayer()
    {
        NewPlayerUI.SetActive(true);
        addNewPlayerButton.SetActive(false);
    }

    public void closeNewPlayerUI()
    {
        newName.text = "";
        addNewPlayerButton.SetActive(true);
        NewPlayerUI.SetActive(false);
    }

    public void playAfterAddUSER()
    {
        Debug.Log(SceneName);
        Debug.Log(newName.text);
        string connection = "URI=file:" + Application.dataPath + "/StreamingAssets/User.db";
        Debug.Log(connection);
        IDbConnection dbcon = new Mono.Data.SqliteClient.SqliteConnection(connection);
        dbcon.Open();
        IDbCommand cmnd_read = dbcon.CreateCommand();
        //IDataRecord record;
        string Name = "'"+newName.text.ToString()+"'" ;
        string query = "INSERT INTO my_user(Nama, MaxSC, MaxCF) VALUES("+Name+", '0', '0')";
        cmnd_read.CommandText = query;
        Debug.Log(cmnd_read.CommandText);
        cmnd_read.ExecuteNonQuery();
        dbcon.Close();
        dbcon = null;

        PlayerPrefs.SetString("ID", getLastId());
        PlayerPrefs.SetString("scene", SceneName);
        Debug.Log(getLastId());
        SceneManager.LoadScene(SceneName);
    }

    public string getLastId()
    {
        string connection = "URI=file:" + Application.dataPath + "/StreamingAssets/User.db";
        Debug.Log(connection);
        IDbConnection dbcon = new Mono.Data.SqliteClient.SqliteConnection(connection);
        dbcon.Open();
        IDbCommand cmnd_read = dbcon.CreateCommand();
        //IDataRecord record;
        string Name = "'" + newName.text.ToString() + "'";
        string query = "Select * from my_user order by id DESC Limit 1";
        cmnd_read.CommandText = query;
        IDataReader reader;
        reader = cmnd_read.ExecuteReader();
        string id = "null";
        while (reader.Read())
        {
            id = reader[0].ToString();
        }
        dbcon.Close();
        dbcon = null;
        return id;
    }

    private void OnApplicationQuit()
    {
        Debug.Log("EXIT");
        Application.Quit();
    }

    public void Exit()
    {
        OnApplicationQuit();
    }
}