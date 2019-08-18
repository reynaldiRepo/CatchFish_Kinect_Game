using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using Mono.Data.Sqlite;
using System.IO;
using UnityEngine.UI;

public class MainGameDB : MonoBehaviour
{
        public  void UpdateDataUser(string ID, string sceneName, int score, int jumlah_tangkapan)
        {
        string query;
        if (sceneName == "Colorfull")
        {
            query = "insert into logCF(idUser, Score, Tanggal, BerhasilPindah) values("+ID.ToString()+", "+score.ToString()+ ", DATETIME('NOW')," + jumlah_tangkapan.ToString()+")";
        }
        else
        {
            query = "insert into logSC(idUser, Score, Tanggal, BerhasilPindah) values(" + ID.ToString() + ", " + score.ToString() + ", DATETIME('NOW')," + jumlah_tangkapan.ToString() + ")";
        }

        string connection = "URI=file:" + Application.dataPath + "/StreamingAssets/User.db";
        Debug.Log(connection);
        IDbConnection dbcon = new Mono.Data.SqliteClient.SqliteConnection(connection);
        dbcon.Open();
        IDbCommand cmnd_read = dbcon.CreateCommand();
        cmnd_read.CommandText = query;
        Debug.Log(cmnd_read.CommandText);
        cmnd_read.ExecuteNonQuery();
        dbcon.Close();
        dbcon = null;

        if (sceneName == "Colorfull")
        {
            string score_max = getMaxScoreCF(ID);
            updateHighScoreCF(ID, score_max);
        }
        else
        {
            string score_max = getMaxScoreSC(ID);
            updateHighScoreSC(ID, score_max);
        }
    }

    string getMaxScoreSC(string id)
    {
        string query = "SELECT max(score) FROM logSC WHERE idUser = "+id+" ";
        string connection = "URI=file:" + Application.dataPath + "/StreamingAssets/User.db";
        Debug.Log(connection);
        IDbConnection dbcon = new Mono.Data.SqliteClient.SqliteConnection(connection);
        dbcon.Open();

        IDbCommand cmnd_read = dbcon.CreateCommand();
        IDataReader reader;
        cmnd_read.CommandText = query;
        Debug.Log(cmnd_read.CommandText);
        reader = cmnd_read.ExecuteReader();
        string maxScore = "";
        while (reader.Read())
        {
            maxScore = reader[0].ToString();
        }
        dbcon.Close();
        dbcon = null;

        return maxScore;
    }

    string getMaxScoreCF(string id)
    {
        string query = "SELECT max(score) FROM logCF WHERE idUser = " + id + " ";
        string connection = "URI=file:" + Application.dataPath + "/StreamingAssets/User.db";
        Debug.Log(connection);
        IDbConnection dbcon = new Mono.Data.SqliteClient.SqliteConnection(connection);
        dbcon.Open();

        IDbCommand cmnd_read = dbcon.CreateCommand();
        IDataReader reader;
        cmnd_read.CommandText = query;
        Debug.Log(cmnd_read.CommandText);
        reader = cmnd_read.ExecuteReader();
        string maxScore = "";
        while (reader.Read())
        {
            maxScore = reader[0].ToString();
        }
        dbcon.Close();
        dbcon = null;

        return maxScore;
    }

    void updateHighScoreSC (string id, string maxScore)
    {
        string query = "update my_user set MaxSC = "+maxScore+" where Id = "+id+" ";
        string connection = "URI=file:" + Application.dataPath + "/StreamingAssets/User.db";
        Debug.Log(connection);
        IDbConnection dbcon = new Mono.Data.SqliteClient.SqliteConnection(connection);
        dbcon.Open();
        IDbCommand cmnd_read = dbcon.CreateCommand();
        cmnd_read.CommandText = query;
        Debug.Log(cmnd_read.CommandText);
        cmnd_read.ExecuteNonQuery();
        dbcon.Close();
        dbcon = null;
    }

    void updateHighScoreCF(string id, string maxScore)
    {
        string query = "update my_user set MaxCF = " + maxScore + " where Id = " + id + " ";
        string connection = "URI=file:" + Application.dataPath + "/StreamingAssets/User.db";
        Debug.Log(connection);
        IDbConnection dbcon = new Mono.Data.SqliteClient.SqliteConnection(connection);
        dbcon.Open();
        IDbCommand cmnd_read = dbcon.CreateCommand();
        cmnd_read.CommandText = query;
        Debug.Log(cmnd_read.CommandText);
        cmnd_read.ExecuteNonQuery();
        dbcon.Close();
        dbcon = null;
    }


}
