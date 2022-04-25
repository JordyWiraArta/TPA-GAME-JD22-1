using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponManagerScript : MonoBehaviour
{

    public static bool isPistol = false;
    public static bool isRifle = false;
    private bool isAnim;
    private float weight;
    [SerializeField] private RawImage pistolImage;
    [SerializeField] private RawImage rifleImage;
    [SerializeField] private TMPro.TMP_Text ammoCount;
    PistolManagerScript Pistol;
    RifleManagerScript Rifle;

    private void Start()
    {
        Pistol = GetComponent<PistolManagerScript>();
        Rifle = GetComponent<RifleManagerScript>();
        isAnim = false;
        ammoCount.SetText("");
    }

    private void Update()
    {
        weaponUI();

        if (PistolManagerScript.getPistol)
        {
            
            if (questScript.state >=1 && Input.GetKeyDown(KeyCode.Alpha1) && !isPistol && !questScript.nearAsuna)
            {
                pistolImage.color = new Color(1, 1, 1, 1);
                isPistol = true;
                isRifle = false;
                Pistol.pistol.SetActive(true);
                    if(Rifle.getRifle)
                    {
                        rifleImage.color = new Color(1, 1, 1, 0.9f);
                        Rifle.rifle.SetActive(false);
                        Rifle.rightHand.weight = 0;
                    }
                isAnim = true;
            } else if((questScript.state >= 1 && Input.GetKeyDown(KeyCode.Alpha1) && isPistol) || (isPistol && questScript.nearAsuna))
            {
                isPistol = false;
                Pistol.pistol.SetActive(false);
                Pistol.rightHand.weight = 0;
                ammoCount.SetText("");
                pistolImage.color = new Color(1, 1, 1, 0.9f);
            }
        }

        if (Rifle.getRifle)
        {
            if (questScript.state >=3 && Input.GetKeyDown(KeyCode.Alpha2) && !isRifle && !questScript.nearAsuna)
            {
                rifleImage.color = new Color(1, 1, 1, 1);
                pistolImage.color = new Color(1, 1, 1, 0.9f);
                isRifle = true;
                isPistol = false;
                Rifle.rifle.SetActive(true);
                Pistol.pistol.SetActive(false);
                Pistol.rightHand.weight = 0;
                isAnim = true;
            } else if((questScript.state >= 3 && Input.GetKeyDown(KeyCode.Alpha2) && isRifle) || (isRifle && questScript.nearAsuna))
            {
                isRifle = false;
                Rifle.rifle.SetActive(false);
                Rifle.rightHand.weight = 0;
                ammoCount.SetText("");
                rifleImage.color = new Color(1, 1, 1, 0.9f);
            }
        }


        animationSwitch();

    }

    private void weaponUI()
    {
        if (Pistol.isInit)
        {
            pistolImage.color = new Color(1, 1, 1, 1);
            Pistol.isInit = false;

        }

        if (Rifle.isInit)
        {
            rifleImage.color = new Color(1, 1, 1, 1);
            Rifle.isInit = false;
        }

        if (isPistol)
        {
            ammoCount.SetText(Pistol.currAmmo + "/" + Pistol.extraAmmo);
        }

        if (isRifle)
        {
            ammoCount.SetText(Rifle.currAmmo + "/" + Rifle.extraAmmo);
        }
    }

    private void animationSwitch()
    {
        if (isAnim && isPistol)
        {
            Pistol.rightHand.weight += Time.deltaTime;
            weight = Pistol.rightHand.weight;
        }

        if (isAnim && isRifle)
        {
            Rifle.rightHand.weight += Time.deltaTime;
            weight = Rifle.rightHand.weight;
        }

        if (weight >= 1)
        {
            isAnim = false;
        }
    }
        
}
