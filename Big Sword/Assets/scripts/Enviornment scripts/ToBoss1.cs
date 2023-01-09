using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ToBoss1 : MonoBehaviour
{
    public bool inside;
    public GameObject youdied;
    public float time;


    private void Update()
    {
        if (inside==true)
        {
            time += Time.deltaTime;
            youdied.GetComponent<Animator>().SetTrigger("Exit");
        }

        if (time >= 1.1f)
        {
         SceneManager.LoadScene("BOSS1");
        }
    }


    public void OnTriggerEnter(Collider other)
    {

        inside = true;
    }    
}
