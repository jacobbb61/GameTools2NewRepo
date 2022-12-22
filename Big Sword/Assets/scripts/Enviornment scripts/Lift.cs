using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lift : MonoBehaviour
{
    public bool move = false;
    public bool bottom = true;

    public float speed;

    public Transform upPos;
    public Transform bottomPos;
    public Transform targetPosition;
    AudioSource Source;
    private GameObject Player;

    private void Start()
    {
        Source = GetComponent<AudioSource>();
        Player = GameObject.FindGameObjectWithTag("Player");
        bottom = true;
    }
    void Update()
    {
        if (move == true) 
        { 
            transform.position = Vector3.MoveTowards(transform.position, targetPosition.position, speed * Time.deltaTime);
           // if (transform.position == upPos.position) { Source.enabled = false;} else {  Source.enabled = true; } 
        } 
        if ((bottom == true) && (transform.position == upPos.position)) { move = false; }
       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (bottom == true) { move = true; targetPosition = upPos; }
        if (bottom == false) { move = true; targetPosition = bottomPos; }
        Player.transform.parent = transform.parent;
    }
    private void OnTriggerExit(Collider other)
    {
        Player.transform.parent = null;
    }
}
