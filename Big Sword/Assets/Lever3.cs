using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever3 : MonoBehaviour
{
    public bool pulled, inside;
    public GameObject Text;
    public GameObject MemoryObj;
    public GameObject LiftToBoss;


    private void Start()
    {
        MemoryObj = GameObject.FindGameObjectWithTag("Memory");
        if (MemoryObj.GetComponent<GameMem>().Boss2Lever == true) { LiftToBoss.SetActive(false); }
    }


    void Update()
    {
        if (inside == true && Input.GetKeyDown(KeyCode.JoystickButton3))
        {
            MemoryObj.GetComponent<GameMem>().Boss2Lever = true;
            if (MemoryObj.GetComponent<GameMem>().Boss2Lever == true) { LiftToBoss.SetActive(false); }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Text.SetActive(true);
        inside = true;
    }

    private void OnTriggerExit(Collider other)
    {
        Text.SetActive(false);
        inside = false;
    }
}


