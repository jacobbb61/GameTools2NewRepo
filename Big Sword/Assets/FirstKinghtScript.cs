using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstKinghtScript : MonoBehaviour
{
    public GameObject Door;
   void Update()
    {
        if (GetComponent<KnightCombat>().dead) { Door.SetActive(false); }
    }
}
