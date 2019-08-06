using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    //private CharacterController2D controller;
    [SerializeField]
    private float runSpeed = 0.2f;
    private float horizontalMove = 0f;
    private Rigidbody2D m_Rigidbody2D;
    private Vector3 m_Velocity = Vector3.zero;
    private SpriteRenderer spriteRenderer;
    [Range(0, .3f)] [SerializeField]
    private float m_MovementSmoothing = .05f;
    [SerializeField]
    private float m_JumpForce = 400f;
    private bool jump = false;
    [SerializeField]
    private Transform m_GroundCheck;
    [SerializeField]
    private LayerMask m_WhatIsGround;
    const float k_GroundedRadius = .2f;
    private bool m_Grounded;

    public UnityEvent OnLandEvent;

    private void Awake()
    {
        if (OnLandEvent == null)
            OnLandEvent = new UnityEvent();
    }

    private void Start()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {

        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
        }
        //Debug.Log(horizontalMove);
    }
    void FixedUpdate()
    {
        bool wasGrounded = m_Grounded;
        m_Grounded = false;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
        //Debug.Log(colliders);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                m_Grounded = true;
                if (!wasGrounded)
                    OnLandEvent.Invoke();
            }
        }

        Move(horizontalMove * Time.fixedDeltaTime,jump);
        jump = false;
        
    }
    public void Move(float move,bool jump)
    {
        Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
        //Debug.Log(targetVelocity);
        m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);
        if (move < 0)
        {
            Flip(true);
        }
        else if (move>0)
        {
            Flip(false);
        }

        if (jump)
        {
            // Add a vertical force to the player.
            //m_Grounded = false;
            m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
        }
    }


    //倒轉圖案
    private void Flip(bool isflip)
    {
            spriteRenderer.flipX = isflip;
    }
}