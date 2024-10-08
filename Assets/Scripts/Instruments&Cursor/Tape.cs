using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tape : Instrument
{
    public GameManager gameManager;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Crack")
        {
            gameManager.AddToSatisfaction();
            gameManager.fixing = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Crack")
        {
            gameManager.fixing = false;
        }
    }
}