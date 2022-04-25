using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerScript : MonoBehaviour
{
    [SerializeField] private GameObject PlayerCanvas;
    [SerializeField] private GameObject runCanvas;
    [SerializeField] private GameObject timerPanel;
    [SerializeField] private TMPro.TMP_Text timerTxt;
    [SerializeField] Transform spherePos;
    [SerializeField] private LayerMask playerMask;

    private float timer;
    private float currentTime;
    private int printTime;
    private bool activate;

    public static bool loseToTime, win;
    public static int counter;
    void Start()
    {
        currentTime = 60;
        timer = 0;
        timerPanel.SetActive(false);
        runCanvas.SetActive(false);
    }

    void Update()
    {
        if(timer > 0)
        {
            timer -= Time.deltaTime;
        }

        if(timer <= 0 && !generalScript.isPause)
        {
            timer = 0;
            runCanvas.SetActive(false);
            PlayerCanvas.SetActive(true);
        }

        if(activate && timer == 0 && !generalScript.isPause)
        {
            timerPanel.SetActive(true);
            if(currentTime == 60)
            {
                timerTxt.SetText("01:00");    
            } else
            {
                timerTxt.SetText("00:" + printTime);
            }
            currentTime -= Time.deltaTime;
            printTime = (int) currentTime;
        }

        if(counter == 5)
        {
            win = true;
            activate = false;
        }

        if(currentTime <= 0)
        {
            activate = false;
            loseToTime = true;
        }
        

        if(Physics.CheckSphere(spherePos.position, 1, playerMask) && !activate && counter!=5)
        {
            runCanvas.SetActive(true);
            PlayerCanvas.SetActive(false);
            timer = 3;
            activate = true;
        }


    }
}
