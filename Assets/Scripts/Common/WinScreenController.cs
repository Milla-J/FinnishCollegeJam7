using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WinScreenController: MonoBehaviour
{
    [SerializeField] private GameObject _winScreen;
    [SerializeField] private TMP_Text _ComplimentText;
    [SerializeField] private TMP_Text _ScoreText;
    // To do: Implement cool animations

    private void Awake()
    {
        _winScreen.SetActive(false);
    }
    public void ShowWinScreen(string complimentText, string score)
    {
        _winScreen.SetActive(true);
        _ComplimentText.text = complimentText;
        _ScoreText.text = score;
        Time.timeScale = 0; //Stoping the game
    }
}
