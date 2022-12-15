using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    public bool pulled, inside;
    public GameObject Text;
    public GameObject MemoryObj;
    public GameObject Lift;
    public GameObject LiftToBoss;
    public GameObject LeverOn;


    private void Start()
    {
        MemoryObj = GameObject.FindGameObjectWithTag("Memory");
        if (MemoryObj.GetComponent<GameMem>().Boss1Lever == true) { LiftToBoss.SetActive(true); LeverOn.SetActive(true); }
    }


    void Update()
    {
        if (inside == true && Input.GetKeyDown(KeyCode.JoystickButton3))
        {
            MemoryObj.GetComponent<GameMem>().Boss1Lever = true;
            Lift.GetComponent<Lift>().bottom = false;
            if (MemoryObj.GetComponent<GameMem>().Boss1Lever == true) { LiftToBoss.SetActive(true); LeverOn.SetActive(true); }
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
