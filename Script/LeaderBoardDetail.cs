using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaderBoardDetail : MonoBehaviour
{
    public Text id, name, cf, sc;
    public GameObject leaderBoardDetail;
    public GameObject thisObj;

    public void openDetailLeaderBoard()
    {
        Debug.Log("ID " + id.text);
        //Debug.Log("Name " + name.text);
        //Debug.Log("CF " + cf.text);
        //Debug.Log("SC " + sc.text);
        leaderBoardDetail.SetActive(true);
        leaderBoardDetail.GetComponent<LeaderBoardUIMGRL>().OpenUiLeaderBoardDetail(id.text.ToString(),name.text.ToString());
    }

    public void deleteUser()
    {
        Destroy(thisObj);
        leaderBoardDetail.GetComponent<LeaderBoardUIMGRL>().deleteUSERdata(id.text.ToString());
    }


}
