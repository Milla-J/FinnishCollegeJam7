using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Robot : MonoBehaviour
{
    [HideInInspector] public bool InUse;
    public event Action<Robot> OnExitConveyer;

    //Move parameters
    private Transform _Start;
    private Transform _End;

    private PopUpManager _PopUpManager;
    private GameplayLoopManager _GameplayLoopManager;
    private LabyrinthPuller _Assigner;

    private Labyrinth _AssosiatedLabyrinth;
    private bool _isLabyrinthAssigned = false;


   public void Instantiate(GameplayLoopManager gameplayLoopManager, LabyrinthPuller assigner, PopUpManager popUpManager, Transform startMovementPoint, Transform endMovementPoint)
   {
        _Assigner = assigner;
        _PopUpManager = popUpManager;
        _GameplayLoopManager = gameplayLoopManager;
        _Start = startMovementPoint;
        _End = endMovementPoint;
        gameObject.SetActive(false);
    }

    void OnMouseDown()
    {
        // This will be called when the square is clicked
        Debug.Log("Square clicked!");
        if (!_isLabyrinthAssigned)
        {
            Debug.LogWarning("WAIT WHERE IS THE LABYRINTH >:(");
        }
        _PopUpManager.HandleLabyrinthPopUp(_AssosiatedLabyrinth);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("RobotExit"))
        {
            StopAllCoroutines();
            _AssosiatedLabyrinth.RemoveFromUse();
            _isLabyrinthAssigned = false;
            OnExitConveyer?.Invoke(this);

            gameObject.SetActive(false);
            Debug.Log("RobotExit");
        }
    }

    public void StartConveyerWay()
    {
        gameObject.SetActive(true);

        _AssosiatedLabyrinth = _Assigner.GetAvailableLabyrinth();  //Assigning labyrinth
        _isLabyrinthAssigned = true;

        StartCoroutine(Move());
    }

    public IEnumerator Move()
    {
        transform.position = _Start.position;

        while (transform.position != _End.position)
        {
            float step = _GameplayLoopManager.ConveyerSpeed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, _End.position, step);
            yield return null;
        }
    }
}
