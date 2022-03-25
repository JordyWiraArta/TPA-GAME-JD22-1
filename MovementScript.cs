using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private Camera camField;
    [SerializeField] private float turnSpeed;
    private CharacterController playerController;

    [SerializeField] float groundY;
    [SerializeField] LayerMask groundMaskLayer;
    Vector3 spherePos;

    private float gravity = -9.8f;
    private Vector3 velocity;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        setGravity();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("Pos X", Input.GetAxis("Horizontal"));
        animator.SetFloat("Pos Y", Input.GetAxis("Vertical"));
  
    }

    bool isGrounded()
    {
        spherePos = new Vector3(transform.position.x, transform.position.y - groundY, transform.position.z);
        if (Physics.CheckSphere(spherePos, playerController.radius - 0.1f, groundMaskLayer)) return true;
        else return false;
    }

    void setGravity()
    {
        if (!isGrounded()) velocity.y += gravity * Time.deltaTime;

        playerController.Move(velocity * Time.deltaTime);
    }
}
