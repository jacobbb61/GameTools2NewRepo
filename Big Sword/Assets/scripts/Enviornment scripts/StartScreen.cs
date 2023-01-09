using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class StartScreen : MonoBehaviour
{
    public bool timer;
    public GameObject youdied;
    public float time;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.JoystickButton3)) 
        { 
            timer = true;
        }
        if (timer)
        {
            time += Time.deltaTime;
        }
        if (time >= 1.1f)
        {
            SceneManager.LoadScene("TutorialScene");
        }
    }
}
