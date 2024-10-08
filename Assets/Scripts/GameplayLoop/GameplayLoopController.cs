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

public class GameplayLoopController : MonoBehaviour
{
    public DifficultyLevels DifficultyLevel { get; private set; }

    [Header("Ending The Game Conditions")]
    [SerializeField] private int _robotsInGame;

    [Header("Ending The Game Parameters")]
    [SerializeField] private string _BestCompliment;
    [SerializeField] private string _NormalCompliment;
    [SerializeField] private string _WorstCompliment;
    [SerializeField] private float _scoreMultiplier;

    [Header("Robots Exit, Win and Lose Controllers")]
    [SerializeField] private RobotsExit _robotsExit;
    [SerializeField] private WinScreenController _winScreenController;

    [Header("Pullers")]
    [SerializeField] private RobotsPool _RobotsPool;
    [SerializeField] private GagsPuller _GagsPool;


    [Header("Game Difficulty Flow parameters")]
    [SerializeField] private float _EntitiesPassedBeforeNormal;
    [SerializeField] private float _EntitiesPassedBeforeHard;

    [Space(5)]
    [Header("Difficulty parameters")]
    [Space(2)]
    [Header("Speed")]
    [SerializeField] private float _EasyConveyerSpeed;
    [SerializeField] private float _NormalConveyerSpeed;
    [SerializeField] private float _HardConveyerSpeed;
    [Header("Spawn Rate (seconds between spawns)")]
    [SerializeField] private float _EasySpawnRate; //seconds between spawns
    [SerializeField] private float _NormalSpawnRate;
    [SerializeField] private float _HardSpawnRate;
    [Header("Spawn Chance")]
    [SerializeField] private float _GagsSpawnChance;

    [SerializeField] private AnimationSpeedController _ConveyorBeltSpeedController;


    private float currentSpawnRate;

    private bool gameStarted = false;
    private bool IsThisFirstRun = false;
    private bool passedEasyMode = false;
    private bool passedNormalMode = false;

    ///Getters
    public float ConveyerSpeed { get; private set; } //Speed that is being shared 

    public void StartGame()
    {
        ConveyerSpeed = _EasyConveyerSpeed;
        _ConveyorBeltSpeedController.UpdateSpeed(ConveyerSpeed);
        currentSpawnRate = _EasySpawnRate;

        IsThisFirstRun = true;
        gameStarted = true;

        _RobotsPool.Initialize(_robotsInGame);
        _robotsExit.SetEndingCondition(_robotsInGame, _scoreMultiplier);
        _robotsExit.OnEndingConditionMet += EndGame;

        StartCoroutine(SpawnEntitiesCoroutine());
    }

    IEnumerator SpawnEntitiesCoroutine()
    {
        while (true)
        {
            if (IsThisFirstRun)
            {
                IsThisFirstRun = false;
                SpawnRobot();
                continue;
            }

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
        if(_RobotsPool.TryToGetRobot(out Robot newRobot))
        {
            TryToSwitchMode(_RobotsPool.PoolCount);
            newRobot.StartConveyerWay();
        }
        else
        {
            Debug.LogWarning("No robot available");
        }
    }

    private void TryToSwitchMode(int robotsCount)
    {
        if(_RobotsPool.PoolCount == _EntitiesPassedBeforeNormal && !passedEasyMode)
        {
            Debug.Log("Switched to Normal Mode");

            passedEasyMode = true;
            DifficultyLevel = DifficultyLevels.Normal;
            ConveyerSpeed = _NormalConveyerSpeed;
            currentSpawnRate = _NormalSpawnRate;
            _ConveyorBeltSpeedController.UpdateSpeed(ConveyerSpeed);
        }
        else if(_RobotsPool.PoolCount == _EntitiesPassedBeforeHard && !passedNormalMode)
        {
            Debug.Log("Switched to Hard Mode");

            passedNormalMode = true;
            DifficultyLevel = DifficultyLevels.Hard;
            ConveyerSpeed = _HardConveyerSpeed;
            currentSpawnRate = _HardSpawnRate;
            _ConveyorBeltSpeedController.UpdateSpeed(ConveyerSpeed);
        }
    }

    private void EndGame(float score) //Raw score is and
    {
        StopAllCoroutines();
        _winScreenController.ShowWinScreen(CalculateCompliment(score), score.ToString());
        Debug.Log("End game");
    
    }
    private string CalculateCompliment(float score)
    {
        float maxScore = _robotsInGame * _scoreMultiplier;
        float scorePercentage = (score / maxScore) * 100;         // Calculate the percentage of the achieved score

        // Determine the compliment based on the percentage range
        if (scorePercentage >= 90f)
        {
            return _BestCompliment;
        }
        else if (scorePercentage >= 50f)
        {
            return _NormalCompliment;
        }
        else
        {
            return _WorstCompliment;
        }
    }
}
