using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InstrumentCollisionDetection : MonoBehaviour
{
    public GameManager gameManager;

    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the instrment has touched the wall
        if (other.tag == "Wall")
        {
            Debug.Log("Instrument touched wall");
            gameManager.StartCoroutine(gameManager.LowerSatisfactio());
        }
    }

    void OnTriggerExit(Collider other)
    {
        Debug.Log("No longer touching wall");
        gameManager.StopCoroutine(gameManager.LowerSatisfactio());
    }
}
