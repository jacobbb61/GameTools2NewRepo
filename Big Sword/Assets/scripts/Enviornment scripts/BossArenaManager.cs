using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossArenaManager : MonoBehaviour
{

    public KnightBossCombat BossCombat;
    public GameObject Colider;
    public GameObject HPbar;
    public GameObject BossName;
    
    private void OnTriggerEnter(Collider other)
    {
        BossCombat.enabled=true;
        Colider.SetActive(true);
        HPbar.SetActive(true);
        BossName.SetActive(true);
    }

}
