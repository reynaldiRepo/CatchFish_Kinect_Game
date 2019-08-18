using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class playOrExit : MonoBehaviour
{
    public GameObject gameControl;
    public Camera maincamera;
    public GameObject UI_Pause;
    public GameObject db_mgr;

    public void PlayAgain()
    {
        gameControl.GetComponent<ChatchFish>().jumalah_tangkapan = 0;
        gameControl.GetComponent<ChatchFish>().PlayAgain();
        if (UI_Pause.active)
        {
            UI_Pause.SetActive(false);
        }
    }


    public void pause()
    {
        Time.timeScale = 0f;
        UI_Pause.SetActive(true);
    }

    public void closeUI()
    {
        UI_Pause.SetActive(false);
        Time.timeScale = 1;
    }

    public void Exit()
    {
        maincamera.GetComponent<KinectManager>().OnApplicationQuit();
        SceneManager.LoadScene("MainMenu");
    }

    public void playAgainWithDB()
    {

        int score_value = gameControl.GetComponent<ChatchFish>().score_value;
        int jumlah_tangkapan = gameControl.GetComponent<ChatchFish>().jumalah_tangkapan;
        db_mgr.GetComponent<MainGameDB>().UpdateDataUser(PlayerPrefs.GetString("ID").ToString(), PlayerPrefs.GetString("scene").ToString(), score_value, jumlah_tangkapan);
        gameControl.GetComponent<ChatchFish>().jumalah_tangkapan = 0;
        gameControl.GetComponent<ChatchFish>().PlayAgain();
    }

    public void exitWithDB()
    {
        int score_value = gameControl.GetComponent<ChatchFish>().score_value;
        int jumlah_tangkapan = gameControl.GetComponent<ChatchFish>().jumalah_tangkapan;
        db_mgr.GetComponent<MainGameDB>().UpdateDataUser(PlayerPrefs.GetString("ID").ToString(), PlayerPrefs.GetString("scene").ToString(), score_value, jumlah_tangkapan);
        gameControl.GetComponent<ChatchFish>().jumalah_tangkapan = 0;
        maincamera.GetComponent<KinectManager>().OnApplicationQuit();
        SceneManager.LoadScene("MainMenu");
    }
}
