using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class InstrumentCollisionDetection : MonoBehaviour
{
    public GameManager gameManager;

    private List<Collider2D> collidersInTrigger = new List<Collider2D>();

    void OnTriggerEnter2D(Collider2D other)
    {
        //check if instrument touches wall
        if (other.tag == "Wall")
        {
            if (collidersInTrigger.Count == 0)
            {
                //Debug.Log("Instrument touched wall");
                gameManager.StartCoroutine(gameManager.LowerSatisfaction());
            }

            if (!collidersInTrigger.Contains(other))
            {
                collidersInTrigger.Add(other);
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Wall")
        {
            if (collidersInTrigger.Contains(other))
            {
                collidersInTrigger.Remove(other);
            }

            if (collidersInTrigger.Count <= 0)
            {
                //Debug.Log("No longer touching wall");
                gameManager.StopLowerSatisfaction();
            }
        }
    }
}
