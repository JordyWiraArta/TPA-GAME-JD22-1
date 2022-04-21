using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class PickUpForPistolScript  : MonoBehaviour
{
    // pistol
    [SerializeField] private GameObject pistol;
    [SerializeField] private Rig rightHand;
    [SerializeField] private GameObject pistolOnField;

    [SerializeField] private LayerMask weaponMask;
    [SerializeField] private LayerMask ammoMask;
    private Vector2 screenCenter = new Vector2(Screen.width / 2, Screen.height / 2);

    [SerializeField] private GameObject player;
    [SerializeField] private GameObject interact;
    [SerializeField] private TMPro.TMP_Text InteractText;

    bool anim = false;

    public static bool plusAmmo;
    public static bool getPistol;

    void Start()
    {
        // init pistol attribute
        

        pistol.SetActive(false);
        rightHand.weight = 0;
        
    }


    void Update()
    {
        
        Ray ray = Camera.main.ScreenPointToRay(screenCenter);
        if(questScript.state >= 1)
        {
            if (Input.GetKeyDown(KeyCode.F) && Physics.Raycast(ray, out RaycastHit hit, 4f, weaponMask) )
            {
                pistolOnField.SetActive(false);
                pistol.SetActive(true);

                anim = true;
            }

            if(rightHand.weight >= 1)
            {
                anim = false;
                getPistol = true;
            }
        
            if (anim)
            {
                rightHand.weight += Time.deltaTime;
            }

            
        }

        if (getPistol)
        {
            ammoPickup(ray);

        }

    }


    void ammoPickup(Ray ray)
    {
        bool rayHitAmmo = Physics.Raycast(ray, out RaycastHit hit, 8f, ammoMask);
        bool nearAmmo = Physics.CheckSphere(player.transform.position, 1.5f, ammoMask);
        if (rayHitAmmo || nearAmmo)
        {
            interact.SetActive(true);
            InteractText.SetText("Press [F] to pick Ammo");
            if (Input.GetKeyDown(KeyCode.F))
            {
                if (rayHitAmmo)
                {
                    hit.transform.gameObject.SetActive(false);
                }

                if (nearAmmo)
                {
                    Collider[] ammos = Physics.OverlapSphere(player.transform.position, 1.5f, ammoMask);
                    foreach(Collider ammo in ammos)
                    {
                        ammo.transform.gameObject.SetActive(false);
                        break;
                    }
                }

                plusAmmo = true;
            }
        } else
        {
            interact.SetActive(false);
        }
    }

 


}
