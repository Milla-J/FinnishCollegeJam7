using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private LoseScreenController _loseScreenController;
    [SerializeField] private GameplayLoopController _gameplayLoopController;
    [SerializeField] private TutorialController _tutorialController;
    [SerializeField] private MenuManager _menuController;
    [SerializeField] private GameObject _pauseMenuPanel;

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
        Time.timeScale = 1;

        slider.maxValue = _patienceSize;
        slider.value = _patienceSize;
        satisfaction = slider.value;

        _tutorialController.PlayTutorial();
        _tutorialController.OnTutorialEnded += () => _gameplayLoopController.StartGame();
    }

    void Update()
    {
        if (satisfaction <= 0)
        {
            _loseScreenController.ShowLoseScreen();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _pauseMenuPanel.SetActive(true);
            _menuController.PauseGame();
        }
    }

    public void StopLowerSatisfaction()
    {
        //Debug.Log("Stopping coroutine");
        stopSatisfactionLowering = true;
    }

    public void UnlockLowerSatisfaction()
    {
        stopSatisfactionLowering = false;
    }

    public IEnumerator LowerSatisfaction()
    {
        while (!stopSatisfactionLowering)
        {
            if (satisfaction > slider.minValue)
            {
                satisfaction -= _descreasingStrenght;
                slider.value = satisfaction;
            }
            yield return new WaitForSeconds(_delay);
        }

        stopSatisfactionLowering = false;
    }
}
