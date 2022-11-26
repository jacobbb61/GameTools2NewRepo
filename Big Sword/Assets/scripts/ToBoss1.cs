using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ToBoss1 : MonoBehaviour
{
    public GameObject UI;
    public bool inside;
    public Object BossScene;

    private void Update()
    {
        if (inside==true && Input.GetKeyDown(KeyCode.JoystickButton3))
        {
            SceneManager.LoadScene(BossScene.name);
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
