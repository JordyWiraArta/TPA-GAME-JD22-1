using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pistolShootScript : MonoBehaviour
{

    private float fireRateTimer, fireRate, damage, bulletSpeed, bulletDrop;
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform shootAtPosition;

    [SerializeField] private AudioClip pistolShot;
    AudioSource audioSource;

    MouseVIew mv;
    pistolAmmoScript ammo;

    // Start is called before the first frame update
    void Start()
    {
        fireRate = 1;
        fireRateTimer = fireRate;
        damage = 70;
        bulletSpeed = 120;
        bulletDrop = 50;

        mv = GetComponentInParent<MouseVIew>();
        audioSource = GetComponent<AudioSource>();
        ammo = GetComponent<pistolAmmoScript>();
    }

    // Update is called once per frame
    void Update()
    {
        bool cooldown = fireOrNot();
        if (PickUpForPistolScript.getPistol == true && cooldown)
        {
            fire();
        }
    }

    bool fireOrNot()
    {
        fireRateTimer += Time.deltaTime;
        if (fireRateTimer < fireRate) return false;
        if (ammo.currAmmo == 0) return false;
        else if (Input.GetKeyDown(KeyCode.Mouse0)) return true;
        return false;
    }

    void fire()
    {
        fireRateTimer = 0;
        shootAtPosition.LookAt(mv.aimSphere);
        audioSource.PlayOneShot(pistolShot);
        ammo.currAmmo -= 1;
        Debug.Log("" + ammo.currAmmo + " " + ammo.extraAmmo);
        GameObject shootBullet = Instantiate(bullet, shootAtPosition.position, shootAtPosition.rotation);
        Rigidbody rigidBody = shootBullet.GetComponent<Rigidbody>();
        rigidBody.AddForce(shootAtPosition.forward * bulletSpeed, ForceMode.Impulse);

    }

}
