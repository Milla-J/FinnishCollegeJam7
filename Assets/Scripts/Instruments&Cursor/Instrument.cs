using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Instrument : MonoBehaviour
{
    [SerializeField] protected InstrumentsInfo _Info;
    protected Camera _MainCamera;

    //Components
    protected Transform _InstrumentTransform;
    protected TargetJoint2D _targetJoint2D;

    protected bool _IsActive;
    protected Vector3 _zAxis = new Vector3(0, 0, 1);
    protected float _RotationT; // t for rotation lerp


    private void Awake()
    {
        _MainCamera = Camera.main;
        _InstrumentTransform = GetComponent<Transform>();
        _targetJoint2D = GetComponent<TargetJoint2D>();
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

    void OnJointBreak2D(Joint2D brokenJoint)
    {
        Debug.LogWarning("Joint2D is broke!!! Regenerating...");
        Destroy(_targetJoint2D);
        _targetJoint2D = gameObject.AddComponent<TargetJoint2D>();
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
        var worldPos = new Vector2(_MainCamera.ScreenToWorldPoint(mousePos).x, _MainCamera.ScreenToWorldPoint(mousePos).y);
        _targetJoint2D.target = worldPos;
        Debug.Log("Follow Cursor");
    }

    private void Rotate()
    {
        var degrees = Input.GetAxis("Mouse ScrollWheel") * _Info.RotationStrenght;
        Quaternion targetRotation = _InstrumentTransform.rotation * Quaternion.Euler(0, 0, degrees);

       _InstrumentTransform.Rotate(_zAxis, degrees);
    }
}
