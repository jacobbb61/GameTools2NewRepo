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

    private GameObject Player;

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        bottom = true;
    }
    void Update()
    {
        if (move == true) { transform.position = Vector3.MoveTowards(transform.position, targetPosition.position, speed * Time.deltaTime); }
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
