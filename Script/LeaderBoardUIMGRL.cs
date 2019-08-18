using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using Mono.Data.Sqlite;
using System.IO;
using UnityEngine.UI;

public class LeaderBoardUIMGRL : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject thisObject;
    private IDbConnection dbcon;
    public GameObject rowDetailSample;
    public GameObject rowDetailParet;
    private List<GameObject> data;
    public Text namaUser;
    public Text SceneName;
    private string id, nama;
    void Start()
    {
        data = new List<GameObject>();
        thisObject.SetActive(false);
        //Debug.Log(System.DateTime.Now.ToString().Split(new char[0])[0]);
    }

    public void OpenUiLeaderBoardDetail(string id_val, string nama_val)
    {
        id = id_val;
        nama = nama_val;
        showDataScoreCF();
    }
    public void showDataScoreCF()
     {
        destroyOBJ();
        //=======for title============//
        namaUser.text = nama.ToString();
        SceneName.text = "(COLORFULL)";
        //===========================
        string connection = "URI=file:" + Application.dataPath + "/StreamingAssets/User.db";
        Debug.Log(connection);
        IDbConnection dbcon = new SqliteConnection(connection);
        dbcon.Open();
        IDbCommand cmnd_read = dbcon.CreateCommand();
        IDataReader reader;
        

        string query = "SELECT * FROM logCF " +
                       "WHERE idUser='"+id+ "' order by datetime(Tanggal) DESC";
        Debug.Log(query);
        cmnd_read.CommandText = query;
        reader = cmnd_read.ExecuteReader();
        int index = 0;
        while (reader.Read())
        {
            Debug.Log(reader[0] + " " + reader[1] + " " + reader[2] + " " + reader[3]);
            GameObject tempGO = Instantiate(rowDetailSample) as GameObject;
            tempGO.transform.Find("ScoreText").GetComponent<Text>().text = reader[1].ToString();
            tempGO.transform.Find("TglText").GetComponent<Text>().text = reader[2].ToString();
            tempGO.transform.Find("SisaWaktu").GetComponent<Text>().text = reader[3].ToString();
            data.Add(tempGO);
            tempGO.transform.SetParent(rowDetailParet.transform);

        }
        dbcon.Close();
    }

    void destroyOBJ()
    {
        int index = data.Count;
        for (int i = 0; i < index; i++)
        {
            Destroy(data[i]);
        }
    }

    public void closeUI()
    {
        destroyOBJ();
        thisObject.SetActive(false);
    }

    public void showDataScoreSC()
    {
        destroyOBJ();
        namaUser.text = nama.ToString();
        SceneName.text = "(SOFT COLOR)";
        //===========================
        string connection = "URI=file:" + Application.dataPath + "/StreamingAssets/User.db";
        Debug.Log(connection);
        IDbConnection dbcon = new SqliteConnection(connection);
        dbcon.Open();
        IDbCommand cmnd_read = dbcon.CreateCommand();
        IDataReader reader;


        string query = "SELECT * FROM logSC " +
                       "WHERE idUser='" + id + "' order by datetime(Tanggal) DESC";
        Debug.Log(query);
        cmnd_read.CommandText = query;
        reader = cmnd_read.ExecuteReader();
        int index = 0;
        while (reader.Read())
        {
            Debug.Log(reader[0] + " " + reader[1] + " " + reader[2] + " " + reader[3]);
            GameObject tempGO = Instantiate(rowDetailSample) as GameObject;
            tempGO.transform.Find("ScoreText").GetComponent<Text>().text = reader[1].ToString();
            tempGO.transform.Find("TglText").GetComponent<Text>().text = reader[2].ToString();
            tempGO.transform.Find("SisaWaktu").GetComponent<Text>().text = reader[3].ToString();
            data.Add(tempGO);
            tempGO.transform.SetParent(rowDetailParet.transform);

        }
        dbcon.Close();
    }

    public void deleteUSERdata(string id)
    {
        string connection = "URI=file:" + Application.dataPath + "/StreamingAssets/User.db";
        Debug.Log(connection);
        IDbConnection dbcon = new Mono.Data.SqliteClient.SqliteConnection(connection);
        dbcon.Open();
        IDbCommand cmnd_read = dbcon.CreateCommand();
        //IDataRecord record;
        string query = "Delete from my_user where id = '"+id+"'";
        cmnd_read.CommandText = query;
        Debug.Log(cmnd_read.CommandText);
        cmnd_read.ExecuteNonQuery();
        dbcon.Close();
        dbcon = null;
    }

}
