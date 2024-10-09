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
            _AssosiatedLabyrinth = _Assigner.GetAvailableLabyrinth();
            _isLabyrinthAssigned = true;
        }
        _PopUpManager.HandleLabyrinthPopUp(_AssosiatedLabyrinth);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("RobotExit"))
        {
            Debug.Log("RobotExit");
            StopAllCoroutines();
            _AssosiatedLabyrinth.RemoveFromUse();
            _isLabyrinthAssigned = false;
            OnExitConveyer?.Invoke(this);

            gameObject.SetActive(false);
        }
    }

    public void StartConveyerWay()
    {
        gameObject.SetActive(true);
        Debug.Log("positions are: " + _Start.position + " and " + _End.position);

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
