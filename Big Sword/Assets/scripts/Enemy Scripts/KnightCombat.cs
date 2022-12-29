using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class KnightCombat : MonoBehaviour
{
    GameObject player;
    Animator anim;
    AudioSource Source;
    public AudioClip Hit;
    public float Atk1Range, Atk2Range;
    public float AtkT, Atk3WaitT;

    public GameObject HPBar;

    private NavMeshAgent navMeshAgent;

    public int HP;


    public bool dead = false;
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
        if (dead==true && AtkT<20) { Killed(); AtkT = 200; }
        HPBar.transform.localScale = new Vector3(0.5f, 0.1f, HP / 15f); 

        if (AtkT > 0f) { AtkT -= Time.deltaTime; } else { navMeshAgent.speed = 3f; navMeshAgent.angularSpeed = 360f; }
        if (AtkT < 1f)  { navMeshAgent.speed = 3f; navMeshAgent.angularSpeed = 360f; }

        if (Vector3.Distance(transform.position, player.transform.position) < Atk2Range) { Atk3WaitT += Time.deltaTime; }
        if ((AtkT <= 0f) && (Atk3WaitT >= 4f) && (Vector3.Distance(transform.position, player.transform.position) < Atk2Range)) { Attack3(); }

        if ((AtkT <= 0f) && (Vector3.Distance(transform.position, player.transform.position) < Atk2Range)) { Attack2(); }

        if ((AtkT <= 0f) && (Vector3.Distance(transform.position, player.transform.position) > Atk2Range && (Vector3.Distance(transform.position, player.transform.position) < Atk1Range))) { Attack1(); }

    }

    public void Attack1()
    {
        AtkT = 2.5f;
        anim.SetTrigger("Attack1");
        navMeshAgent.speed = 0f; navMeshAgent.angularSpeed = 0f;
    }
    public void Attack2()
    {
        AtkT = 2f;
        anim.SetTrigger("Attack2");
    }
    public void Attack3()
    {
        AtkT = 4f;
        navMeshAgent.speed = 2f; navMeshAgent.angularSpeed = 3000f;
        anim.SetTrigger("Attack3");
    }

    void Killed()
    {
        HPBar.SetActive(false);
        GetComponent<KnightMove>().enabled = false;
        navMeshAgent.enabled = false;
        GetComponentInChildren<EnemyDetect>().enabled = false;
        anim.SetTrigger("Dead");
        HP = 2;
        Destroy(this.gameObject, 3f);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("playerattack"))
        {
            HP--;
            Source.clip = Hit;
            Source.Play();
        }
    }
}
