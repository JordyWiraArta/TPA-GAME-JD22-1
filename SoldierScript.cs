using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierScript : MonoBehaviour
{
    public Transform player;
    public AudioClip sound;
    public GameObject bullet;
    private Transform shootAtPosition;
    private AudioSource audioSource;

    private int maxHealth, damage, currHealth;
    private float fireRate, fireRateTimer;
    private float bulletSpeed, bulletDrop;
    private float noticeRadius, attackRadius;
    [SerializeField] private LayerMask playerMask;

    private Animator moveAnimate;
    private bool isAttack, isDead;
    private float timer;

    private CharacterController soldier;
    private Vector3 normalPosition;
    private Vector3 drop;
    private HealthScript hs;

    private void Start()
    {
        timer = 0;
        maxHealth = 100;
        currHealth = maxHealth;
        fireRate = 0.1f;
        fireRateTimer = fireRate;
        damage = 1;
        bulletSpeed = 300;
        bulletDrop = 30;
        noticeRadius = 35;
        attackRadius = 15;
        moveAnimate = GetComponent<Animator>();
        shootAtPosition = GameObject.Find("shootAtPosition").transform;
        normalPosition = transform.position;
        soldier = GetComponent<CharacterController>();
        audioSource = GetComponent<AudioSource>();
        hs = GetComponentInChildren<HealthScript>();
        hs.setMaxHealth(maxHealth);
    }

    void Update()
    {
        if (!isDead)
        {
            attacked();
            died();
            isAttack = Physics.CheckSphere(transform.position, attackRadius, playerMask);
            if (!isAttack)
            {
                movement();
            } else 
            {
                moveAnimate.SetFloat("PosY", 0);
                transform.LookAt(player.transform);
                if (fireOrNot())
                {
                    fire();
                }
            }
            if (bulletScript.bulletCollide != null && bulletScript.bulletCollide.transform.name.Equals("Player"))
            {
                PlayerStatus.currHealth -= damage;
                bulletScript.bulletCollide = null;
            }
        }

    }

    private void died()
    {
        if (questScript.state == 4 && currHealth <= 0 )
        {
            questScript.counter += 1;
            isDead = true;
            transform.gameObject.SetActive(false);
        } else if(questScript.state == 5 && currHealth <= 0)
        {
            TimerScript.counter += 1;
            isDead = true;
            transform.gameObject.SetActive(false);
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
            hs.setHealth(currHealth);
            bulletScript.bulletCollide = null;
        }
    }

    private void movement()
    {
       
    moveAnimate.SetFloat("PosY", 1);

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
