using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualNovelTesting : MonoBehaviour
{
    public StoryScenesController scenesController;
    public StoryScene storyScene;
    void Start()
    {
        scenesController.PlayScene(storyScene);
    }
}
