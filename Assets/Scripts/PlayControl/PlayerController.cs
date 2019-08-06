using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float runSpeed = 0.2f;
    private float horizontalMove = 0f;
    private Rigidbody2D m_Rigidbody2D;
    private Vector3 m_Velocity = Vector3.zero;
    private SpriteRenderer spriteRenderer;
    [Range(0, .3f)]
    [SerializeField]
    private float m_MovementSmoothing = .05f;
    [SerializeField]
    private float m_JumpForce = 400f;
    private bool jump = false;

    [SerializeField]
    private Transform m_LGroundCheck;
    [SerializeField]
    private Transform m_RGroundCheck;
    [SerializeField]
    private LayerMask m_WhatIsGround;
    [SerializeField]
    private bool m_Grounded;

    public Animator animator;

    [Header("Events")]
    [Space]

    public UnityEvent OnLandEvent;

    private void Start()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (OnLandEvent == null)
            OnLandEvent = new UnityEvent();

    }

    private void Update()
    {
        //在左腳判定點與右腳判定點的矩形範圍內有包含地板會回傳int，這邊bool
        m_Grounded = Physics2D.OverlapArea(m_LGroundCheck.position, m_RGroundCheck.position, m_WhatIsGround);

        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        if (Input.GetButtonDown("Jump") && m_Grounded == true)
        {
            jump = true;
            animator.SetBool("IsJumping", true);
        }

    }
    void FixedUpdate()
    {
        Move(horizontalMove * Time.fixedDeltaTime, jump);
        jump = false;
        bool wasGrounded = m_Grounded;
        Debug.Log("wasGround"+wasGrounded);
        Debug.Log("m_ground" + m_Grounded);
        m_Grounded = false;

        if (m_Grounded == true)
        {
            if (wasGrounded)
                OnLandEvent.Invoke();
            Debug.Log("inhere");
        }

    }


    public void Move(float move, bool jump)
    {
        Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
        m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);
        if (move < 0)
        {
            Flip(true);
        }
        else if (move > 0)
        {
            Flip(false);
        }

        if (jump)
        {
            m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
        }
    }

    public void OnLanding()
    {
        animator.SetBool("IsJumping", false);
        Debug.Log("jump is false");
    }
    //倒轉圖案
    private void Flip(bool isflip)
    {
        spriteRenderer.flipX = isflip;
    }


}