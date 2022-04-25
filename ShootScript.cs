using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ShootScript : MonoBehaviour
{

    private float fireRateTimer, fireRate, bulletSpeed;
    [SerializeField] private GameObject bullet;
    [SerializeField] private AudioClip pistolShot;

    private int currAmmo;
    private AudioSource audioSource;
    private Transform shootAtPosition;

    private MouseVIew mv;
    private PistolManagerScript Pistol;
    private RifleManagerScript Rifle;

    private float recoilTime;
    [SerializeField] private float duration;

    // Start is called before the first frame update
    void Start()
    {
        mv = GetComponent<MouseVIew>();
        Pistol = GetComponent<PistolManagerScript>();
        Rifle = GetComponent<RifleManagerScript>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        getDataWeapon();

        if (fireOrNot() && !questScript.talk)
        {
            fire();
            RaycastHit hit = mv.rayAim(Mathf.Infinity);
            if(questScript.state == 2)
            {
                if (hit.transform.CompareTag("Target") && questScript.counter < 10)
                {
                    questScript.counter += 1;

                } 
            }

            if(questScript.state == 3)
            {
                if (hit.transform.CompareTag("Target") && questScript.counter < 50)
                {
                    questScript.counter += 1;

                }
            }
            currAmmo--;

            
        }

        if(recoilTime > 0)
        {
            mv.axisY.Value -= 1 * Time.deltaTime * 5;
            recoilTime -= Time.deltaTime * 5;
        }

        updateDataWeapon();
    }

    private void getDataWeapon()
    {
        if (WeaponManagerScript.isPistol)
        {
            fireRate = 1 / Pistol.fireRate;
            bulletSpeed = Pistol.bulletSpeed;
            shootAtPosition = Pistol.shootAtPosition;
            currAmmo = Pistol.currAmmo;

        }
        else if (WeaponManagerScript.isRifle)
        {
            fireRate = 1 / Rifle.fireRate;
            bulletSpeed = Rifle.bulletSpeed;
            shootAtPosition = Rifle.shootAtPosition;
            currAmmo = Rifle.currAmmo;
        }
        else
        {
            fireRate = 0;
        }
    }

    private void updateDataWeapon()
    {
        if (WeaponManagerScript.isPistol)
        {
            Pistol.currAmmo = currAmmo;
        }
        if (WeaponManagerScript.isRifle)
        {
            Rifle.currAmmo = currAmmo;
        }
    }

    private bool fireOrNot()
    {
        fireRateTimer += Time.deltaTime;
        if (fireRateTimer < fireRate) return false;
        if (currAmmo == 0) return false;
        if(WeaponManagerScript.isPistol && Input.GetKeyDown(KeyCode.Mouse0))
        {
            return true;
        }
        if(WeaponManagerScript.isRifle && Input.GetKey(KeyCode.Mouse0))
        {
            return true;
        }
        return false;
    }

    private void fire()
    {
        fireRateTimer = 0;
        shootAtPosition.LookAt(mv.aimSphere);
        audioSource.PlayOneShot(pistolShot);
        GameObject shootBullet = Instantiate(bullet, shootAtPosition.position, shootAtPosition.rotation);
        Rigidbody rigidBody = shootBullet.GetComponent<Rigidbody>();
        rigidBody.AddForce(shootAtPosition.forward * bulletSpeed, ForceMode.Impulse);

        recoil();
    }

    private void recoil()
    {
        recoilTime = duration;
    }

}
