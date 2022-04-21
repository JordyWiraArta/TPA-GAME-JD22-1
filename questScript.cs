using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class questScript : MonoBehaviour
{
    [SerializeField] private LayerMask asuna;
    [SerializeField] private GameObject interactAsuna;
    [SerializeField] private GameObject dialogPanel;
    [SerializeField] private TMPro.TMP_Text questMsg;
    [SerializeField] private TMPro.TMP_Text asunaDialog;
    [SerializeField] private TMPro.TMP_Text interactText;

    public static int state = 0;
    int dial = 0;
    bool isOngoing, nearAsuna;
    public static bool talk;
    string[] questMessages = {
        "Talk to Asuna",
        "Pick up the pistol",
        "",
        "",
        "",
        "",
        ""
    };

    string[] questDialog =
    {

    };

    // Start is called before the first frame update
    void Start()
    {
        isOngoing = false;
        talk = false;
        questMsg.SetText(questMessages[0]);
        dialogPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        nearAsuna = Physics.CheckSphere(transform.position, 1, asuna);
        viewInteract();
        onGoingMission();
        if (nearAsuna)
        {
            questState();
        }
    }

    public void viewInteract()
    {
        if (nearAsuna && !talk)
        {
            interactAsuna.SetActive(true);
            interactText.SetText("Press [f] to interact");
        }
        else
        {
            interactAsuna.SetActive(false);
        }
    }

    public void questState()
    {
        if (!isOngoing)
        {
            switch (state)
            {
                case 0:
                    dialogQ1();
                    break;

                case 1:
                    dialogQ2();
                    break;
            }
        }
        else
        {
            if(talk){
                if (dial == 0)
                {
                    talk = false;
                } else if (Input.GetKeyDown(KeyCode.Space))
                {
                    dialogPanel.SetActive(false);
                    dial = 0;
                }
            } else if (Input.GetKeyDown(KeyCode.F))
            {
                dial += 1;
                dialogPanel.SetActive(true);
                asunaDialog.SetText("Finish your first Mission!");
                talk = true;
            }
        }

    }

    private void dialogQ1()
    {
        if (talk)
        {
            if (dial == 3)
            {
                talk = false;
                dial = 0;
                state += 1;
                questMsg.SetText(questMessages[state]);
            }
            else if (dial == 2 && Input.GetKeyDown(KeyCode.Space))
            {
                dial += 1;
                dialogPanel.SetActive(false);
            }
            else if (dial == 1 && Input.GetKeyDown(KeyCode.Space))
            {
                asunaDialog.SetText("Go pick up the pistol in the armory!");
                dial += 1;
            }
            else if (Input.GetKeyDown(KeyCode.Space))
            {
                asunaDialog.SetText("Here is your first mission!");
                dial += 1;
            }
        }
        else if (Input.GetKeyDown(KeyCode.F))
        {
            dialogPanel.SetActive(true);
            asunaDialog.SetText("Hello there new Soldier!");
            talk = true;
        }
    }

    private void dialogQ2()
    {
        if (talk)
        {
            if (dial == 2)
            {
                talk = false;
                dial = 0;
                state += 1;
                questMsg.SetText(questMessages[state]);
            }
            else if (dial == 1 && Input.GetKeyDown(KeyCode.Space))
            {
                dial += 1;
                dialogPanel.SetActive(false);
            }
            else if (Input.GetKeyDown(KeyCode.Space))
            {
                asunaDialog.SetText("Next try to shoot those pistol in the shooting target!");
                dial += 1;
            }
        }
        else if (Input.GetKeyDown(KeyCode.F))
        {
            dialogPanel.SetActive(true);
            asunaDialog.SetText("Great!");
            talk = true;
        }
    }

    private void onGoingMission()
    {
        switch (state)
        {
            case 1:
                if (PickUpForPistolScript.getPistol)
                {
                    questMsg.color = new Color(0, 1, 0, 1);
                    isOngoing = false;
                }
                else isOngoing = true;
                break;
        }
    }

}
