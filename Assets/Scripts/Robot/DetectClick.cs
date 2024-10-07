using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectClick : MonoBehaviour
{
    public GameManager gameManager; //reference to the game manager

    void OnMouseDown()
    {
        // This will be called when the square is clicked
        Debug.Log("Square clicked!");

        gameManager.OpenPopup();
    }
}
