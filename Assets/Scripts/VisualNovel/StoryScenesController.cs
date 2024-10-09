using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class StoryScenesController : MonoBehaviour
{
    //Speed parameters
    [SerializeField] private float _symbolsTypingSpeed;
    [SerializeField] private float _timeAfterSentence;

    //UI parameters
    [SerializeField] private TMP_Text _Text;
    [SerializeField] private TMP_Text _Speaker;

    private bool _IsPlayingScene;
    private StoryScene _currentScene;

    private State _SentanceState;
    private int _sentenceIndex;

    private Action OnSentenceCompleted;


    private void Start()
    {
        OnSentenceCompleted += NextSentence;
        _IsPlayingScene = false;
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
            _Speaker.text = _currentScene.Sentences[_sentenceIndex].Speaker;

        }
        else   // If no more sentences left - scene is done
            _IsPlayingScene = false;
    }

    private IEnumerator TypeSentence(string sentence)
    {
        Debug.Log("Typing...");

        _SentanceState = State.Playing;
        _Text.text = "";
        int wordIndex = 0;

        while (_SentanceState != State.Completed)
        {
            _Text.text += sentence[wordIndex];
            yield return new WaitForSeconds(_symbolsTypingSpeed);
            if (++wordIndex == sentence.Length)
            {
                _SentanceState = State.Completed;
                yield return new WaitForSeconds(_timeAfterSentence);
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
