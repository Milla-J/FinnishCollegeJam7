using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstrumentHolder : MonoBehaviour
{
    [SerializeField] private CursorBehaviour _CursorBehaviour;
    [SerializeField] private Instrument _AssociatedInstrument;

    //Getters
    public Instrument AssociatedInstrument => _AssociatedInstrument; 



    //private void OnMouseDown()
    //{
    //    _CursorBehaviour.HandleInstrumentChoose(_AssociatedInstrument);
    //}
}
