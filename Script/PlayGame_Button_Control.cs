using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayGame_Button_Control : MonoBehaviour
{
    public string id = "null";
    public string sceneName = "null";
    public void clickToPlay()
    {
        Debug.Log(id);
        PlayerPrefs.SetString("ID", id);
        PlayerPrefs.SetString("scene", sceneName);
        SceneManager.LoadScene(sceneName);

    }
}
