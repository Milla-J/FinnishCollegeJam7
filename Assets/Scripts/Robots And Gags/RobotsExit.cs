using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class RobotsExit : MonoBehaviour
{
    [SerializeField] private PopUpController popUpController;
    [Header("Score parameters")]
    [SerializeField] private float _scoreMultiplier;
    private int _robotsPassed;
    private int _targetRobotsAmount;
    private float _currentScore;

    public event Action<float> OnEndingConditionMet;

    public void SetEndingCondition(int robotsPassed)
    {
        _targetRobotsAmount = robotsPassed;
        _robotsPassed = 0;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Robot"))
        {
            if(++_robotsPassed == _targetRobotsAmount)
            {
                OnEndingConditionMet?.Invoke(CalculateScore());
            }
            Robot leftedRobot = collision.gameObject.GetComponent<Robot>();
            Debug.Log(_robotsPassed);
            //Add score
            _currentScore += leftedRobot.AssosiatedLabyrinth.FinalFactor;
            //
            popUpController.CloseIfMatches(leftedRobot.AssosiatedLabyrinth);
        }
    }

    private float CalculateScore()
    {
        Debug.Log(_currentScore + " " + (_currentScore / _targetRobotsAmount) * _scoreMultiplier);
        return(float)(Math.Round(_targetRobotsAmount / _currentScore) * _scoreMultiplier);
    }

}
