using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ToRespawnScene : MonoBehaviour
{
    public GameObject UI;
    public bool inside;
    public bool timer;
    public GameObject youdied;
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
        if (time >= 1.1f)
        {
            SceneManager.LoadScene("RespawnScene");
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
