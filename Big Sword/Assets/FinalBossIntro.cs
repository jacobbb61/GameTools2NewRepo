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
        Boss.GetComponent<KnightBossCombat>().enabled = false;
        Boss.GetComponent<KnightBossMove>().enabled = false;
    }
    void Update()
    {
        Timey += Time.deltaTime;
        if (Timey >= 10f)
        {
            Player.GetComponentInChildren<PlayerCombat>().enabled = true;
            Player.GetComponent<PlayerMove>().enabled = true;
            Boss.GetComponent<KnightBossCombat>().enabled = true;
            Boss.GetComponent<KnightBossMove>().enabled = true;
            gameObject.SetActive(false);
        }
    }
}
