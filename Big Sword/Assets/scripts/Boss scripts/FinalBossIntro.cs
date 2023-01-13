using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBossIntro : MonoBehaviour
{
    public GameObject Player, Boss;
    public float Timey;
    private void Start()
    {
        Player.GetComponentInChildren<PlayerCombat>().enabled = false;
        Player.GetComponent<PlayerMove>().enabled = false;
        Boss.GetComponent<FinalBossCombat>().enabled = false;
        Boss.GetComponent<FinalBossMove>().enabled = false;
    }
    void Update()
    {
        Timey += Time.deltaTime;

        if (Timey >= 22f)
        {
            Player.GetComponentInChildren<PlayerCombat>().enabled = true;
            Player.GetComponent<PlayerMove>().enabled = true;
            Boss.GetComponent<FinalBossCombat>().enabled = true;
            Boss.GetComponent<FinalBossMove>().enabled = true;
            Destroy(gameObject);
        }
    }
}
