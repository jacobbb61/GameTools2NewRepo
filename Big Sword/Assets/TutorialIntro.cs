using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialIntro : MonoBehaviour
{
    public GameObject CamIntro, MainCam, Player;
    public float Timer;
    void Start()
    {
        CamIntro.SetActive(true);
        MainCam.SetActive(false);
        Player.GetComponentInChildren<Animator>().SetTrigger("Intro");
        Player.GetComponentInChildren<PlayerCombat>().enabled=false;
        Player.GetComponent<PlayerMove>().enabled=false;
    }

    // Update is called once per frame
    void Update()
    {
        Timer += Time.deltaTime;
        if (Timer >= 2.5f)
        {
            CamIntro.SetActive(false);
            MainCam.SetActive(true);
            Player.GetComponentInChildren<PlayerCombat>().enabled = true;
            Player.GetComponent<PlayerMove>().enabled = true;
            Destroy(this);
        }
    }
}
