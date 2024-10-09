using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoopTester : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameplayLoopManager.Instance.StartGame();
    }
}
