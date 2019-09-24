using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHit : MonoBehaviour
{

    [SerializeField] enum AttacksWhat { Enemy, Player };
    [SerializeField] AttacksWhat attacksWhat;
    private int launchDirection = 1;
    [SerializeField] private GameObject parent;

    //private BoxCollider2D boxCollider2d;


    // Start is called before the first frame update
    void Start()
    {
        //boxCollider2d = GetComponent<BoxCollider2D>();
        //ContactFilter2D contactFilter2D=new
    }

    // Update is called once per frame
    void Update()
    {
        //if (boxCollider2d.enabled)
        //{
        //    Collider2D[] enemiesToDamage = Physics2D.OverlapCollider(boxCollider2d,LayerMask.NameToLayer("Enemy"),);
        //}
    }

    void OnTriggerEnter2D(Collider2D col)
    {

        //    if (col.gameObject.GetComponent(attacksWhat.ToString()) != null)
        //    {
        //        if (parent.transform.position.x < col.transform.position.x)
        //        {
        //            launchDirection = 1;
        //        }
        //        else
        //        {
        //            launchDirection = -1;
        //        }
        if (col.gameObject.CompareTag("Enemy"))
        {
        Debug.Log("OnTriggerEnter");
            
        col.gameObject.GetComponent(attacksWhat.ToString()).SendMessage("Hit");
        }
        //    }

    }
}
