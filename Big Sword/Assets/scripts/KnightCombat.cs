using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class KnightCombat : MonoBehaviour
{
    GameObject player;
    Animator anim;
    private NavMeshAgent navMeshAgent;
    public float AtkT;
    public int HP;
    public GameObject HPBar;
    public bool dead = false;
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
        if (dead==true && AtkT<20) { Killed(); AtkT = 200; }
        HPBar.transform.localScale = new Vector3(0.5f, 0.1f, HP / 15f); 

        if (AtkT > 0) { AtkT -= Time.deltaTime; navMeshAgent.speed = 0.1f; navMeshAgent.angularSpeed = 1000; } else { navMeshAgent.speed = 3f; navMeshAgent.angularSpeed = 360; }

        if ((AtkT<=0)&&(Vector3.Distance(transform.position, player.transform.position) < 4)) { Attack1(); }      
    }

    public void Attack1()
    {
        AtkT = 2f;
        anim.SetTrigger("Attack1");
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
        if (other.CompareTag("playerattack")) { HP--; }
    }
}
