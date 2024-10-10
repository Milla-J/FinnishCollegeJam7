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
    private bool _isTutorialSkipped;

    private void Awake()
    {
        _toolsOutline.SetActive(false);
        _conveyerOutline.SetActive(false);
        _isTutorialSkipped = false;
    }

    private void Update()
    {
        if (_isTutorialSkipped)
        {
            return;
        }
        if (!_waitForInputToSkip)
            return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            _visualNovelController.StopPlayingScene();
            HandleTutorialEnd();
            _isTutorialSkipped = true;
        }
    }

    public void PlayTutorial()
    {
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
        _tutorialVisuals.SetActive(false);
        _skipTutorialText.SetActive(false);

        _skipTutorialText.SetActive(false);

        OnTutorialEnded?.Invoke();
        _tutorialVisuals.SetActive(false);
    }
    private void HandleCompletedScentence(int scentenceCount)
    {
        if (scentenceCount == 2)
        {
            // Show where are the tools
            Debug.Log("scentenceCount 3 and making things");
            _toolsOutline.gameObject.SetActive(true);
            _toolsOutline.transform.position = _toolsOutlineSnap.position;
        }

        else if (scentenceCount == 5)
        {
            // Show where is the conveyer
            Debug.Log("scentenceCount 6 and making things");
            _conveyerOutline.gameObject.SetActive(true);
            _conveyerOutline.transform.position = _conveyerOutlineSnap.position;
        }

        else if(_toolsOutline.activeSelf == true)
        {
            _toolsOutline.gameObject.SetActive(false);
        }

        else if (_conveyerOutline.activeSelf == true)
        {
            _conveyerOutline.gameObject.SetActive(false);
        }
    }
}
