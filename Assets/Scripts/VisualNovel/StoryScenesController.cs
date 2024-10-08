using System.Collections;
using TMPro;
using UnityEngine;

public class StoryScenesController : MonoBehaviour
{
    [SerializeField] private float _symbolsTypingSpeed;
    [SerializeField] private float _timeAfterSentence;
    [SerializeField] private TMP_Text _Text;
    [SerializeField] private TMP_Text _Speaker;
    private StoryScene _currentScene;

    private State _SentanceState;

    private int _sentenceIndex;

    // Public methods

    public void PlayScene(StoryScene scene)
    {
        StartCoroutine(PlaySceneCoroutine(scene));
    }

    // Private methods
    private IEnumerator PlaySceneCoroutine(StoryScene scene)
    {
        _currentScene = scene;
        _sentenceIndex = -1;

        while (true)
        {
            if (_SentanceState == State.Playing)
                continue;

            if (++_sentenceIndex == scene.Sentences.Count)
                yield break;

            _Speaker.text = _currentScene.Sentences[_sentenceIndex].Speaker;
            TypeText(_currentScene.Sentences[_sentenceIndex].Text);
            yield return null;
        }
    }

    private IEnumerator TypeText(string text)
    {
        Debug.Log("Typing...");

        _SentanceState = State.Playing;
        _Text.text = "";
        int wordIndex = 0;

        while (_SentanceState != State.Completed)
        {
            _Text.text += text[wordIndex];
            yield return new WaitForSeconds(_symbolsTypingSpeed);
            if (++wordIndex == text.Length)
            {
                _SentanceState = State.Completed;
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
