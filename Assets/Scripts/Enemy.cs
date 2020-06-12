using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Animator anim;
    //private bool alive;
    public GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        //alive = true;
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("Distance", Vector2.Distance(transform.position, Player.transform.position));
    }

    private void Hit()
    {
        Debug.Log("Get Hit!!");
    }
}
