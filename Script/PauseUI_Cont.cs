using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseUI_Cont : MonoBehaviour
{
    public GameObject thisObj;
    // Start is called before the first frame update
    void Start()
    {
        thisObj.SetActive(false);
    }
    
}
