using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boss1ToBonfire : MonoBehaviour
{

    void Update()
    {

    }


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("active");
        SceneManager.LoadScene("BonfireScene");
    }
}