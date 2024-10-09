using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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

    [SerializeField] private float _GagsSpawnChance;


    [Header("Pullers")]
    [SerializeField] private RobotsPuller _RobotsPuller;
    [SerializeField] private GagsPuller _GagsPuller;

    private float currentSpawnRate;
    //Timer and Timings
    private float timer;

    private bool gameStarted = false;
    private bool passedEasyMode = false;
    private bool passedNormalMode = false;

    private List<Robot> robotsOnScene;
    private Action<int> OnRobotsCountUpdated;

    ///Getters
    public float Speed { get; private set; } //Speed ythat is being shared 


    public void StartGame()
    {
        Speed = _EasySpeed;
        currentSpawnRate = _EasySpawnRate;

        robotsOnScene = new();
        OnRobotsCountUpdated += TryToSwitchMode;
        gameStarted = true;
    }

    private void Update()
    {
        //if (!_gameStarted)
        //    return;

        timer += Time.deltaTime;
        Debug.Log(timer);

        //if( && !passedEasyMode)
    }
    IEnumerator SpawnEntity
    private void SpawnRobot() //To-DO: remake to SpawnEntity()
    {
        Robot newRobot = _RobotsPuller.PullRobot();
        robotsOnScene.Add(newRobot);
        OnRobotsCountUpdated?.Invoke(robotsOnScene.Count);
        newRobot.StartConveyerWay();
    }

    private void TryToSwitchMode(int robotsCount)
    {
        if(robotsCount == _EntitiesPassedBeforeNormal && !passedEasyMode)
        {
            passedEasyMode = true;
            Speed = _NormalSpeed;
            currentSpawnRate = _NormalSpawnRate;
        }
    }
}
