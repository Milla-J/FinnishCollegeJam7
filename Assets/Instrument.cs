using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instrument : MonoBehaviour
{
    [SerializeField] InstrumentsInfo _info;

    private Transform _InstrumentTransform;
    private bool _IsActive = false;
    private Vector3 _zAxis = new Vector3(0, 0, 1);


    private void Awake()
    {
        _InstrumentTransform = GetComponent<Transform>();
    }

    private void Update()
    {
        if (!_IsActive)
            return;

        FollowCursor();
        Rotate();
    }

    //Object methods
    public void Take()
    {
        _IsActive = true;
    }

    private void FollowCursor()
    {
        var mousePos = Input.mousePosition;
        var worldPos = new Vector2(Camera.main.ScreenToWorldPoint(mousePos).x, Camera.main.ScreenToWorldPoint(mousePos).y);
        _InstrumentTransform.position = worldPos;
    }

    private void Rotate()
    {
        var degrees = Input.GetAxis("Mouse ScrollWheel") * _info.RotationStrenght;
        _InstrumentTransform.Rotate(_zAxis, degrees);
    }
}
