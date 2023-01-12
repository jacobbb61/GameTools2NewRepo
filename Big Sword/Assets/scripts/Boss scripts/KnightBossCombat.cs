using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class KnightBossCombat : MonoBehaviour
{
    GameObject player;
    Animator anim;
    AudioSource Source;
    public GameObject HPBar, ReturnObj, musicP1, musicP2;

    private NavMeshAgent navMeshAgent;

    public int HP;

    public float Atk1Range, Atk2Range, Atk3Range;
    public float AtkT, Atk4WaitT;


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
 

    void Update()
    {
        if (HP <= 0) { dead = true; }
        if (HP <= 30 && PhaseChange == false) {
            Phase(); 
            PhaseChange = true; 
        } 
        if (HP>30) { 
            musicP1.SetActive(true);
            musicP2.SetActive(false);
        }
        if (dead == true && AtkT < 20f) { Killed(); AtkT = 200f; }
        HPBar.transform.localScale = new Vector3(0.5f, 0.1f, HP);

        if (AtkT > 0f) { AtkT -= Time.deltaTime;  } else { navMeshAgent.speed = 1.8f; navMeshAgent.angularSpeed = 360f;  }
        if (AtkT > 1f && AtkT<1.5f && attack3==true) { navMeshAgent.angularSpeed = 6000f; navMeshAgent.speed = 1f; } 
        if (AtkT > 0.9f && AtkT<1.2f && attack3==true) { navMeshAgent.speed = 500f; navMeshAgent.stoppingDistance = 5f; } 
        if (AtkT < 0.9f && attack3==true) { navMeshAgent.speed = 0f; navMeshAgent.angularSpeed = 0f; navMeshAgent.stoppingDistance = 1f; }


        if ((PhaseChange==true) && Vector3.Distance(transform.position, player.transform.position) < Atk1Range) { Atk4WaitT += Time.deltaTime; }

        if ((PhaseChange == false) && (AtkT <= 0f) && (Vector3.Distance(transform.position, player.transform.position) > Atk3Range)) { Attack3(3f); }
        if ((PhaseChange == true) && (AtkT <= 0f) && (Vector3.Distance(transform.position, player.transform.position) > Atk3Range)) { Attack3(2.25f); }

        if ((PhaseChange == true) && (AtkT <= 0f) && (Atk4WaitT>=5f) && (Vector3.Distance(transform.position, player.transform.position) < Atk1Range)) { Attack4(); }

        if ((PhaseChange == false) && (AtkT <= 0f) && (Vector3.Distance(transform.position, player.transform.position) < Atk1Range)) { Attack2(Random.Range(1,4)); }
        if ((PhaseChange == true) && (AtkT <= 0f) && (Vector3.Distance(transform.position, player.transform.position) < Atk1Range)) { Attack2(Random.Range(1,4)); }

        if ((PhaseChange == false) && (AtkT <= 0f) && (Vector3.Distance(transform.position, player.transform.position) > Atk1Range+2 && (Vector3.Distance(transform.position, player.transform.position) < Atk2Range+2))) { Attack1(3f); }
        if ((PhaseChange==true)&&(AtkT <= 0f) && (Vector3.Distance(transform.position, player.transform.position) > Atk1Range+2 && (Vector3.Distance(transform.position, player.transform.position) < Atk2Range+2))) { Attack1(2f); }

        
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
            if ( num == 1){ AtkT = 2.5f;anim.SetTrigger("Attack2");navMeshAgent.speed = 1f; navMeshAgent.angularSpeed = 300f; }
            if ( num == 2){ AtkT = 2f;anim.SetTrigger("Attack5");navMeshAgent.speed = 1f; navMeshAgent.angularSpeed = 200f; }
            if ( num == 3){ AtkT = 2f;anim.SetTrigger("Attack6");navMeshAgent.speed = 1f; navMeshAgent.angularSpeed = 200f; }
        }
        else
        {
            if (num == 1) { AtkT = 1.9f; anim.SetTrigger("Attack2"); navMeshAgent.speed = 1f; navMeshAgent.angularSpeed = 300f; }
            if (num == 2) { AtkT = 1.2f; anim.SetTrigger("Attack5"); navMeshAgent.speed = 1f; navMeshAgent.angularSpeed = 300f; }
            if (num == 3) { AtkT = 1.2f; anim.SetTrigger("Attack6"); navMeshAgent.speed = 1f; navMeshAgent.angularSpeed = 300f; }
        }
        Debug.Log(num);
        attack3 = false;
    }  

    public void Attack3(float time)
    {
        attack3 = true;
        AtkT = time;
        navMeshAgent.speed = 0.5f; navMeshAgent.angularSpeed = 0.5f;
        anim.SetTrigger("Attack3");
    }
    public void Attack4()
    {
        AtkT = 2f;
        Atk4WaitT = 0f;
        navMeshAgent.speed = 0f; navMeshAgent.angularSpeed = 0f;
        anim.SetTrigger("Attack4");
    }
    public void Phase()
    {
        navMeshAgent.speed = 0f; navMeshAgent.angularSpeed = 0f;
        AtkT = 3.5f;
        anim.SetBool("Phase2", true);
        anim.SetTrigger("PhaseChange");
        musicP2.SetActive(true);
        musicP1.SetActive(false);
    }
    void Killed()
    {
        GameObject Mem = GameObject.FindGameObjectWithTag("Memory");
        Mem.GetComponent<GameMem>().Boss1Killed = true;
        HPBar.SetActive(false);
        GetComponent<KnightBossMove>().enabled = false;
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