using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class KnightBossCombat : MonoBehaviour
{
    GameObject player;
    Animator anim;

    public GameObject HPBar, ReturnObj;

    private NavMeshAgent navMeshAgent;

    public int HP;

    public float Atk1Range, Atk2Range, Atk3Range;
    public float AtkT, Atk3WaitT;


    public bool dead = false;
    public bool attack3 = false;
    public bool PhaseChange = false;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponentInChildren<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (HP <= 0) { dead = true; }
        if (HP <= 20 && PhaseChange == false) { Phase(); PhaseChange = true; }
        if (dead == true && AtkT < 20f) { Killed(); AtkT = 200f; }
        HPBar.transform.localScale = new Vector3(0.5f, 0.1f, HP);

        if (AtkT > 0f) { AtkT -= Time.deltaTime;  } else { navMeshAgent.speed = 3f; navMeshAgent.angularSpeed = 360f; attack3 = false; }
        if (AtkT > 1f && AtkT<1.5f && attack3==true) { navMeshAgent.angularSpeed = 6000f; } 
        if (AtkT > 0.9f && AtkT<1.2f && attack3==true) { navMeshAgent.speed = 500f; navMeshAgent.stoppingDistance = 5f; } 
        if (AtkT < 0.9f && attack3==true) { navMeshAgent.speed = 0f; navMeshAgent.angularSpeed = 0f; navMeshAgent.stoppingDistance = 1f; }


        if (Vector3.Distance(transform.position, player.transform.position) < Atk1Range) { Atk3WaitT += Time.deltaTime; }

        if ((AtkT <= 0f) && (Vector3.Distance(transform.position, player.transform.position) > Atk3Range)) { Attack3(); }

        if ((AtkT <= 0f) && (Vector3.Distance(transform.position, player.transform.position) < Atk1Range)) { Attack2(); }

        if ((PhaseChange == false) && (AtkT <= 0f) && (Vector3.Distance(transform.position, player.transform.position) > Atk1Range && (Vector3.Distance(transform.position, player.transform.position) < Atk2Range))) { Attack1(); }
        if ((PhaseChange==true)&&(AtkT <= 0f) && (Vector3.Distance(transform.position, player.transform.position) > Atk1Range && (Vector3.Distance(transform.position, player.transform.position) < Atk2Range))) { Attack1P2(); }

    }


    public void Attack1()
    {
        AtkT = 4f;
        navMeshAgent.speed = 4f; navMeshAgent.angularSpeed = 360f;
        anim.SetTrigger("Attack1");
        attack3 = false;
    }

    public void Attack1P2()
    {
        AtkT = 1.5f;
        navMeshAgent.speed = 6f; navMeshAgent.angularSpeed = 360f;
        anim.SetTrigger("Attack1");
        attack3 = false;
    }
    public void Attack2()
    {
        AtkT = 1.5f;
        navMeshAgent.speed = 0f; navMeshAgent.angularSpeed = 100f;
        anim.SetTrigger("Attack2");
        attack3 = false;
    }
    public void Attack3()
    {
        attack3 = true;
        AtkT = 3f;
        Atk3WaitT = 0f;
        navMeshAgent.speed = 0f; navMeshAgent.angularSpeed = 0f;
        anim.SetTrigger("Attack3");
    }
    public void Phase()
    {
        navMeshAgent.speed = 0f; navMeshAgent.angularSpeed = 0f;
        AtkT = 3.5f;
        anim.SetBool("Phase2", true);
        anim.SetTrigger("PhaseChange");
    }
    void Killed()
    {
        HPBar.SetActive(false);
        GetComponent<KnightBossMove>().enabled = false;
        navMeshAgent.enabled = false;
        GetComponentInChildren<EnemyDetect>().enabled = false;
        anim.SetTrigger("Dead");
        HP = 2;
        Destroy(this.gameObject, 3f);
        ReturnObj.SetActive(true);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("playerattack")) { HP--; }
    }
}