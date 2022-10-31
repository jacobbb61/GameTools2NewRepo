using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BonfireToBoss1 : MonoBehaviour
{
    
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("active");
        SceneManager.LoadScene("Boss1Scene");
    }
}
