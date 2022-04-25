using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class RifleManagerScript : MonoBehaviour
{
    // pistol
    public GameObject rifle;
    public Rig rightHand;
    [SerializeField] private GameObject rifleOnField;

    [SerializeField] private LayerMask ammoMask;
    private Vector2 screenCenter = new Vector2(Screen.width / 2, Screen.height / 2);

    [SerializeField] private GameObject player;
    [SerializeField] private GameObject interact;
    [SerializeField] private TMPro.TMP_Text InteractText;

    bool anim = false;

    public bool getRifle, isInit;
    AmmoClass ammo;
    public int currAmmo, extraAmmo, megazineSize;
    public float fireRate, bulletSpeed, bulletDrop;
    public int damage;
    public Transform shootAtPosition;

    private MouseVIew mv;
    private RaycastHit hit;

    void Start()
    {
        // init pistol attribute

        ammo = new AmmoClass();
        rifle.SetActive(false);
        rightHand.weight = 0;
        mv = GetComponent<MouseVIew>();
    }


    void Update()
    {
        hit = mv.rayAim(8f);
        if (questScript.state == 3)
        {
            if (hit.transform && hit.transform.CompareTag("rifle"))
            {
                interact.SetActive(true);
                InteractText.SetText("Press [F] to pickup weapon");
                if (Input.GetKeyDown(KeyCode.F))
                {
                    rifleOnField.SetActive(false);
                    rifle.SetActive(true);

                    anim = true;
                }
            }

            if (rightHand.weight >= 1)
            {
                anim = false;
                getRifle = true;
                isInit = true;
                WeaponManagerScript.isRifle = true;
            }

            if (anim)
            {
                rightHand.weight += Time.deltaTime;
            }
        }

        if (getRifle)
        {
            ammoPickup();
            if (((currAmmo == 0 && extraAmmo != 0) || Input.GetKeyDown(KeyCode.R)) && WeaponManagerScript.isRifle)
            {
                ammo.reload(currAmmo, extraAmmo, megazineSize);
                currAmmo = ammo.currAmmo;
                extraAmmo = ammo.extraAmmo;
            }
        }

    }


    void ammoPickup()
    {
        bool rayHitAmmo = hit.transform && hit.transform.CompareTag("RifleAmmo");
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
                    extraAmmo = this.ammo.plusAmmo(extraAmmo, megazineSize);
                }

                if (nearAmmo)
                {
                    Collider[] ammos = Physics.OverlapSphere(player.transform.position, 1.5f, ammoMask);
                    foreach (Collider ammo in ammos)
                    {
                        if (ammo.transform.CompareTag("RifleAmmo"))
                        {
                        ammo.transform.gameObject.SetActive(false);

                        extraAmmo = this.ammo.plusAmmo(extraAmmo, megazineSize);
                        }
                        break;
                    }
                }

            }
        }
    }




}
