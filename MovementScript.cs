using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private float turnSpeed;
    [SerializeField] private CharacterController playerController;

    [SerializeField] private float groundY;
    [SerializeField] private LayerMask groundMaskLayer;
    [SerializeField] private float height, directionalSpeed;
    
    [SerializeField] private AudioClip walkSound;

    private float gravity = -9.8f;
    private Vector3 velocity;

    private float timer;
    private AudioSource audiosource;
    private bool isJump, isHorizontal, isVertical;

    [SerializeField] private Transform teleportLocation;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
        animator = GetComponent<Animator>();
        velocity.y = transform.position.y;
        audiosource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

        if (velocity.y <0 && isGrounded())
        {
            velocity.y = 0;
        }

        if(timer >= 0)
        {
            timer -= Time.deltaTime;
        }

        if (!questScript.talk)
        {
            if (isGrounded() == true && velocity.y == 0)
            {
                animator.SetFloat("Pos X", Input.GetAxis("Horizontal"));
                animator.SetFloat("Pos Y", Input.GetAxis("Vertical"));

                sound();
                animator.SetBool("ISJUMP", false);

                if (Input.GetButtonDown("Jump"))
                {
                    isJump = true;
                    animator.SetBool("ISJUMP", true);
                }
            } 


            else if (!isJump && isGrounded() == false)
            {
                animator.SetBool("ISJUMP", true);
            }
            
            if(isJump)
            {
                velocity.y = Mathf.Sqrt(height * -2 * gravity);
                isJump = false;
            }
       
            velocity.y += (gravity * Time.deltaTime);
            playerController.Move(velocity * Time.deltaTime);
        }
        else
        {
            animator.SetFloat("Pos X", 0);
            animator.SetFloat("Pos Y", 0);
        }

        if (questScript.isMoved) teleportPlayer();
    }

    private void sound()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            if (timer <= 0)
            {
                audiosource.PlayOneShot(walkSound);
                timer = 0.6f;
            }
        }
    }

    private bool isGrounded()
    {
        if (Physics.CheckSphere(transform.position, groundY, groundMaskLayer))
        {
            return true;
        }

        return false;
    }

    private void teleportPlayer()
    {
        transform.position = teleportLocation.position;
        questScript.isMoved = false;
    }
}
