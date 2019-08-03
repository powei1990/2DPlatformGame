using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayController : MonoBehaviour
{
    private float maxSpeed = 0.2f;
    Rigidbody2D rigidbody2d;
    SpriteRenderer spriteRenderer;
    bool controlEnabled = true;
    Vector2 move;
    void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        //float vertical = Input.GetAxis("Vertical");
        if (controlEnabled)
        {
            move = transform.position;
            move.x = move.x + maxSpeed * horizontal;
            Debug.Log(move.x);
            transform.position = move;
            rigidbody2d.MovePosition(move);

            if (move.x > 0.001f)
                spriteRenderer.flipX = false;
            else if (move.x < -0.001f)
                spriteRenderer.flipX = true;
        }
        else
        {
            move.x = 0;
        }
    }



}
