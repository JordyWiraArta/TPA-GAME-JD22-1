using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class questScript : MonoBehaviour
{
    [SerializeField] private LayerMask asuna;
    [SerializeField] private LayerMask telporterMask;
    [SerializeField] private GameObject interactAsuna;
    [SerializeField] private GameObject dialogPanel;
    [SerializeField] private GameObject questPanel;
    [SerializeField] private TMPro.TMP_Text questMsg;
    [SerializeField] private TMPro.TMP_Text asunaDialog;
    [SerializeField] private TMPro.TMP_Text interactText;
    [SerializeField] private Animator tunnelAnimate;
    [SerializeField] private GameObject invinsibleDoorWay;

    public static int state = 0;
    int dial = 0;
    float timer = 0;
    bool isOngoing;
    public static bool nearAsuna;
    public static bool talk;
    public static int counter;
    public static bool isMoved;

    // Start is called before the first frame update
    void Start()
    {
        isOngoing = false;
        talk = false;
        questMsg.SetText("Talk to Asuna");
        dialogPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        nearAsuna = Physics.CheckSphere(transform.position, 3, asuna);
        viewInteract();
        onGoingMission();
        if (nearAsuna)
        {
            questState();
        }
    }

    private void LateUpdate()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            tunnelAnimate.SetBool("isAnimate", false);
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

                case 2:
                    dialogQ3();
                    break;

                case 3:
                    dialogQ4();
                    break;

                case 4:
                    dialogQ5();
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
                asunaDialog.SetText("Finish your Mission!");
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
                questMsg.SetText("Pick up the pistol");
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

    private void dialogQ3()
    {
        if (talk)
        {
            if (dial == 2)
            {
                talk = false;
                dial = 0;
                state += 1;
                questMsg.color = Color.white;
                counter = 0;
            }
            else if (dial == 1 && Input.GetKeyDown(KeyCode.Space))
            {
                dial += 1;
                dialogPanel.SetActive(false);
            }
            else if (Input.GetKeyDown(KeyCode.Space))
            {
                asunaDialog.SetText("Now get yourself a rifle, then shoot 50 bullets on the target!");
                dial += 1;
            }
        }
        else if (Input.GetKeyDown(KeyCode.F))
        {
            dialogPanel.SetActive(true);
            asunaDialog.SetText("amazing!");
            talk = true;
        }
    }

    private void dialogQ4()
    {
        if (talk)
        {
            if (dial == 4)
            {
                talk = false;
                dial = 0;
                state += 1;
                questMsg.color = Color.white;
                counter = 0;
                tunnelAnimate.SetBool("isAnimate", true);
                timer = 1;
            } 
            else if(dial == 3)
            {
                dial++;
                dialogPanel.SetActive(false);
            }
            else if(dial == 2 && Input.GetKeyDown(KeyCode.Space))
            {
                dial += 1;
                asunaDialog.SetText("Go and eleminate those soldiers!");
            } 
            else if (dial == 1 && Input.GetKeyDown(KeyCode.Space))
            {
                dial += 1;
                asunaDialog.SetText("the village nearby are being attack by some soldiers");
            }
            else if (Input.GetKeyDown(KeyCode.Space))
            {
                asunaDialog.SetText("We have receive a new mission");
                dial += 1;
            }
        }
        else if (Input.GetKeyDown(KeyCode.F))
        {
            dialogPanel.SetActive(true);
            asunaDialog.SetText("Nice!");
            talk = true;
        }
    }

    private void dialogQ5()
    {
        if (talk)
        {
            if (dial == 3)
            {
                talk = false;
                dial = 0;
                state += 1;
                invinsibleDoorWay.SetActive(false);
                questMsg.SetText("Head to the teleporter and defeat the boss!");
                questMsg.color = Color.white;
                counter = 0;
            }
            else if (dial == 2)
            {
                dial++;
                dialogPanel.SetActive(false);
            }
            else if (dial == 1 && Input.GetKeyDown(KeyCode.Space))
            {
                dial += 1;
                asunaDialog.SetText("Head to the teleporter and defeat the boss!");
            }
            else if (Input.GetKeyDown(KeyCode.Space))
            {
                asunaDialog.SetText("We have sqouted nearby teleporter");
                dial += 1;
            }
        }
        else if (Input.GetKeyDown(KeyCode.F))
        {
            dialogPanel.SetActive(true);
            asunaDialog.SetText("You are doing great!");
            talk = true;
        }
    }

    private void onGoingMission()
    {
        switch (state)
        {
            case 1:
                if (PistolManagerScript.getPistol)
                {
                    questMsg.color = Color.green;
                    isOngoing = false;
                }
                else isOngoing = true;
                break;

            case 2:
                if (counter <= 10)
                {
                    questMsg.color = Color.white;
                    questMsg.SetText("Shoot 10 rounds at the target! (" + counter + "/10)");
                    isOngoing = true;
                    
                    
                }
                if(counter == 10)
                {
                    isOngoing = false;
                    questMsg.color = Color.green;
                }

                break;

            case 3:
                if(counter <= 50)
                {
                    questMsg.SetText("Shoot 50 bullets with the rifle! (" + counter + "/50)");
                    isOngoing = true;
                }

                if(counter == 50)
                {
                    isOngoing = false;
                    questMsg.color = Color.green;
                }
                break;

            case 4:
                if(counter <= 16)
                {
                    questMsg.SetText("Eliminate the soldiers that are attacking the village! ("+ counter + "/16)");
                    isOngoing = true;

                }

                if(counter == 16)
                {
                    isOngoing = false;
                    questMsg.color = Color.green;
                }
                break;

            case 5:
                if (TimerScript.win && Physics.CheckSphere(transform.position, 4, telporterMask))
                {
                    interactAsuna.SetActive(true);
                    interactText.SetText("Press [F] to teleport");
                    if (Input.GetKeyDown(KeyCode.F))
                    {
                        isMoved = true;
                    }
                }
                if (BossScript.isBoss)
                {
                    questPanel.SetActive(false);
                }

                break;
        }
    }

}
