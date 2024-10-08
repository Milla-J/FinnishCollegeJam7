using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brush : Instrument
{
    public GameManager gameManager;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Rust")
        {
            gameManager.AddToSatisfaction();
            gameManager.fixing = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Rust")
        {
            gameManager.fixing = false;
        }
    }
}
