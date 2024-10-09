using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject _pauseMenu;

    private bool _isMenuShowing = false;

    private void Update()
    {
        
    }

    public void Pause()
    {
        if (!_isMenuShowing)
        {
            ShowPauseMenu();
        } else
        {
            HidePauseMenu();
        }
    }

    public void ShowPauseMenu()
    {
        _isMenuShowing = true;
        Time.timeScale = 0f;
        _pauseMenu.SetActive(true);
    }

    public void HidePauseMenu()
    {
        _isMenuShowing = false;
        Time.timeScale = 1f;
        _pauseMenu.SetActive(false);
    }
}
