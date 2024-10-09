using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoopTester : MonoBehaviour
{
    [SerializeField] private GameplayLoopController _GameplayLoopManager;
    // Start is called before the first frame update
    void Start()
    {
        _GameplayLoopManager.StartGame();
    }
}
