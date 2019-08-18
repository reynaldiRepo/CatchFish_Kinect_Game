using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text timer;
    public float Start_time = 300f;
    public float time_space = 1f;
    public bool update = false;
    public GameObject gameOver;
    

    void Start()
    {
        gameOver.SetActive(false);
        StartCoroutine(TimeRoutine());
    }

    IEnumerator TimeRoutine(){
        while(!update && Start_time > 0){
            yield return new WaitForSeconds(time_space);
            Start_time -= time_space;
            timer.text = Start_time.ToString();
        }
    }

    public void reset(){
        update = false;
        Start();
    }

    public void show_gameOver(){
        gameOver.SetActive(true);
    }
}
