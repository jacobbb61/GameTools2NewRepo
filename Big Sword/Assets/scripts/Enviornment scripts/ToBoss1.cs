using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ToBoss1 : MonoBehaviour
{
    public GameObject UI;
    public bool inside;


    private void Update()
    {
        if (inside==true && Input.GetKeyDown(KeyCode.JoystickButton3))
        {
            SceneManager.LoadScene("BOSS1");
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
