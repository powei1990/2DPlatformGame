using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //private CharacterController2D controller;
    [SerializeField]
    private float runSpeed = 0.2f;
    private float horizontalMove = 0f;
    private Rigidbody2D m_Rigidbody2D;
    private Vector3 m_Velocity = Vector3.zero;
    [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;
    private void Start()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {

        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        //Debug.Log(horizontalMove);
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        Move(horizontalMove * Time.fixedDeltaTime);

    }
    public void Move(float move)
    {


        // Move the character by finding the target velocity
        Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
        Debug.Log(targetVelocity);
        // And then smoothing it out and applying it to the character
        m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

        // If the input is moving the player right and the player is facing left...
        if (move > 0)
        {
            // ... flip the player.
            Flip();
        }
        // Otherwise if the input is moving the player left and the player is facing right...
        else if (move < 0)
        {
            // ... flip the player.
            Flip();
        }
    }



    private void Flip()
    {
        //// Switch the way the player is labelled as facing.
        //m_FacingRight = !m_FacingRight;

        //// Multiply the player's x local scale by -1.
        //Vector3 theScale = transform.localScale;
        //theScale.x *= -1;
        //transform.localScale = theScale;
    }
}