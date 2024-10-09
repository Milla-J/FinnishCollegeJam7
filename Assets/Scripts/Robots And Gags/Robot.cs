using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Robot : MonoBehaviour
{
    public bool InUse;
    public event Action<Robot> OnExitConveyer;

    [SerializeField] private GameManager gameManager; //reference to the game manager
    [SerializeField] private LabyrinthAssigner assigner;

    //Move parameters
    public float speed = 5f;
    public GameObject start;
    public GameObject end;

    private Vector3 startPosition;
    private Vector3 endPosition;

    private Labyrinth _labyrinth;
    private bool _isLabyrinthAssigned = false;


    private void Awake()
    {
        gameObject.SetActive(false);  //Making sure object in inactive
    }

    void OnMouseDown()
    {
        // This will be called when the square is clicked
        Debug.Log("Square clicked!");
        if (!_isLabyrinthAssigned)
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
            StopAllCoroutines();
            _labyrinth.RemoveFromUse();
            _isLabyrinthAssigned = false;
            OnExitConveyer?.Invoke(this);

            gameObject.SetActive(false);
        }
    }

    public void StartConveyerWay()
    {
        gameObject.SetActive(true);

        startPosition = start.transform.position;
        endPosition = end.transform.position;
        Debug.Log("positions are: " + startPosition + " and " + endPosition);

        StartCoroutine(Move());
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
