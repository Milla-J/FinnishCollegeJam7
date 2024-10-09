using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstrumentFix : Instrument
{
    public GameManager gameManager;
    public string targetTag;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == targetTag)
        {
            gameManager.AddToSatisfaction();
            gameManager.fixing = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == targetTag)
        {
            gameManager.fixing = false;
        }
    }
}
