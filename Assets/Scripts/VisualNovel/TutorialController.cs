using TMPro;
using UnityEngine;
using System;

public class TutorialController : MonoBehaviour
{
    public event Action OnTutorialEnded;
    [SerializeField] private StoryScene _tutorialScene;
    //UI parameters
    [SerializeField] private GameObject _tutorialVisuals;
    [SerializeField] private TMP_Text _text;
    [SerializeField] private TMP_Text _speaker;

    [SerializeField] private VisualNovelController _visualNovelController;
    public void PlayTutorial()
    {
        _tutorialVisuals.SetActive(true);
        _visualNovelController.Initialize(_text, _speaker);
        _visualNovelController.PlayScene(_tutorialScene);
        _visualNovelController.OnSceneEnded += HandleTutorialEnd;
    }
    private void HandleTutorialEnd()
    {
        OnTutorialEnded?.Invoke();
        _tutorialVisuals.SetActive(false);
    }
}
