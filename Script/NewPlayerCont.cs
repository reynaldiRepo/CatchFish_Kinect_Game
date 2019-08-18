using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPlayerCont : MonoBehaviour
{
    public GameObject thisObj;
    public GameObject addPlayerBtn;
    // Start is called before the first frame update
    void Start()
    {
       thisObj.SetActive(false); 
    }
}
