using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualNovelTesting : MonoBehaviour
{
    public VisualNovelController scenesController;
    public StoryScene storyScene;

    private void Start()
    {
        scenesController.PlayScene(storyScene);
    }

}
