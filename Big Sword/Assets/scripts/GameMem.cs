using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMem : MonoBehaviour
{
    
    public bool Boss1Lever;
    public bool Boss1Killed;
    public bool Boss2Lever;
    public bool Boss2Lever2;
    public bool Boss2Killed;

    void Start()
    {
        DontDestroyOnLoad(this);
    }

}
