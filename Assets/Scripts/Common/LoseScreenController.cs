using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseScreenController : MonoBehaviour
{
    [SerializeField] private GameObject _loseScreen;
    private void Awake()
    {
        _loseScreen.SetActive(false);
    }
    public void ShowLoseScreen()
    {
        _loseScreen.SetActive(true);
        Time.timeScale = 0f;
    }
}
