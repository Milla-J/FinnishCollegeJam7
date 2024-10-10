using TMPro;
using UnityEngine;
using System;

public class TutorialController : MonoBehaviour
{
    public event Action OnTutorialEnded;
    [SerializeField] private StoryScene _tutorialScene;

    [SerializeField] private VisualNovelController _visualNovelController;

    [Header("UI parameters")]
    [SerializeField] private GameObject _tutorialVisuals;
    [SerializeField] private TMP_Text _text;
    [SerializeField] private TMP_Text _speaker;
    [SerializeField] private GameObject _skipTutorialText;

    [Header("On-Scene Instructions")]
    [SerializeField] private GameObject _toolsOutline;
    [SerializeField] private Transform _toolsOutlineSnap;
    [SerializeField] private GameObject _conveyerOutline;
    [SerializeField] private Transform _conveyerOutlineSnap;

    private bool _waitForInputToSkip;

    private void Update()
    {
        if (!_waitForInputToSkip)
            return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            _visualNovelController.StopPlayingScene();
            HandleTutorialEnd();
        }
    }

    public void PlayTutorial()
    {
        _toolsOutline.SetActive(false);
        _conveyerOutline.SetActive(false);

        _tutorialVisuals.SetActive(true);
        _skipTutorialText.SetActive(true);

        _visualNovelController.Initialize(_text, _speaker);
        _visualNovelController.PlayScene(_tutorialScene);
        _visualNovelController.OnSceneEnded += HandleTutorialEnd;
        _visualNovelController.OnSentenceCompleted += HandleCompletedScentence;

        _waitForInputToSkip = true;
    }
    private void HandleTutorialEnd()
    {
        _toolsOutline.SetActive(false);
        _conveyerOutline.SetActive(false);
        _skipTutorialText.SetActive(false);

        OnTutorialEnded?.Invoke();
        _tutorialVisuals.SetActive(false);
    }
    private void HandleCompletedScentence(int scentenceCount)
    {
        if(scentenceCount == 3)
        {
            // Show where are the tools
            _toolsOutline.SetActive(true);
            _toolsOutline.transform.position = _toolsOutlineSnap.position;
        }

        if (scentenceCount == 6)
        {
            // Show where is the conveyer
            _conveyerOutline.SetActive(true);
            _conveyerOutline.transform.position = _conveyerOutlineSnap.position;
        }

        else if(_toolsOutline.activeSelf == true)
        {
            _toolsOutline.SetActive(false);
        }

        else if (_conveyerOutline.activeSelf == true)
        {
            _conveyerOutline.SetActive(false);
        }
    }
}
