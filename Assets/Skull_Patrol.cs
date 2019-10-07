using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skull_Patrol : StateMachineBehaviour
{
    GameObject NPC;
    GameObject[] waypoints;
    int randomSpot;
    float speed=1;
    float waitTime;
    float starWaitTime=3;


    //private void Awake()
    //{

    //}

    
    // OnStateEnter is called before OnStateEnter is called on any state inside this state machine
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        NPC = animator.gameObject;
        //randomSpot = 0;
        waypoints = GameObject.FindGameObjectsWithTag("WayPoint");
        randomSpot = Random.Range(0, waypoints.Length);
    }

    // OnStateUpdate is called before OnStateUpdate is called on any state inside this state machine
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log(randomSpot);
        if (waypoints.Length == 0) return;
        //Debug.Log(Vector2.Distance(waypoints[currentWP].transform.position, NPC.transform.position));
        if (Vector2.Distance(waypoints[randomSpot].transform.position,
                            NPC.transform.position)<3.0f)
        {
            if (waitTime<=0)
            {
                randomSpot = Random.Range(0, waypoints.Length);
                waitTime = starWaitTime;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }
        //Vector2 direction = (waypoints[currentWP].transform.position - NPC.transform.position);
        NPC.transform.position = Vector2.MoveTowards(NPC.transform.position, waypoints[randomSpot].transform.position, speed * Time.deltaTime);
        //Debug.Log();
    }

    // OnStateExit is called before OnStateExit is called on any state inside this state machine
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called before OnStateMove is called on any state inside this state machine
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateIK is called before OnStateIK is called on any state inside this state machine
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMachineEnter is called when entering a state machine via its Entry Node
    //override public void OnStateMachineEnter(Animator animator, int stateMachinePathHash)
    //{
    //    
    //}

    // OnStateMachineExit is called when exiting a state machine via its Exit Node
    //override public void OnStateMachineExit(Animator animator, int stateMachinePathHash)
    //{
    //    
    //}
}
