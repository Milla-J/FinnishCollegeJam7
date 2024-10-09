using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum DifficultyLevels
{
    Easy,
    Normal,
    Hard
}

public class GameplayLoopManager : MonoBehaviour
{
    public DifficultyLevels DifficultyLevel { get; private set; }

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
    [SerializeField] private RobotsPuller _RobotsPuller;
    [SerializeField] private GagsPuller _GagsPool;

    private float currentSpawnRate;

    private bool gameStarted = false;
    private bool passedEasyMode = false;
    private bool passedNormalMode = false;

    private List<Robot> robotsOnScene;
    private Action<int> OnRobotsCountUpdated;

    ///Getters
    public float ConveyerSpeed { get; private set; } //Speed that is being shared 

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
        if(_RobotsPuller.TryToGetRobot(out Robot newRobot))
        {
            robotsOnScene.Add(newRobot);
            OnRobotsCountUpdated?.Invoke(robotsOnScene.Count);
            newRobot.StartConveyerWay();
        }
        else
        {
            Debug.LogWarning("No robot available");
        }
    }

    private void TryToSwitchMode(int robotsCount)
    {
        if(robotsCount == _EntitiesPassedBeforeNormal && !passedEasyMode)
        {
            DifficultyLevel = DifficultyLevels.Normal;

            passedEasyMode = true;
            ConveyerSpeed = _NormalConveyerSpeed;
            currentSpawnRate = _NormalSpawnRate;
        }
        else if(robotsCount == _EntitiesPassedBeforeHard && !passedNormalMode)
        {
            DifficultyLevel = DifficultyLevels.Hard;

            passedNormalMode = true;
            ConveyerSpeed = _HardConveyerSpeed;
            currentSpawnRate = _HardSpawnRate;
        }
    }
}
