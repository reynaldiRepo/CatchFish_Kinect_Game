using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatchFish : MonoBehaviour{
    public GameObject cursor;
    private bool checkFish;
    private bool checkBucket;
    private Vector3[] initPos;  
    private Vector3[] BucketPosIndex;
    private int bucketIndex;
    private bool fishInBucket;
    public Text tempFish_visual;
    public GameObject btn_Submit;
    public GameObject btn_cencel;


    // Access Bilangan
    private GameObject opr1, opr2, oprt;
    private int ValueTotal, ValueTemp;
    private float PauseTime = 0f;
    private float time_space = 1f;

    // Access Greeting
    public GameObject Greeting;
    public GameObject FailsText; 

    //access Timer
    public GameObject timer;
    private float time_value;
    private bool time_update;

    //ScoreAcces
    public Text score;
    public Text total_score;
    public int score_value = 0;

    //AddJaring
    public GameObject Jaring1;
    public GameObject Jaring2;

    //for operator
    private int opt = 1;

    // Make class Fish
    public class FishSprite
    {
        private GameObject obj;
        private bool Catch = false;
        public Vector3 InitPositionOBJ;

        public GameObject getObj(){
            return obj;
        }
        public void setObj(string FishName){
            obj = GameObject.Find(FishName);
        }
        public bool getCatch(){
            return Catch;
        }
        public void setCatch(bool catching){
            Catch = catching;
        }
    }

    //Create Fish
    private FishSprite[] fishGroup;
    private FishSprite fishCek = null;

    //for check button cancel or submit
    private bool checkSubmitBTN, checkCancelBTN;

    //lock & cancel countdown obj
    public GameObject lockAnswerCD, cancelAnswerCD;
    private bool lockAnswerUI_ACTV, cancelAnswerUI_ACTV = false;
    private int hasClickSub = 0;
    private int hasClickCan = 0;
    private int countToLock = 4;
    private int countToCan = 4;
    public Text CountDownSubmitDisp, CountDownCancelDisp;

    //for catchCount;
    public int jumalah_tangkapan = 0;
    int RandomNumber(int min, int max)  
    {    
        return Random.Range(min,max);  
    }  


    void activeLockAnswer()
    {
        if (lockAnswerUI_ACTV && hasClickSub != 1)
        {
            StartCoroutine(ActivingUILockAnswer());
            hasClickSub = 1;
        }
    }

    IEnumerator ActivingUILockAnswer()
    {
        while (lockAnswerUI_ACTV && countToLock > 0)
        {
            yield return new WaitForSeconds(1.0f);
            lockAnswerCD.SetActive(true);
            countToLock -= 1;
            CountDownSubmitDisp.text = countToLock.ToString();
        }

        if(countToLock == 0)
        {
            lockAnswerCD.SetActive(false);
            if (ValueTemp == ValueTotal)
            {
              timer.GetComponent<Timer>().update = true;
              Reset();
            }
            else
            {
                timer.GetComponent<Timer>().update = true;
                ResetOnFail();
            }
        }
    }

    bool checkHandAndBTNSUBMIT()
    {
        if(cursor.transform.position.x >= btn_Submit.transform.position.x - 0.1f && cursor.transform.position.x <= btn_Submit.transform.position.x + 0.05f
            && cursor.transform.position.y >= btn_Submit.transform.position.y - 0.1f && cursor.transform.position.y <= btn_Submit.transform.position.y + 0.1f)
        {
            Debug.Log("On Button Submit Pos");
            btn_Submit.GetComponent<Animator>().enabled = true;
            lockAnswerUI_ACTV = true;
            CountDownSubmitDisp.text = countToLock.ToString();
            activeLockAnswer();
            return true;
        }
        else
        {
            btn_Submit.GetComponent<Animator>().enabled = false;
            lockAnswerUI_ACTV = false;
            lockAnswerCD.SetActive(false);
            countToLock = 4;
            hasClickSub = 0;
            return false;
        }
    }


    void activeCancelAnswer()
    {
        if (cancelAnswerUI_ACTV && hasClickCan != 1)
        {
            StartCoroutine(ActivingUICancelAnswer());
            hasClickCan = 1;
        }
    }

    IEnumerator ActivingUICancelAnswer()
    {
        while (cancelAnswerUI_ACTV && countToCan > 0)
        {
            yield return new WaitForSeconds(1.0f);
            cancelAnswerCD.SetActive(true);
            CountDownCancelDisp.text = countToCan.ToString();
            countToCan -= 1;
        }

        if (countToCan == 0)
        {
            cancelAnswerCD.SetActive(false);
            
            for (int i = 0; i < fishGroup.Length; i++)
            {
                Vector3 backToPos = new Vector3();
                backToPos = initPos[i];
                fishGroup[i].getObj().transform.position = backToPos;
                fishGroup[i].getObj().GetComponent<Animator>().enabled = true;
                fishGroup[i].setCatch(false);
            }

            bucketIndex = 0;
            ValueTemp = 0;
            tempFish_visual.text = ValueTemp.ToString();
        }
    }

    bool checkHandAndBTNCANCEL()
    {
        if (cursor.transform.position.x >= btn_cencel.transform.position.x - 0.1f && cursor.transform.position.x <= btn_cencel.transform.position.x + 0.05f
            && cursor.transform.position.y >= btn_cencel.transform.position.y - 0.1f && cursor.transform.position.y <= btn_cencel.transform.position.y + 0.1f)
        {
            Debug.Log("On Button Cancel Pos");
            btn_cencel.GetComponent<Animator>().enabled = true;
            cancelAnswerUI_ACTV = true;
            CountDownCancelDisp.text = countToCan.ToString();
            activeCancelAnswer();
            return true;
        }
        else
        {
            btn_cencel.GetComponent<Animator>().enabled = false;
            cancelAnswerUI_ACTV = false;
            cancelAnswerCD.SetActive(false);
            countToCan = 4;
            hasClickCan = 0;
            return false;
        }
    }

    bool checkHandCollideBucket()
    {
        if (cursor.transform.position.x >= 0.75f && cursor.transform.position.x <= 1F 
        && cursor.transform.position.y >= 0.3f && cursor.transform.position.y <= 0.6f){
            Debug.Log("In Bucket Position");
            checkFish = false;
            return true;
        }else{
            return false;
        }

    }

    bool checkHandCollideFish(GameObject fishCekO)
    {
        if (cursor.transform.position.x >= fishCekO.transform.position.x - 0.05f && cursor.transform.position.x <= fishCekO.transform.position.x + 0.05f
        && cursor.transform.position.y >= fishCekO.transform.position.y - 0.05f && cursor.transform.position.y <= fishCekO.transform.position.y + 0.05f)
        {
            Debug.Log("Fish is selected");
            return true;
        }
        else
        {
            return false;
        }

    }


    void SettingNumber()
    {
        opr1 = GameObject.Find("/Canvas/VARa");
        opr2 = GameObject.Find("/Canvas/VARb");
        oprt = GameObject.Find("/Canvas/Operator");
        Text txt1 = opr1.GetComponent<Text>();
        Text txt2 = opr2.GetComponent<Text>();
        Text txtT = oprt.GetComponent<Text>();
        if (opt == 1)
        {
            int varA = RandomNumber(1, 5);
            int varB = RandomNumber(1, 5);
            ValueTotal = varA + varB;
            txt1.text = varA.ToString();
            txt2.text = varB.ToString();
            txtT.text = "+";
        }
        if(opt == 2)
        {
            int varA = RandomNumber(5, 10);
            int varB = RandomNumber(1, 4);
            ValueTotal = varA - varB;
            txt1.text = varA.ToString();
            txt2.text = varB.ToString();
            txtT.text = "-";
        }
        if(opt == 3)
        {
            int[] bagi1 = new int[10] {6, 10, 15, 14, 8, 20, 21, 18, 12, 16 };
            int[] bagi2 = new int[10] {3, 5, 3, 2, 4, 5, 7, 6, 4, 4};
            int operandRand = RandomNumber(0, 10);
            int varA = bagi1[operandRand];
            int varB = bagi2[operandRand];
            ValueTotal = varA / varB;
            txt1.text = varA.ToString();
            txt2.text = varB.ToString();
            txtT.text = ":";

        }

        if (opt == 4)
        {
            int[] kali1 = new int[6] { 4, 2, 3, 2, 7, 2 };
            int[] kali2 = new int[6] { 2, 4, 2, 1, 1, 2 };
            int operandRand = RandomNumber(0, 6);
            int varA = kali1[operandRand];
            int varB = kali2[operandRand];
            ValueTotal = varA * varB;
            txt1.text = varA.ToString();
            txt2.text = varB.ToString();
            txtT.text = "X";

        }

        ValueTemp = 0;
        tempFish_visual.text = ValueTemp.ToString();
    }

    public void PlayAgain(){
        score_value = 0;
        score.text = score_value.ToString();
        timer.GetComponent<Timer>().Start_time = 301f;
        timer.GetComponent<Timer>().reset();
        cursor.SetActive(true);
        Vector3 backToPos = new Vector3();
        for (int i = 0; i < fishGroup.Length; i++)
        {   
            backToPos = initPos[i];
            fishGroup[i].getObj().transform.position = backToPos;
            fishGroup[i].getObj().GetComponent<Animator>().enabled = true;
            fishGroup[i].setCatch(false);
        }
        Time.timeScale = 1f;
        Start();
    }

    void Reset()
    {   
        // activing jaring
        Jaring1.SetActive(false);
        Jaring2.SetActive(true);
        // cursor off
        cursor.SetActive(false);
        // greeting ctive
        Greeting.SetActive(true);
        for (int i = 0; i < fishGroup.Length; i++)
        {    
            fishGroup[i].getObj().GetComponent<Animator>().enabled = false;
            fishGroup[i].setCatch(false);
        }

        score_value += 10;
        score.text = score_value.ToString();
        StartCoroutine(TimeRoutine());
    }

    void ResetOnFail()
    {
        // activing jaring
        Jaring1.SetActive(false);
        Jaring2.SetActive(true);
        // cursor off
        cursor.SetActive(false);
        // greeting ctive
        FailsText.SetActive(true);
        for (int i = 0; i < fishGroup.Length; i++)
        {
            fishGroup[i].getObj().GetComponent<Animator>().enabled = false;
            fishGroup[i].setCatch(false);
        }
        StartCoroutine(TimeRoutine());
    }

    IEnumerator TimeRoutine(){
        Vector3 backToPos = new Vector3();
        while(PauseTime < 4){
            yield return new WaitForSeconds(time_space);
            PauseTime += time_space;
            Debug.Log("Change Level Time : "+PauseTime);
        }
        timer.GetComponent<Timer>().update = true;
        for (int i = 0; i < fishGroup.Length; i++)
        {   
            backToPos = initPos[i];
            fishGroup[i].getObj().transform.position = backToPos;
            fishGroup[i].getObj().GetComponent<Animator>().enabled = true;
        }
        cursor.SetActive(true);
        timer.GetComponent<Timer>().reset();
        PauseTime = 0;
        opt = RandomNumber(1, 5);
        Start();
    }

    void Start()
    {
        Debug.Log("Time Now1 " + System.DateTime.Now.ToString());
        Debug.Log("Time Now "+System.DateTime.Now.ToString("dd - MM - yyyy"));
        Time.timeScale = 1f;
        Debug.Log(PlayerPrefs.GetString("ID"));
        Debug.Log(PlayerPrefs.GetString("scene"));
        //opt = RandomNumber(1, 3);
        Debug.Log("num operator = " + opt);
        //activing jaring
        Jaring1.SetActive(true);
        Jaring2.SetActive(false);

        // Time.timeScale = 0.7f;
        Greeting.SetActive(false);
        FailsText.SetActive(false);
        SettingNumber();
        lockAnswerCD.SetActive(false);
        cancelAnswerCD.SetActive(false);
        // fishCek = null;
        fishGroup = new FishSprite[10];
        initPos = new Vector3[10];
        BucketPosIndex = new Vector3[10];
        checkFish = false;
        checkBucket = false;
        bucketIndex = 0;

        //make fish is life
        for (int i = 0; i < 10; i++)
        {
            string FishName = string.Format("/Fish ({0})",i+1);
            FishSprite fishObj = new FishSprite();
            fishObj.setObj(FishName);
            fishGroup[i]=fishObj;
            fishGroup[i].setCatch(false);
            Debug.Log("FishPos "+fishGroup[i].getObj().transform.position);
            initPos[i] = fishGroup[i].getObj().transform.position;
            fishGroup[i].InitPositionOBJ = new Vector3(fishGroup[i].getObj().transform.position.x,
                                                        fishGroup[i].getObj().transform.position.y,
                                                        fishGroup[i].getObj().transform.position.z);
        }
        //Debug.Log("HandCursor Position " + cursor.transform.position);

        // make position fish place
        float yPos = 0.39f;
        int indexL = 0;
        for (int i = 0; i < 2; i++)
        {
            for (float posX = 0.8f; posX < 0.95f; posX += 0.04f)
            {
                BucketPosIndex[indexL] = new Vector3(posX, yPos, -10f);
                Debug.Log("Make location catched " + indexL);
                indexL += 1;
                
            }
            yPos -=0.1f;
        }
        time_value = timer.GetComponent<Timer>().Start_time;
    }

    

    void Update()
    {
        if(time_value != 0)
        {
            int index = 0;
            if (checkFish == false && checkHandCollideBucket() == false)
            {
                while (index <= 9 && checkFish == false)
                {
                    fishCek = fishGroup[index];
                    checkFish = checkHandCollideFish(fishCek.getObj());
                    fishInBucket = fishCek.getCatch();
                    index += 1;
                }
            }
            else if (checkFish == true && !fishInBucket)
            {
                fishCek.getObj().GetComponent<Animator>().enabled = false;
                fishCek.getObj().transform.position = new Vector3(cursor.transform.position.x, cursor.transform.position.y, -10f);
                checkBucket = checkHandCollideBucket();
                if (checkBucket == true)
                {
                    fishCek.getObj().transform.position = BucketPosIndex[bucketIndex];
                    fishCek.setCatch(true);
                    bucketIndex += 1;
                    checkFish = false;
                    ValueTemp += 1;
                    tempFish_visual.text = ValueTemp.ToString();
                    jumalah_tangkapan += 1;
                   Debug.Log(ValueTemp);
                }
            }
            checkSubmitBTN = checkHandAndBTNSUBMIT();
            checkCancelBTN = checkHandAndBTNCANCEL();
            if (checkSubmitBTN)
            {
                if (checkFish)
                {
                    fishCek.getObj().GetComponent<Animator>().enabled = true;
                    fishCek.getObj().transform.position = fishCek.InitPositionOBJ;
                    checkFish = false;
                }
            }

            if (checkCancelBTN)
            {
                if (checkFish)
                {
                    fishCek.getObj().GetComponent<Animator>().enabled = true;
                    fishCek.getObj().transform.position = fishCek.InitPositionOBJ;
                    checkFish = false;
                }
            }



            time_value = timer.GetComponent<Timer>().Start_time;
        }
        else
        {
            Time.timeScale = 0f;
            timer.GetComponent<Timer>().show_gameOver();
            total_score.text = "Score : "+score_value.ToString();
        }
    }
     
}
