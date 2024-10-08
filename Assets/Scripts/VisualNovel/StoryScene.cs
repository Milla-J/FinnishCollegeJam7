using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StoryScene", menuName = "ScriptableObjects/VisualNovel")]
[System.Serializable]
public class StoryScene : ScriptableObject
{
    public List<Sentence> Sentences;

    [System.Serializable]
    public class Sentence
    {
        public string Text;
        public string Speaker;
    }
}
