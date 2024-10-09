using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUp : MonoBehaviour
{
    public bool IsOpen {  get; private set; }
    private Animator _popUpFrameAnimator;

    private void Awake()
    {
        _popUpFrameAnimator = GetComponent<Animator>();
    }
    public void Open()
    {
        IsOpen = true;
        gameObject.SetActive(true);
        _popUpFrameAnimator.SetTrigger("Appear");
    }

    public void Close()
    {
        IsOpen = false;
        _popUpFrameAnimator.SetTrigger("FadeAway");
    }

    public void SetActiveFalse() => gameObject.SetActive(false);
}
