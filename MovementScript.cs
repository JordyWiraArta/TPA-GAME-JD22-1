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

    private float gravity = -9.8f;
    private Vector3 velocity;

    private bool isJump, isHorizontal, isVertical;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        velocity.y = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (velocity.y <0 && isGrounded())
        {
            velocity.y = 0;
        }

        
        if (isGrounded() == true && velocity.y == 0)
        {
            animator.SetFloat("Pos X", Input.GetAxis("Horizontal"));
            animator.SetFloat("Pos Y", Input.GetAxis("Vertical"));
            animator.SetBool("ISJUMP", false);

            if (Input.GetButtonDown("Jump"))
            {
                isJump = true;
                animator.SetBool("ISJUMP", true);
            }
        } else if(velocity.y > 0)
        {
           /* isHorizontal = Input.GetButtonDown("Horizontal");
            isVertical = Input.GetButtonDown("Vertical");
            if (isHorizontal)
            {
                velocity.x = Input.GetAxis("Horizontal") * directionalSpeed;
                isHorizontal = false;
            }

            if (isVertical)
            {
                velocity.z = Input.GetAxis("Vertical") * directionalSpeed;
                isVertical = false;
            }*/
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

    private bool isGrounded()
    {
        if (Physics.CheckSphere(transform.position, groundY, groundMaskLayer))
        {
            return true;
        }

        return false;
    }
}
