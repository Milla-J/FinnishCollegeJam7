using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectClick : MonoBehaviour
{
    [SerializeField] private GameManager gameManager; //reference to the game manager
    [SerializeField] private LabyrinthAssigner assigner;

    private Labyrinth _labyrinth;
    private bool _isLabyrinthAssigned = false;

    void OnMouseDown()
    {
        // This will be called when the square is clicked
        Debug.Log("Square clicked!");
        if(!_isLabyrinthAssigned)
        {
            _labyrinth = assigner.GetAvailableLabyrinth(DifficultyLevels.Easy);
            _isLabyrinthAssigned = true;
        }
        gameManager.OpenPopup(_labyrinth);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("RobotExit"))
        {
            Debug.Log("RobotExit");
            _labyrinth.RemoveFromUse();
            _isLabyrinthAssigned = false;
        }
    }
}
