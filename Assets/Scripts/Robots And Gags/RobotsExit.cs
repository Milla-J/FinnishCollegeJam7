using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotsExit : MonoBehaviour
{
    [SerializeField] private PopUpController popUpController;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Robot"))
        {
            popUpController.CloseIfMatches(collision.gameObject.GetComponent<Robot>().AssosiatedLabyrinth);
        }
    }
}
