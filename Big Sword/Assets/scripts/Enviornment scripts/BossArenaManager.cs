using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossArenaManager : MonoBehaviour
{

    public GameObject BossHPBar;
    public GameObject player;
    void Update()
    {
        if ((Vector3.Distance(transform.position, player.transform.position) < 20) && GetComponent<KnightCombat>().dead==false) { BossHPBar.SetActive(true); } else { BossHPBar.SetActive(false); }
    }
}
