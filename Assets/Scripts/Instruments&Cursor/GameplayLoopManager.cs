using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayLoopManager : MonoBehaviour
{
    public static GameplayLoopManager Instance;

    [Header("Game Difficulty Flow parameters")]
    [SerializeField] private float _EntitiesPassedBeforeNormal;
    [SerializeField] private float _EntitiesPassedBeforeHard;

    [Header("Difficulty parameters")]
    [SerializeField] private float _EasySpeed;
    [SerializeField] private float _NormalSpeed;
    [SerializeField] private float _HardSpeed;

    [SerializeField] private float _EasySpawnRate; //seconds between spawns
    [SerializeField] private float _NormalSpawnRate;
    [SerializeField] private float _HardSpawnRate;
    private float currentSpawnRate;

    [SerializeField] private float _GagsSpawnChance;

    [Header("Pullers")]
    [SerializeField] private RobotsPuller _RobotsPuller;
    [SerializeField] private GagsPuller _GagsPuller;

    bool gameStarted = false;

    //Timer and Timings
    private float timer;
    private bool passedEasyMode = false;
    private bool passedNormalMode = false;

    ///Getters
    public float Speed { get; private set; } //Speed ythat is being shared 


    public void StartGame()
    {
        gameStarted = true;
        SwitchToEasyMode();
    }

    private void Update()
    {
        //if (!_gameStarted)
        //    return;

        timer += Time.deltaTime;
        Debug.Log(timer);

        //if( && !passedEasyMode)
    }

    private void SpawnEntity()
    {
        //Randomly dependinh on the chance spawn robot or gag
    }
    private void SwitchToEasyMode()
    {
        Speed = _EasySpeed;
        currentSpawnRate = _EasySpawnRate;
    }
}
