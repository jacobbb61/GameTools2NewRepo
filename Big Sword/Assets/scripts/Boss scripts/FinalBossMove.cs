using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class FinalBossMove : MonoBehaviour
{
    Animator anim;
    public GameObject Player;
    public Vector3 LastSeen, Vel;
    public bool move;
    public float speed, detect;
    private NavMeshAgent navMeshAgent;
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();

    }


    void Update()
    {



        navMeshAgent.destination = Player.transform.position;

        Vel = navMeshAgent.velocity;
        if ((Mathf.Abs(Vel.x) <= 0.5f) && ((Mathf.Abs(Vel.z) <= 0.5f))) { anim.SetTrigger("Idle"); anim.ResetTrigger("Walk"); }
        else
        { anim.SetTrigger("Walk"); anim.ResetTrigger("Idle"); }


    }
}
