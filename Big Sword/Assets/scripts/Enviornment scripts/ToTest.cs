using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ToTest : MonoBehaviour
{
    public GameObject UI, Boss1Killed, Boss2Killed, GameMem;
    public bool inside;


    public void Awake()
    {

    }

    private void Update()
    {        
        GameMem = GameObject.FindGameObjectWithTag("Memory");
        if (GameMem.GetComponent<GameMem>().Boss1Killed) { Boss1Killed.SetActive(true); }
        if (GameMem.GetComponent<GameMem>().Boss2Killed) { Boss2Killed.SetActive(true); }
        if (inside == true && Input.GetKeyDown(KeyCode.JoystickButton3))
        {
            SceneManager.LoadScene("BOSS3");
        }
    }


    public void OnTriggerEnter(Collider other)
    {
        UI.SetActive(true);
        inside = true;
    }
    public void OnTriggerExit(Collider other)
    {
        UI.SetActive(false);
        inside = false;
    }
}