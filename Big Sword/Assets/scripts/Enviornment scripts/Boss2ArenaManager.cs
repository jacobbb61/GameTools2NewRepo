using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2ArenaManager : MonoBehaviour
{
    public SewerBossCombat BossCombat;
    public GameObject bridge;
    public GameObject lever;
    public GameObject HPbar;
    public GameObject BossName;
    public AudioSource BossMusic;
    private void OnTriggerEnter(Collider other)
    {
        BossCombat.enabled = true;
        bridge.SetActive(false);
        lever.SetActive(false);
        HPbar.SetActive(true);
        BossName.SetActive(true);
        BossMusic.enabled = true;
    }

}
