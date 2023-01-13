using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class SewerBossCombat : MonoBehaviour
{
    GameObject player;
    Animator anim;
    AudioSource Source;
    Boss2ArenaManager AM;
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
        Source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (HP <= 0) { dead = true; }
        if (HP <= 30 && PhaseChange==false) { Phase(); PhaseChange = true; }
        if (dead == true && AtkT < 20f) { Killed(); AtkT = 200f; }
        HPBar.transform.localScale = new Vector3(0.5f, 0.1f, HP);


        // change sword colliders to be bigger

        

            if (AtkT > 0f) { AtkT -= Time.deltaTime; } 
            else 
            {  attack3 = false; }
            if (AtkT <= 0f && PhaseChange==false) { navMeshAgent.speed = 2f; navMeshAgent.angularSpeed = 360f; }
            if (AtkT <= 0f && PhaseChange==true) { navMeshAgent.speed = 4f; navMeshAgent.angularSpeed = 800f; }
            if (AtkT > 1f && AtkT < 1.5f && attack3 == true) { navMeshAgent.angularSpeed = 6000f; }
            if (AtkT > 0.9f && AtkT < 1.2f && attack3 == true) { navMeshAgent.speed = 500f; }
            if (AtkT < 0.9f && attack3 == true) { navMeshAgent.speed = 0f; navMeshAgent.angularSpeed = 0f; }


        if ((PhaseChange == false) && (AtkT <= 0f) && (Vector3.Distance(transform.position, player.transform.position) > Atk3Range)) { Attack3(3f); }
        if ((PhaseChange == true) && (AtkT <= 0f) && (Vector3.Distance(transform.position, player.transform.position) > Atk3Range)) { Attack3(2.5f); }

        if ((PhaseChange == false) && (AtkT <= 0f) && (Vector3.Distance(transform.position, player.transform.position) < Atk1Range)) { Attack2(Random.Range(1, 4)); }
        if ((PhaseChange == true) && (AtkT <= 0f) && (Vector3.Distance(transform.position, player.transform.position) < Atk1Range)) { Attack2(Random.Range(1, 4)); }

        if ((PhaseChange == false) && (AtkT <= 0f) && (Vector3.Distance(transform.position, player.transform.position) > Atk1Range + 2 && (Vector3.Distance(transform.position, player.transform.position) < Atk2Range + 2))) { Attack1(2f); }
        if ((PhaseChange == true) && (AtkT <= 0f) && (Vector3.Distance(transform.position, player.transform.position) > Atk1Range + 2 && (Vector3.Distance(transform.position, player.transform.position) < Atk2Range + 2))) { Attack1(1.25f); }

    }


    public void Attack1(float time)
    {
        AtkT = time;
        navMeshAgent.speed = 10f; navMeshAgent.angularSpeed = 360f;
        anim.SetTrigger("Attack1");
        attack3 = false;
    }
    public void Attack2(int num)
    {
        if (PhaseChange == false)
        {
            if (num == 1) { AtkT = 1.5f; anim.SetTrigger("Attack2"); navMeshAgent.speed = 1f; navMeshAgent.angularSpeed = 300f; }
            if (num == 2) { AtkT = 1.25f; anim.SetTrigger("Attack5"); navMeshAgent.speed = 1f; navMeshAgent.angularSpeed = 200f; }
            if (num == 3) { AtkT = 1f; anim.SetTrigger("Attack6"); navMeshAgent.speed = 1f; navMeshAgent.angularSpeed = 200f; }
        }
        else
        {
            if (num == 1) { AtkT = 1f; anim.SetTrigger("Attack2"); navMeshAgent.speed = 1f; navMeshAgent.angularSpeed = 500f; }
            if (num == 2) { AtkT = 1f; anim.SetTrigger("Attack5"); navMeshAgent.speed = 1f; navMeshAgent.angularSpeed = 500f; }
            if (num == 3) { AtkT = 1.25f; anim.SetTrigger("Attack6"); navMeshAgent.speed = 1f; navMeshAgent.angularSpeed = 500f; }
        }
        Debug.Log(num);
        attack3 = false;
    }
    public void Attack3(float time)
    {
        attack3 = true;
        AtkT = time;
        Atk3WaitT = 0f;
        navMeshAgent.speed = 0f; navMeshAgent.angularSpeed = 0f;
        anim.SetTrigger("Attack3");
    }
    public void Phase()
    {
        navMeshAgent.speed = 0f; navMeshAgent.angularSpeed = 0f;
        AtkT = 5f; 
        anim.SetBool("Phase2", true);
        anim.SetTrigger("PhaseChange");
    }
    void Killed()
    {
        AM.BossMusic.enabled = false;
        GameObject Mem = GameObject.FindGameObjectWithTag("Memory");
        Mem.GetComponent<GameMem>().Boss2Killed = true;
        HPBar.SetActive(false);
        GetComponent<SewerBossMove>().enabled = false;
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
