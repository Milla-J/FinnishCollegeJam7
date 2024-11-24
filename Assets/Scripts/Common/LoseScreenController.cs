using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoseScreenController : MonoBehaviour
{
    [SerializeField] private GameObject _loseScreen;
    [SerializeField] private RectTransform _satisfactionMeter;
    [SerializeField] private GameObject _satisfactionMeterBloom;

    [SerializeField] private float _bloomPeak;
    [SerializeField] private float _bloomLowest;
    private float _currentBloom;
    private float _targetBloom;

    private bool _playingLoseScreen;
    

    private void Awake()
    {
        _loseScreen.SetActive(false);

        Vector2 meterWorldCoordinates = Camera.main.ScreenToWorldPoint(_satisfactionMeter.transform.position);  // Setting _realWorldSatisfactionMeter Position
        _satisfactionMeterBloom.transform.position = meterWorldCoordinates;

        //_realWorldSatisfactionMeter have to be properlly scaled beforehand
    }

    private void Update()
    {
        if (!_playingLoseScreen)
            return;


    }

    public void ShowLoseScreen()
    {
        _loseScreen.SetActive(true);
        Time.timeScale = 0f;
    }
    
    private IEnumerator PlayLoseScene()
    {

        yield return new WaitForSeconds(5f);
    }
}
