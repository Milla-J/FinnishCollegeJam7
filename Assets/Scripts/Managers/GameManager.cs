using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private LoseScreenController _loseScreenController;
    [SerializeField] private GameplayLoopController _gameplayLoopController;

    [Header("Satisfaction changes parameters")]
    [SerializeField] private Slider slider; //Reference to the satisfaction meter

    [SerializeField] private float _patienceSize;
    [SerializeField] private float _delay = 0.5f;
    [SerializeField] private float _descreasingStrenght = 0.2f;
    private float satisfaction; //satisfaction level
    private bool stopSatisfactionLowering = false;

    //public bool fixing = false;

    void Start()
    {
        slider.maxValue = _patienceSize;
        slider.value = _patienceSize;
        Time.timeScale = 1;
        satisfaction = slider.value;
        _gameplayLoopController.StartGame();
    }

    void Update()
    {
        if (satisfaction <= 0)
        {
            _loseScreenController.ShowLoseScreen();
        }
    }

    public void StopLowerSatisfaction()
    {
        //Debug.Log("Stopping coroutine");
        stopSatisfactionLowering = true;
    }

    public IEnumerator LowerSatisfaction()
    {
        while (!stopSatisfactionLowering)
        {
            //if (!fixing && satisfaction > slider.minValue)
            //{
            //    satisfaction -= 0.02f;
            //    slider.value = satisfaction;
            //}
            if (satisfaction > slider.minValue)
            {
                satisfaction -= _descreasingStrenght;
                slider.value = satisfaction;
            }
            yield return new WaitForSeconds(_delay);
        }

        stopSatisfactionLowering = false;
    }

    //public void AddToSatisfaction()
    //{
    //    if (satisfaction < slider.maxValue)
    //    {
    //        satisfaction += 0.2f;
    //        slider.value = satisfaction;
    //    }
    //}
}
