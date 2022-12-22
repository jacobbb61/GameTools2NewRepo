using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{

    public Animator anim;
    public GameObject ThisEnemy, ThisEnemyDetect, HPBar;
    public int HP;
    AudioSource Source;

    void Start()
    {
        Source = GetComponent<AudioSource>();
    }
    private void Update()
    {
        if (HP==0){ dead();}
        if (HP == 2) { HPBar.transform.localScale = new Vector3 (0.5f, 0.04f, 0.3f); }
        if (HP == 1) { HPBar.transform.localScale = new Vector3 (0.5f, 0.04f, 0.15f); }
    }
    void dead()
    {
            HPBar.SetActive(false);  
            ThisEnemy.GetComponent<EnemyMove>().enabled = false;
            ThisEnemy.GetComponent<EnemyMove>().navMeshAgent.enabled = false;
            ThisEnemyDetect.GetComponent<EnemyDetect>().enabled = false;           
            anim.SetTrigger("Dead");
            HP = 2;
            Destroy(ThisEnemy, 2f);  
    }


    public void OnTriggerEnter(Collider other)
    {      
        if (other.CompareTag("playerattack"))
        {
            HP--; 
            Source.Play(); 
        }  
    }

}
