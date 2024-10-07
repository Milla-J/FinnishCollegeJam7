using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instrument : MonoBehaviour
{
    [SerializeField] private InstrumentsInfo _Info;

    private Transform _InstrumentTransform;
    private bool _IsActive;
    private Vector3 _zAxis = new Vector3(0, 0, 1);


    private void Awake()
    {
        _InstrumentTransform = GetComponent<Transform>();
        gameObject.SetActive(false);
        _IsActive = false;
    }

    private void Update()
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
        _InstrumentTransform.position = worldPos;
    }

    private void Rotate()
    {
        var degrees = Input.GetAxis("Mouse ScrollWheel") * _Info.RotationStrenght;
        _InstrumentTransform.Rotate(_zAxis, degrees);
    }
}
