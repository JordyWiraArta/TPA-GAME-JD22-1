using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class questScript : MonoBehaviour
{
    [SerializeField] private LayerMask asuna;
    private GameObject interactAsuna;
    public GameObject questUI1;
    public GameObject questUI2;
    public GameObject asunaQuestMessage1;

    bool quest1, isOngoing, quest2, talk, nearAsuna;

    // Start is called before the first frame update
    void Start()
    {
        interactAsuna = GameObject.Find("Interact");
        quest1 = true;
        isOngoing = false;
        quest2 = false;
        questUI1.SetActive(true);
        questUI2.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        nearAsuna = Physics.CheckSphere(transform.position, 1, asuna);
        viewInteract();
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
            if (quest1)
            {
                if (talk)
                {
                    if (Input.GetKeyDown(KeyCode.F))
                    {
                        asunaQuestMessage1.SetActive(false);
                        quest1 = false;
                        quest2 = true; 
                        talk = false;
                    }
                }else if (Input.GetKeyDown(KeyCode.F))
                {
                    talk = true;
                    asunaQuestMessage1.SetActive(true);
                }
                
            }
            else if (quest2)
            {
                questUI2.SetActive(true);
                questUI1.SetActive(false);
                isOngoing = true;
            }
        }
        else
        {

        }

    }

}
