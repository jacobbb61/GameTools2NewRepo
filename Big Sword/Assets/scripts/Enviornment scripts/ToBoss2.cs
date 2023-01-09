using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ToBoss2 : MonoBehaviour
{
    public GameObject youdied;
    public float time;
    public bool inside;


    private void Update()
    {
        if (inside == true)
        {
            time += Time.deltaTime;
            youdied.GetComponent<Animator>().SetTrigger("Exit");
        }

        if (time >= 1.1f)
        {
            SceneManager.LoadScene("BOSS2");
        }
    }


    public void OnTriggerEnter(Collider other)
    {
       // UI.SetActive(true);
        inside = true;
    }
    public void OnTriggerExit(Collider other)
    {
       // UI.SetActive(false);
        inside = false;
    }
}
