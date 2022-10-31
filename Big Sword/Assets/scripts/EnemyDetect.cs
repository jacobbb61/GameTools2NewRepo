using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetect : MonoBehaviour
{
    public GameObject player ;
    public Vector3 LastHit, Lastseen, ReturnPos;
    public Vector3 collision = Vector3.zero;
    public float detect;
    public float ChaseT = 0f;
    public bool InRange = false;
    public bool seen = false;
    private void Start()
    {
        Lastseen = this.transform.position;
        ReturnPos = this.transform.position;
        player = GameObject.FindGameObjectWithTag("Player");
    }
    void Update()
    {


        if (Vector3.Distance(transform.position, player.transform.position) < detect)
        {
           InRange = true;

            transform.LookAt(player.transform.position);

            var ray = new Ray(this.transform.position, this.transform.TransformDirection(Vector3.forward));
            Physics.Raycast(ray, out RaycastHit hit, 40f);
            if (!hit.transform.CompareTag("Player")) { seen = false; detect = 15; } else { seen = true; Lastseen = player.transform.position; detect = 25; ChaseT = 0f; }


         

        }
        else { InRange = false; seen = false; }

        if (seen==false) { ChaseT += Time.deltaTime;}

        if (ChaseT >= 5f) { Lastseen = ReturnPos; }

    }
}
