using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class PistolManagerScript  : MonoBehaviour
{
    // pistol
    public GameObject pistol;
    public Rig rightHand;
    [SerializeField] private GameObject pistolOnField;

    [SerializeField] private LayerMask ammoMask;

    [SerializeField] private GameObject player;
    [SerializeField] private GameObject interact;
    [SerializeField] private TMPro.TMP_Text InteractText;

    bool anim = false;
    public static bool getPistol;
    public bool isInit;
    AmmoClass ammo;

    public int currAmmo, extraAmmo, megazineSize;
    public float fireRate, bulletSpeed, bulletDrop;
    public int damage;
    public Transform shootAtPosition;

    MouseVIew mv;
    RaycastHit hit;

    void Start()
    {
        // init pistol attribute
        

        pistol.SetActive(false);
        rightHand.weight = 0;
        ammo = new AmmoClass();
        mv = GetComponent<MouseVIew>();
    }


    void Update()
    {
        hit = mv.rayAim(8f);
        if (questScript.state == 1)
        {
            if(hit.transform && hit.transform.CompareTag("pistol"))
            {
                interact.SetActive(true);
                InteractText.SetText("Press [F] to pickup weapon");
                if (Input.GetKeyDown(KeyCode.F) )
                {
                    pistolOnField.SetActive(false);
                    pistol.SetActive(true);

                    anim = true;
                }
            }
            if(rightHand.weight == 1)
            {
                anim = false;
                getPistol = true;
                isInit = true;
                WeaponManagerScript.isPistol = true;
            }
        
            if (anim)
            {
                rightHand.weight += Time.deltaTime;
            }

            
        }

        if (getPistol)
        {
            ammoPickup();
            if (((currAmmo == 0 && extraAmmo != 0) || Input.GetKeyDown(KeyCode.R)) && WeaponManagerScript.isPistol) 
            {
                ammo.reload(currAmmo, extraAmmo, megazineSize);
                currAmmo = ammo.currAmmo;
                extraAmmo = ammo.extraAmmo;
            }
            //Debug.Log(currAmmo + " " + extraAmmo);
        }

    }


    void ammoPickup()
    {
        bool rayHitAmmo = hit.transform && hit.transform.CompareTag("PistolAmmo");
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
                    extraAmmo = ammo.plusAmmo(extraAmmo, megazineSize);
                }

                if (nearAmmo)
                {
                    Collider[] ammos = Physics.OverlapSphere(player.transform.position, 1.5f, ammoMask);
                    foreach(Collider ammo in ammos)
                    {
                        if (ammo.transform.CompareTag("PistolAmmo"))
                        {
                        ammo.transform.gameObject.SetActive(false);
                        extraAmmo =  this.ammo.plusAmmo(extraAmmo, megazineSize);
                        }
                        break;
                    }
                }

            }
        }
    }

 


}
