using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using Mono.Data.Sqlite;
using System.IO;
using UnityEngine.UI;

public class database_conn : MonoBehaviour
{
    public GameObject buttonList;
    public GameObject buttonSample;
    public GameObject leaderBoard_btn;
    public GameObject leaderBoard_UI;
    private IDbConnection dbcon;

    public class user
    {
        private string userId, userName, CF, SC ;

        public string getUserId()
        {
            return userId;
        }

        public string getName()
        {
            return userName;
        }

        public string getCF()
        {
            return CF;
        }

        public string getSC()
        {
            return SC;
        }

        public void setId(string id)
        {
            userId = id;
        }

        public void setName(string name)
        {
            userName = name;
        }

        public void setCF(string CF_val)
        {
            CF = CF_val;
        }

        public void setSC(string SC_val)
        {
            SC = SC_val;
        }
    }

    private List<user> userList;
    private int hasClick;
   // public GameObject detailLead;

    private void Start()
    {

        //only for disable on start
        //detailLead.SetActive(false);
        //========================

        leaderBoard_UI.SetActive(false);
        userList = new List<user>();
        hasClick = 0;



        string connection = "URI=file:" + Application.dataPath + "/StreamingAssets/User.db";
        Debug.Log(connection);
        IDbConnection dbcon = new SqliteConnection(connection);
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
            user User = new user();
            User.setId(reader[0].ToString());
            User.setName(reader[1].ToString());
            User.setCF(reader[3].ToString());
            User.setSC(reader[2].ToString());
            userList.Add(User);
            index++;
        }
        dbcon.Close();
        reader.Close();
        cmnd_read.Dispose();
    }


    public void getUser()
    {
        if (hasClick == 0)
        {
            leaderBoard_btn.SetActive(false);
            leaderBoard_UI.SetActive(true);
            for (int i = 0; i < userList.Count; i++)
            {
                Debug.Log(userList[i].getUserId());
                Debug.Log(userList[i].getName());
                GameObject tempGO = Instantiate(buttonSample) as GameObject;
                tempGO.transform.SetParent(buttonList.transform);
                tempGO.transform.Find("Name").GetComponent<Text>().text = userList[i].getName();
                tempGO.transform.Find("CF").GetComponent<Text>().text = userList[i].getCF();
                tempGO.transform.Find("SC").GetComponent<Text>().text = userList[i].getSC();
                tempGO.transform.Find("ID").GetComponent<Text>().text = userList[i].getUserId();
            }
            hasClick = 1;
        }
        else
        {
            leaderBoard_btn.SetActive(false);
            leaderBoard_UI.SetActive(true);
        }

    }

    public void Exit()
    {
        leaderBoard_btn.SetActive(true);
        leaderBoard_UI.SetActive(false);
    }

}
