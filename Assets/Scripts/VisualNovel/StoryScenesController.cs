using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StoryScenesController : MonoBehaviour
{
    [SerializeField] private float _symbolsTypingSpeed;
    [SerializeField] private float _timeAfterSentence;
    [SerializeField] private TMP_Text _Text;
    [SerializeField] private TMP_Text _Speaker;
    private StoryScene _CurrentStory;

    private State _CurrentStoryState;
    private State _CurruntSentanceStage;

    private int _SentenceIndex;

    private void Awake()
    {
        _CurruntSentanceStage = State.Completed;
    }


    enum State
    {
        Playing,
        Completed
    }

    // Public methods


    public void PlayStoryScene(StoryScene storyScene)
    {
        StartCoroutine(PlayStoryCourutine(storyScene));
    }

    private IEnumerator PlayStoryCourutine(StoryScene storyScene)
    {
        _CurrentStory = storyScene;
        _SentenceIndex = -1;

        while (_CurrentStory.Sentences.Count > _SentenceIndex)
        {
            if (_CurruntSentanceStage == State.Playing)
                continue;

            ++_SentenceIndex;
            TypeText(_CurrentStory.Sentences[_SentenceIndex].Text);
            _Speaker.text = _CurrentStory.Sentences[_SentenceIndex].Speaker;
            yield return null;
        }
    }

    // Private methods
    private IEnumerator TypeText(string text)
    {
        _Text.text = "";
        _CurruntSentanceStage = State.Playing;
        
        for(int i = 0; i < text.Length; i++)
        {
            _Text.text += text[i];
            yield return new WaitForSeconds(_symbolsTypingSpeed);

            if(_CurruntSentanceStage == State.Completed)
                yield break;
        }

        yield return new WaitForSeconds(_timeAfterSentence);
        _CurruntSentanceStage = State.Completed;
    }

}
