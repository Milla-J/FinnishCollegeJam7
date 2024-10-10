using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class VisualNovelController : MonoBehaviour
{
    public event Action OnSceneEnded;
    //Speed parameters
    [SerializeField] private float _symbolsSpawnRate;
    private WaitForSeconds _waitSymbols;
    [SerializeField] private float _timeAfterSentence;
    private WaitForSeconds _waitSentence;

    //UI parameters
    private TMP_Text _speachText;
    private TMP_Text _speakerText;

    private bool _IsPlayingScene;
    private StoryScene _currentScene;

    private State _SentanceState;
    private int _sentenceIndex;

    private Action OnSentenceCompleted;


    private void Start()
    {
        OnSentenceCompleted += NextSentence;
        _IsPlayingScene = false;

        _waitSymbols = new WaitForSeconds(_symbolsSpawnRate);
        _waitSentence = new WaitForSeconds(_timeAfterSentence);
    }

    public void Initialize(TMP_Text speachText, TMP_Text speakerText)
    {
        _speachText = speachText;
        _speakerText = speakerText;
    }

    public void PlayScene(StoryScene scene)
    {
        if(_IsPlayingScene == false)
        {
            _IsPlayingScene = true;
            _currentScene = scene;
            _sentenceIndex = -1;
            NextSentence();
        }
        else
        {
            StopAllCoroutines();
            _currentScene = scene;
            _sentenceIndex = -1;
            NextSentence();
        }
    }

    private void NextSentence()
    {
        if (_currentScene.Sentences.Count > ++_sentenceIndex)
        {
            StartCoroutine(TypeSentence(_currentScene.Sentences[_sentenceIndex].Text));
            _speakerText.text = _currentScene.Sentences[_sentenceIndex].Speaker;

        }
        else   // If no more sentences left - scene is done
        {
            _IsPlayingScene = false;
            OnSceneEnded?.Invoke();
        }
    }

    private IEnumerator TypeSentence(string sentence)
    {
        Debug.Log("Typing...");

        _SentanceState = State.Playing;
        _speachText.text = "";
        int wordIndex = 0;

        while (_SentanceState != State.Completed)
        {
            _speachText.text += sentence[wordIndex];
            yield return _waitSymbols;
            if (++wordIndex == sentence.Length)
            {
                _SentanceState = State.Completed;
                yield return _waitSentence;
                OnSentenceCompleted?.Invoke();
                break;
            }
        }
    }

    enum State
    {
        Playing,
        Completed
    }
}
