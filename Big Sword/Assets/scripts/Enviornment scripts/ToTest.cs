using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ToTest : MonoBehaviour
{
    public GameObject UI, Boss1Killed, Boss2Killed, GameMem;
    public GameObject Boss1Alive, Boss2Alive;
    public bool inside;
    public GameObject youdied;
    public float time;

    public void Awake()
    {

    }

    private void Update()
    {        
        GameMem = GameObject.FindGameObjectWithTag("Memory");
        if (GameMem.GetComponent<GameMem>().Boss1Killed) { Boss1Killed.SetActive(true); Boss1Alive.SetActive(false); }
        if (GameMem.GetComponent<GameMem>().Boss2Killed) { Boss2Killed.SetActive(true); Boss2Alive.SetActive(false); }
        if (inside == true)
        {
            time += Time.deltaTime;
            youdied.GetComponent<Animator>().SetTrigger("Exit");
        }

        if (time >= 1.1f)
        {
            SceneManager.LoadScene("BOSS3");
        }
    }


    public void OnTriggerEnter(Collider other)
    {
  
        inside = true;
    }
    public void OnTriggerExit(Collider other)
    {
    
        inside = false;
    }
}