using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class FinalBossCombat : MonoBehaviour
{
    GameObject player;
    Animator anim;
    AudioSource Source;
    public GameObject HPBar, ReturnObj, PlayerArmature, BossHPUI;

    private NavMeshAgent navMeshAgent;

    public int HP;

    public float Atk1Range, Atk2Range, Atk3Range;
    public float AtkT, Atk4WaitT;
    float T;

    public bool dead = false;
    public bool attack3 = false;
    public bool attackGrab = false;
    public bool PhaseChange = false;
    public bool Grab = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponentInChildren<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        Source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (HP <= 0) { dead = true; }
        if (HP <= 30 && PhaseChange == false) { Phase(); PhaseChange = true; }
        if (dead == true && AtkT < 20f) { Killed(); AtkT = 200f; }
        HPBar.transform.localScale = new Vector3(0.5f, 0.1f, HP);

        if (AtkT > 0f) { AtkT -= Time.deltaTime; } else { navMeshAgent.speed = 1.8f; navMeshAgent.angularSpeed = 360f; }


        if (AtkT > 1f && AtkT < 1.5f && attack3 == true) { navMeshAgent.angularSpeed = 6000f; navMeshAgent.speed = 1f; }
        if (AtkT > 0.9f && AtkT < 1f && attack3 == true) { navMeshAgent.speed = 500f; navMeshAgent.stoppingDistance = 5f; }
        if (AtkT < 0.9f && attack3 == true) { navMeshAgent.speed = 0f; navMeshAgent.angularSpeed = 0f; navMeshAgent.stoppingDistance = 1f; }


        if (AtkT < 0.5f && attackGrab == true) { navMeshAgent.speed = 0f; navMeshAgent.angularSpeed = 50f;  }
        if (AtkT > 0.5f && AtkT < 1f && attackGrab == true) { navMeshAgent.speed = 10f;  navMeshAgent.angularSpeed = 600f; }
        if (AtkT > 1f && AtkT < 2f && attackGrab == true) {  navMeshAgent.speed = 1f; }
        
       
        if (Grab == true)
        {
            BossHPUI.SetActive(false);
            AtkT = 1000f;          
            T += Time.deltaTime;
            if (T >= 1.5f && T <= 1.6f) { Dunk(); }
            if (T >= 2.5f)
            {
                PlayerArmature.SetActive(false);
            }
            if (T >= 5.5f) 
            { 
                player.GetComponentInChildren<PlayerCombat>().youDied = true; 
                player.GetComponentInChildren<PlayerCombat>().HP = 0f; 
            }
        }


        if ((PhaseChange == true) && Vector3.Distance(transform.position, player.transform.position) < Atk1Range) { Atk4WaitT += Time.deltaTime; }

        if ((PhaseChange == false) && (AtkT <= 0f) && (Vector3.Distance(transform.position, player.transform.position) > Atk3Range)) { Attack3(3f); }
        if ((PhaseChange == true) && (AtkT <= 0f) && (Vector3.Distance(transform.position, player.transform.position) > Atk3Range)) { Attack3(3f); }

        if ((PhaseChange == true) && (AtkT <= 0f) && (Atk4WaitT >= 5f) && (Vector3.Distance(transform.position, player.transform.position) < Atk1Range)) { AttackGrab(); }

        if ((PhaseChange == false) && (AtkT <= 0f) && (Vector3.Distance(transform.position, player.transform.position) < Atk1Range)) { Attack2(Random.Range(1, 4)); }
        if ((PhaseChange == true) && (AtkT <= 0f) && (Vector3.Distance(transform.position, player.transform.position) < Atk1Range)) { Attack2(Random.Range(1, 4)); }

        if ((PhaseChange == false) && (AtkT <= 0f) && (Vector3.Distance(transform.position, player.transform.position) > Atk1Range + 2 && (Vector3.Distance(transform.position, player.transform.position) < Atk2Range + 2))) { Attack1(3f); }
        if ((PhaseChange == true) && (AtkT <= 0f) && (Vector3.Distance(transform.position, player.transform.position) > Atk1Range + 2 && (Vector3.Distance(transform.position, player.transform.position) < Atk2Range + 2))) { Attack1(2.5f); }

    }


    public void Attack1(float time)
    {
        AtkT = time;
        navMeshAgent.speed = 2f; navMeshAgent.angularSpeed = 300f;
        anim.SetTrigger("Attack1");
        attack3 = false;
    }
    public void Attack2(int num)
    {
        if (PhaseChange == false)
        {
            if (num == 1) { AtkT = 2.5f; anim.SetTrigger("Attack2"); navMeshAgent.speed = 1f; navMeshAgent.angularSpeed = 300f; }
            if (num == 2) { AtkT = 2f; anim.SetTrigger("Attack5"); navMeshAgent.speed = 1f; navMeshAgent.angularSpeed = 200f; }
            if (num == 3) { AtkT = 2.5f; anim.SetTrigger("Attack6"); navMeshAgent.speed = 1f; navMeshAgent.angularSpeed = 200f; }
        }
        else
        {
            if (num == 1) { AtkT = 2.3f; anim.SetTrigger("Attack2"); navMeshAgent.speed = 1f; navMeshAgent.angularSpeed = 300f; }
            if (num == 2) { AtkT = 1.8f; anim.SetTrigger("Attack5"); navMeshAgent.speed = 1f; navMeshAgent.angularSpeed = 300f; }
            if (num == 3) { AtkT = 2f; anim.SetTrigger("Attack6"); navMeshAgent.speed = 1f; navMeshAgent.angularSpeed = 300f; }
        }
        attack3 = false;
    }

    public void Attack3(float time)
    {
        attack3 = true;
        AtkT = time;
        navMeshAgent.speed = 0.5f; navMeshAgent.angularSpeed = 0.5f;
        anim.SetTrigger("Attack3");
    }

    public void Phase()
    {
        navMeshAgent.speed = 0f; navMeshAgent.angularSpeed = 0f;
        AtkT = 3.5f;
        anim.SetBool("Phase2", true);
        anim.SetTrigger("PhaseChange");
    }
    public void AttackGrab()
    {
        attackGrab = true;
        AtkT = 2f;
        Atk4WaitT = 0f;
        anim.SetTrigger("Grab");
    }

    public void Dunk()
    {
    
        navMeshAgent.enabled = false;
        AtkT = 1000f;
        anim.SetTrigger("Dunk");
    }

    void Killed()
    {
        GameObject Mem = GameObject.FindGameObjectWithTag("Memory");
        Mem.GetComponent<GameMem>().Boss1Killed = true;
        HPBar.SetActive(false);
        GetComponent<FinalBossMove>().enabled = false;
        navMeshAgent.enabled = false;
        GetComponentInChildren<EnemyDetect>().enabled = false;
        anim.SetTrigger("Dead");
        HP = 2;
        ReturnObj.SetActive(true);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("playerattack"))
        {
            HP--;
            Source.Play();
        }
    }
}