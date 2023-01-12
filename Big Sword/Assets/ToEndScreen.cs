using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ToEndScreen : MonoBehaviour
{
    public GameObject UI;
    public GameObject Player;
    public bool inside;
    public bool timer;
    public GameObject youdied;
    public GameObject ending;
    public float time;

    private void Update()
    {
        if (inside == true && Input.GetKeyDown(KeyCode.JoystickButton3))
        {
            youdied.GetComponent<Animator>().SetTrigger("Exit");
            timer = true;
        }
        if (timer)
        {
            time += Time.deltaTime;
    
        }
        if (time >= 1.3f)
        {
            ending.SetActive(true);
            Player.SetActive(false);
            UI.SetActive(false);
            youdied.GetComponent<Animator>().ResetTrigger("Exit");
        }
        if (time >= 9f)
        {
            youdied.GetComponent<Animator>().SetTrigger("Exit");

        }
        if (time >= 11)
        {
            SceneManager.LoadScene("EndScreen");
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
