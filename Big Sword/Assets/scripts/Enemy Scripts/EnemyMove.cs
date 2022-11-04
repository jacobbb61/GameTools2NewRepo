using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyMove : MonoBehaviour
{
    Animator anim;
    public Vector3 LastSeen, Vel;
    public bool move;
    public float speed, detect;
    public NavMeshAgent navMeshAgent;

    void Start()
    {
       anim = GetComponentInChildren<Animator>();
       navMeshAgent = GetComponent<NavMeshAgent>();
        
    }


    void Update()
    {
        
        LastSeen = GetComponentInChildren<EnemyDetect>().Lastseen;

        navMeshAgent.destination = LastSeen;  



        Vel = navMeshAgent.velocity;
        if ((Mathf.Abs(Vel.x) <= 0.5f) && ((Mathf.Abs(Vel.z) <= 0.5f))) { anim.SetTrigger("Idle"); anim.ResetTrigger("Walk"); } else
        {

            if (GetComponentInChildren<EnemyDetect>().seen) {anim.SetTrigger("Run"); anim.ResetTrigger("Walk"); navMeshAgent.speed = 4.5f; }
            else { anim.SetTrigger("Walk"); anim.ResetTrigger("Run"); navMeshAgent.speed = 2f; }

        }
        if (Vector3.Distance(transform.position, GetComponentInChildren<EnemyDetect>().player.transform.position) < 2)
        {
            anim.SetTrigger("Attack");
        }

    }
}
