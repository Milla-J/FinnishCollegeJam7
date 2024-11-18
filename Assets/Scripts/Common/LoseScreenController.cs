using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoseScreenController : MonoBehaviour
{
    [SerializeField] private GameObject _loseScreen;
    [SerializeField] private RectTransform _satisfactionMeter;
    [SerializeField] private GameObject _realWorldSatisfactionMeter;
    private void Awake()
    {
        _loseScreen.SetActive(false);

        Vector2 meterWorldCoordinates = Camera.main.ScreenToWorldPoint(_satisfactionMeter.transform.position);  // Setting _realWorldSatisfactionMeter Position
        _realWorldSatisfactionMeter.transform.position = meterWorldCoordinates;

        //_realWorldSatisfactionMeter have to be properlly scaled beforehand

        //_satisfactionMeter.
        //Instantiate(_satisfactionMeterSubstitution, )
    }
    public void ShowLoseScreen()
    {
        //_loseScreen.SetActive(true);
        //Time.timeScale = 0f;
    }
}
