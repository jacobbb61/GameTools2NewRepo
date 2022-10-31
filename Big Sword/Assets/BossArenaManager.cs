using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossArenaManager : MonoBehaviour
{

    public GameObject BossHPBar;
    void Update()
    {
        if (GetComponentInChildren<EnemyDetect>().InRange==true && GetComponent<KnightCombat>().dead==false) { BossHPBar.SetActive(true); } else { BossHPBar.SetActive(false); }
    }
}
