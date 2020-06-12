using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{

    //public enum State
    //{
    //    PATROL,
    //    CHASE,
    //}
    Animator anim;

    //public State state;
    //private bool alive;
    public GameObject Player;
    //public GameObject[] waypoints;
    //private int waypointInd;

    //public float sightDist = 10;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();

        //waypoints = GameObject.FindGameObjectsWithTag("Waypoint");
        //waypointInd = Random.Range(0, waypoints.Length);

        //state = EnemyAI.State.PATROL;

        //alive = true;

        //StartCoroutine("FSM");

    }
    //IEnumerator FSM()
    //{
    //    while (alive)
    //    {
    //        switch(state)
    //        {
    //            case State.PATROL:
    //                Patrol();
    //                break;
    //            case State.CHASE:
    //                Chase();
    //                break;


    //        }
    //    yield return null;

    //    }
    //}
    void Patrol()
    {

    }

    void Chase()
    {

    }

    public GameObject GetObject()
    {
        return Player;
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
