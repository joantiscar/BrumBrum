using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    public CharacterController controller;  
    public Transform cam;
    public float speed = 12f;
    public float gravity = -9.81f;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    public float jumpHeight = 10f;

    public Transform groundChek;
    public float groundDistance = 0.1f;
    public LayerMask groundMask;
    //Vector3 velocity;
    private float verticalVelocity;

    bool isGrounded;

    bool isTalking = false;

    private Animator animator;


    void Start()
    {
        animator=GetComponentInChildren<Animator>();
    }


    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundChek.position, groundDistance, groundMask);

        if(!isTalking){

            if(isGrounded)
            {
                animator.SetBool("OnGround", true);
                verticalVelocity = -gravity * Time.deltaTime;
                /*
                if(Input.GetKeyDown(KeyCode.Space) && !(animator.GetCurrentAnimatorStateInfo(0).IsName("Attack")))
                {
                    verticalVelocity = jumpHeight;
                    //animator.Play("Jump");
                    //animator.SetBool("OnGround", false);

                    animator.SetBool("Jumping", true);
                }
                */
            }
            else
            {
                verticalVelocity -= -gravity * Time.deltaTime;
                animator.SetBool("OnGround", false);
            }
            /*
            if (Input.GetKeyDown(KeyCode.Space) && (animator.GetCurrentAnimatorStateInfo(0).IsName("Move Blend")))
            {
                verticalVelocity = jumpHeight;
                animator.Play("Jump");
            }
            */

            Vector3 moveVectorJump = new Vector3(0,verticalVelocity,0);
            controller.Move(moveVectorJump * Time.deltaTime);

            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");
            Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

            /*if(Input.GetButtonDown("Jump") && isGrounded)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }*/



            Vector3 auxVec = new Vector3(0, 0, 0);
            

            if (direction.magnitude >= 0.1f && 
                    !animator.GetCurrentAnimatorStateInfo(0).IsName("Attack") && !animator.GetCurrentAnimatorStateInfo(0).IsName("Land"))
            {
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                controller.Move(moveDir.normalized * speed * Time.deltaTime);



                //velocity.y += gravity * Time.deltaTime;

                //controller.Move(velocity * Time.deltaTime);

                auxVec = moveDir.normalized;
            }
            float auxVel = Mathf.Sqrt(auxVec[0] * auxVec[0] + auxVec[2] * auxVec[2]);
            //Debug.Log(auxVel);
            animator.SetFloat("Velocity", auxVel);

            /*
            if (Input.GetKeyDown(KeyCode.Mouse0) && animator.GetBool("OnGround"))
            {
                //Debug.Log("Attack");
                animator.Play("Attack");
            }
            */
        }
        else
        {
            animator.SetFloat("Velocity", 0);
            animator.Play("Move Blend");
        }
    }
    public void isTalkKing()
    {
        isTalking = !isTalking;
    }
}
