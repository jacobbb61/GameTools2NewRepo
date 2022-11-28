using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boss2ToBonfire : MonoBehaviour
{

    public GameObject player, cam;
    public float T;
    bool go = false;
    private void Update()
    {
        if (go==true)
        {
            T += Time.deltaTime;
        }
        if (T >= 10f) {  SceneManager.LoadScene("RespawnScene"); }
    }

    private void OnTriggerEnter(Collider other)
    {
        player.GetComponent<PlayerMove>().enabled = false;
        player.GetComponentInChildren<PlayerCombat>().enabled = false;
        player.GetComponentInChildren<Animator>().SetTrigger("Idle");
        cam.SetActive(true);
        cam.GetComponent<Animator>().enabled = true;
        go = true;
    }
}
