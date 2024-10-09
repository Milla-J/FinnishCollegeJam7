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
    [SerializeField] private float _EasyConveyerSpeed;
    [SerializeField] private float _NormalConveyerSpeed;
    [SerializeField] private float _HardConveyerSpeed;
    [SerializeField] private float _EasySpawnRate; //seconds between spawns
    [SerializeField] private float _NormalSpawnRate;
    [SerializeField] private float _HardSpawnRate;

    [SerializeField] private float _GagsSpawnChance;


    [Header("Pullers")]
    [SerializeField] private RobotsPuller _RobotsPool;
    [SerializeField] private GagsPuller _GagsPool;

    private float currentSpawnRate;

    private bool gameStarted = false;
    private bool passedEasyMode = false;
    private bool passedNormalMode = false;

    private List<Robot> robotsOnScene;
    private Action<int> OnRobotsCountUpdated;

    ///Getters
    public float ConveyerSpeed { get; private set; } //Speed that is being shared 

    private void Awake()
    {
        Instance = this;
    }

    public void StartGame()
    {
        ConveyerSpeed = _EasyConveyerSpeed;
        currentSpawnRate = _EasySpawnRate;

        robotsOnScene = new();
        OnRobotsCountUpdated += TryToSwitchMode;
        gameStarted = true;

        StartCoroutine(SpawnEntitiesCoroutine());
    }

    IEnumerator SpawnEntitiesCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(currentSpawnRate);
            SpawnRobot();
            if(!gameStarted)
            {
                break;
            }
        }
    }
    private void SpawnRobot() //To-DO: remake to SpawnEntity()
    {
        Robot newRobot = _RobotsPool.GetRobot();
        robotsOnScene.Add(newRobot);
        OnRobotsCountUpdated?.Invoke(robotsOnScene.Count);
        newRobot.StartConveyerWay();
    }

    private void TryToSwitchMode(int robotsCount)
    {
        if(robotsCount == _EntitiesPassedBeforeNormal && !passedEasyMode)
        {
            passedEasyMode = true;
            ConveyerSpeed = _NormalConveyerSpeed;
            currentSpawnRate = _NormalSpawnRate;
        }
        else if(robotsCount == _EntitiesPassedBeforeHard && !passedNormalMode)
        {
            passedNormalMode = true;
            ConveyerSpeed = _HardConveyerSpeed;
            currentSpawnRate = _HardSpawnRate;
        }
    }
}
