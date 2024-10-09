using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Slider slider; //Reference to the satisfaction meter
    private float satisfaction; //satisfaction level
    [SerializeField] private float delay = 0.5f;
    private bool stopSatisfactionLowering = false;

    public bool fixing = false;

    [SerializeField] private GameObject _loseScreen;
    [SerializeField] private GameplayLoopController _gameplayLoopController;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        satisfaction = slider.value;
        _gameplayLoopController.StartGame();
    }

    void Update()
    {
        if (satisfaction <= 0)
        {
            GameOver();
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
            if (!fixing && satisfaction > slider.minValue)
            {
                satisfaction -= 0.02f;
                slider.value = satisfaction;
            }
            yield return new WaitForSeconds(delay);
        }

        stopSatisfactionLowering = false;
    }

    public void AddToSatisfaction()
    {
        if (satisfaction < slider.maxValue)
        {
            satisfaction += 0.2f;
            slider.value = satisfaction;
        }
    }

    private void GameOver()
    {
        Time.timeScale = 0;
        _loseScreen.SetActive(true);
    }
}
