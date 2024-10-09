using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotsExit : MonoBehaviour
{
    [SerializeField] private PopUpController popUpController;
    private int _robotsPassed;
    private int _endRobotsAmount;

    public event Action OnEndingConditionMet;

    public void SetEndingCondition(int robotsPassed)
    {
        _endRobotsAmount = robotsPassed;
        _robotsPassed = 0;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Robot"))
        {
            if(++_robotsPassed == _endRobotsAmount)
            {
                OnEndingConditionMet?.Invoke();
            }
            Debug.Log(_robotsPassed);
            popUpController.CloseIfMatches(collision.gameObject.GetComponent<Robot>().AssosiatedLabyrinth);
        }
    }
}
