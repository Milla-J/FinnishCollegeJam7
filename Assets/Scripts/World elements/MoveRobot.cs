using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRobot : MonoBehaviour
{
    public float speed = 5f;
    public GameObject start;
    public GameObject end;

    private Vector3 startPosition;
    private Vector3 endPosition;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = start.transform.position;
        endPosition = end.transform.position;
        Debug.Log("positions are: " + startPosition + " and " + endPosition);

        StartCoroutine(Move());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator Move()
    {
        transform.position = startPosition;

        while (transform.position != endPosition)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, endPosition, step);
            yield return null;
        }
    }
}
