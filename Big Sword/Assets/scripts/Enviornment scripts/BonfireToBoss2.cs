using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BonfireToBoss2 : MonoBehaviour
{

    void Update()
    {

    }


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("active");
        SceneManager.LoadScene("Boss2Scene");
    }

}
