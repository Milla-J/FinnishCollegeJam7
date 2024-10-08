using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Robot : MonoBehaviour
{
    [HideInInspector] public bool InUse;
    public event Action<Robot> OnExitConveyer;
    public Labyrinth AssosiatedLabyrinth { get; private set; }

    [Header("Robot Sprite")]
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite _regularSprite;
    [SerializeField] private Sprite _outlineSprite;


   //Move parameter
   private Transform _Start;
    private Transform _End;

    private GameplayLoopController _GameplayLoopController;
    private LabyrinthPool _LabyrinthController;
    private PopUpController _PopUpController;

    private bool _isLabyrinthAssigned = false;
    private bool _isOutlined = false;


   public void Instantiate(GameplayLoopController gameplayLoopController, LabyrinthPool labyrinthController, PopUpController popUpController, Transform startMovementPoint, Transform endMovementPoint)
   {
        _GameplayLoopController = gameplayLoopController;
        _LabyrinthController = labyrinthController;
        _PopUpController = popUpController;

        _Start = startMovementPoint;
        _End = endMovementPoint;

        spriteRenderer.sprite = _regularSprite;

        gameObject.SetActive(false);
    }

    void OnMouseDown()
    {
        // This will be called when the square is clicked
        //Debug.Log("Square clicked!");
        if (!_isLabyrinthAssigned)
        {
            Debug.LogWarning("WAIT WHERE IS THE LABYRINTH >:(");
        }
        _PopUpController.HandleLabyrinthPopUp(AssosiatedLabyrinth, this);
        AudioManager.instance.PlayAudio(SFXType.OpenPopUp);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("RobotExit"))
        {
            StopAllCoroutines();

            AssosiatedLabyrinth.RemoveFromUse(); //So the labyrinth would be set as available in the Pool
            _isLabyrinthAssigned = false;

            OnExitConveyer?.Invoke(this); //So it would be set as available in the Pool

            gameObject.SetActive(false);
            //Debug.Log("RobotExit");
        }
    }

    public void StartConveyerWay()
    {
        gameObject.SetActive(true);

        AssosiatedLabyrinth = _LabyrinthController.GetAvailableLabyrinth();  //Assigning labyrinth
        _isLabyrinthAssigned = true;

        StartCoroutine(Move());
    }

    public IEnumerator Move()
    {
        transform.position = _Start.position;

        while (transform.position != _End.position)
        {
            float step = _GameplayLoopController.ConveyerSpeed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, _End.position, step);
            yield return null;
        }
    }

    public void Outline() => spriteRenderer.sprite = _outlineSprite;
    public void RemoveOutline() => spriteRenderer.sprite = _regularSprite;

    //public void OutlineHandler()
    //{
    //    if (_isOutlined)
    //    {
    //        _isOutlined = false;
    //        spriteRenderer.sprite = _regularSprite;
    //    }
    //    else
    //    {
    //        _isOutlined = true;
    //        spriteRenderer.sprite = _outlineSprite;
    //    }
    //}
}
