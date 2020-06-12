using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    private bool m_Grounded;
    private bool isTouchingWall;
    private bool isWallSliding;
    //private bool isAttacking = false;
    private bool isFacingRight = true;
    private bool canAttack;


    [SerializeField]
    private int wallJumpForce;

    public float wallCheckDistance;
    public float wallSlideSpeed;
    [SerializeField]
    private float runSpeed = 0.2f;
    private float horizontalMove = 0f;
    [SerializeField]
    [Range(0, .3f)]
    private float m_MovementSmoothing = .05f;
    [SerializeField]
    private float m_JumpForce = 400f;
    private float attackTimer;
    private float fireRate = 2;
   
    


    public Transform m_LGroundCheck;

    public Transform m_RGroundCheck;

    public LayerMask m_WhatIsGround;

    public Transform wallCheck;

    private Rigidbody2D m_Rigidbody2D;
    private Vector3 m_Velocity = Vector3.zero;
    private SpriteRenderer spriteRenderer;
 

    private Animator animator;
    private AnimatorStateInfo animState;

    protected Joystick joystick;
    protected Joybutton joybutton;



    private void Start()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        joystick = FindObjectOfType<Joystick>();
        joybutton = FindObjectOfType<Joybutton>();


    }

    private void Update()
    {
        CheckInput();

        CheckIfWallSliding();
        SetAnimationState(ref horizontalMove);

        animState = animator.GetCurrentAnimatorStateInfo(0);
    }
    void FixedUpdate()
    {
        ApplyMovement();
        CheckSurroundings();
    }

    private void CheckInput()
    {
        horizontalMove = (joystick.Horizontal + Input.GetAxisRaw("Horizontal")) * runSpeed;

        if (Input.GetButtonDown("Jump") )
        {
            Jump();

        }

        if (Input.GetButtonDown("Fire1"))
        {
            Attack();

        }
        else if(attackTimer < fireRate)
        {
            attackTimer += Time.deltaTime;

        }
        else if (attackTimer >= fireRate)
        {
            canAttack=true;
        }

    }

    private void ApplyMovement()
    {

        if (m_Grounded)
        {
            Move(horizontalMove * Time.fixedDeltaTime);
        }
        //防止等於零時卡住
        else if (!m_Grounded && !isWallSliding && horizontalMove != 0)
        {

            Move(horizontalMove * Time.fixedDeltaTime);
        }
        else if (!m_Grounded && !isWallSliding && horizontalMove == 0)
        {
            m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, m_Rigidbody2D.velocity.y);
        }

        if (isWallSliding)
        {
            if (m_Rigidbody2D.velocity.y < -wallSlideSpeed)
            {
                m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, -wallSlideSpeed);
            }
        }

    }

    public void Move(float move)
    {
        Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
        m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

    }

    public void Jump()
    {
        if (isWallSliding && !m_Grounded)
        {
            wallJumpForce = isFacingRight ? -1*Mathf.Abs(wallJumpForce) : Mathf.Abs(wallJumpForce);
            m_Rigidbody2D.AddForce(new Vector2(wallJumpForce, m_JumpForce));
            //Debug.Log("wall jump");
            //Debug.Log(wallJumpForce);

        }

        else if (m_Grounded)
        {
            m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
            //Debug.Log("normal jump");
        }

    }

    public void Attack()
    {
        if (animState.IsName("Player_Idle")&&canAttack)
        {
            animator.SetTrigger("Attack1");
            attackTimer = 0f;
        }

        else if (animState.IsName("Player_Attack_1")&&
                  animState.normalizedTime>0.5f)
        {
            animator.SetTrigger("Attack2");
            attackTimer = 0f;
        }
        else if (animState.IsName("Player_Attack_2")&&
                  animState.normalizedTime > 0.1f)
        {
            animator.SetTrigger("Attack3");
            canAttack = false;
        }

    }

    //倒轉圖案
    private void Flip()
    {
        isFacingRight = !isFacingRight;

        transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
    }

    private void CheckSurroundings()
    {
        //在左腳判定點與右腳判定點的矩形範圍內有包含地板會回傳int，這邊bool
        m_Grounded = Physics2D.OverlapArea(m_LGroundCheck.position, m_RGroundCheck.position, m_WhatIsGround);


        isTouchingWall = Physics2D.Raycast(wallCheck.position, transform.right*transform.localScale.x, wallCheckDistance, m_WhatIsGround);
        //Debug.Log(isTouchingWall);
    }

    private void CheckIfWallSliding()
    {
        if (isTouchingWall && !m_Grounded && m_Rigidbody2D.velocity.y < 0)
        {
            isWallSliding = true;
        }
        else
        {
            isWallSliding = false;
        }
    }


    private void SetAnimationState(ref float horizontalMove)
    {
        //跑步
        if (horizontalMove != 0)
        {
            animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        }
        else if (horizontalMove == 0)
        {
            animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
        }

        //著地
        if (m_Grounded)
        {
            animator.SetBool("IsJumping", false);
            animator.SetBool("IsFalling", false);
        }
        else if (!m_Grounded && m_Rigidbody2D.velocity.y > 0)
        {
            animator.SetBool("IsJumping", true);
        }
        else if (!m_Grounded && m_Rigidbody2D.velocity.y < 0)
        {
            animator.SetBool("IsJumping", false);
            animator.SetBool("IsFalling", true);
        }

        //攀牆
        if (isWallSliding)
        {
            animator.SetBool("IsWallSliding", true);
        }
        else if (!isWallSliding)
        {
            animator.SetBool("IsWallSliding", false);
        }

        //面向
        if (isFacingRight && horizontalMove < 0)
        {
            Flip();
        }
        else if (!isFacingRight && horizontalMove > 0)
        {
            Flip();
        }




    }

}