using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever3 : MonoBehaviour
{
    public bool pulled, inside;
    public GameObject Text;
    public GameObject Door;
    public GameObject MemoryObj;
    public GameObject LiftToBoss;
    public GameObject LeverOn;
    AudioSource Source;

    private void Start()
    {
        MemoryObj = GameObject.FindGameObjectWithTag("Memory");
        if (MemoryObj.GetComponent<GameMem>().Boss2Lever2 == true)
        { 
            LiftToBoss.GetComponent<Animator>().enabled = true; 
            LeverOn.SetActive(true);
            Door.GetComponent<Animator>().SetTrigger("Open");
        }
        Source = GetComponent<AudioSource>();
    }


    void Update()
    {
        if (inside == true && Input.GetKeyDown(KeyCode.JoystickButton3))
        {
            MemoryObj.GetComponent<GameMem>().Boss2Lever2 = true;
            Source.Play();
            if (MemoryObj.GetComponent<GameMem>().Boss2Lever2 == true) { LiftToBoss.GetComponent<Animator>().enabled = true; LeverOn.SetActive(true); }
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


