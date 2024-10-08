using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Instrument : MonoBehaviour
{
    [SerializeField] protected InstrumentsInfo _Info;

    protected Transform _InstrumentTransform;
    protected Rigidbody2D _rb;
    protected bool _IsActive;
    protected Vector3 _zAxis = new Vector3(0, 0, 1);


    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _InstrumentTransform = GetComponent<Transform>();
        gameObject.SetActive(false);
        _IsActive = false;
    }
    protected void FixedUpdate()
    {
        if (!_IsActive)
            return;

        FollowCursor();
        Rotate();
    }

    //Object's public methods
    public void Activate()
    {
        _IsActive = true;
        gameObject.SetActive(true);
    }
    public void Deactivate()
    {
        _IsActive = false;
        gameObject.SetActive(false);
    }

    //Object's private methods
    private void FollowCursor()
    {
        var mousePos = Input.mousePosition;
        var worldPos = new Vector2(Camera.main.ScreenToWorldPoint(mousePos).x, Camera.main.ScreenToWorldPoint(mousePos).y);
        //var newPos = Vector2.Lerp(_InstrumentTransform.position, worldPos, Time.deltaTime);
        //_InstrumentTransform.position = newPos;
        _rb.MovePosition(worldPos);
    }

    private void Rotate()
    {
        var degrees = Input.GetAxis("Mouse ScrollWheel") * _Info.RotationStrenght;
        _InstrumentTransform.Rotate(_zAxis, degrees);
    }
}
