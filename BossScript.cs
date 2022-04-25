using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour
{
    private int maxHealth, damage, currHealth;
    private float fireRate, fireRateTimer;
    private float bulletSpeed, bulletDrop;
    private float noticeRadius, attackRadius;

    private Transform shootAtPosition;
    [SerializeField] Transform player;
    [SerializeField] LayerMask playerMask;
    [SerializeField] GameObject bullet;
    [SerializeField] HealthScript healthBar;
    [SerializeField] Transform spherePos;
    [SerializeField] GameObject healthUI;


    private AudioSource audioSource;
    [SerializeField] private AudioClip sound;
    private bool isAttack;
    public static bool bossDed, isBoss;

    // Start is called before the first frame update
    void Start()
    {
        healthUI.SetActive(false);
        maxHealth = 2000;
        currHealth = maxHealth;
        fireRate = 0.05f;
        fireRateTimer = fireRate;
        damage = 1;
        bulletSpeed = 300;
        bulletDrop = 30;
        noticeRadius = 35;
        attackRadius = 50;
        healthBar.setMaxHealth(maxHealth);
        shootAtPosition = transform.Find("shootAtPosition");
    }

    // Update is called once per frame
    void Update()
    {
        attacked();
        isAttack = Physics.CheckSphere(transform.position, attackRadius, playerMask);
        if (isAttack)
        {
            transform.LookAt(player.position);
            if(fireOrNot()) fire();
        }
        if (bulletScript.bulletCollide != null && bulletScript.bulletCollide.transform.name.Equals("Player"))
        {
            PlayerStatus.currHealth -= damage;
            bulletScript.bulletCollide = null;
        }

        if(Physics.CheckSphere(spherePos.position, 100, playerMask))
        {
            healthUI.SetActive(true);
        }
    }

    private void died()
    {
        if(currHealth <= 0)
        {
            bossDed = true;
        }
    }

    private void attacked()
    {
        if (bulletScript.bulletCollide != null && bulletScript.bulletCollide.transform.gameObject == transform.gameObject)
        {
            if (WeaponManagerScript.isPistol)
            {
                currHealth -= 70;
            }
            if (WeaponManagerScript.isRifle)
            {
                currHealth -= 35;
            }
            healthBar.setHealth(currHealth);
            bulletScript.bulletCollide = null;
        }
    }

    public bool fireOrNot()
    {
        fireRateTimer += Time.deltaTime;
        if (fireRateTimer < fireRate) return false;
        return true;
    }

    private void fire()
    {
        fireRateTimer = 0;
        shootAtPosition.LookAt(player.transform);
        audioSource.PlayOneShot(sound);
        GameObject shootBullet = Instantiate(bullet, shootAtPosition.position, shootAtPosition.rotation);
        Rigidbody rigidBody = shootBullet.GetComponent<Rigidbody>();
        rigidBody.AddForce(shootAtPosition.forward * bulletSpeed, ForceMode.Impulse);
    }
}
