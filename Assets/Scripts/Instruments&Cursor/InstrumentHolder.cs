using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstrumentHolder : MonoBehaviour
{
    [SerializeField] private Instrument _AssociatedInstrument;
    [SerializeField] private CursorBehaviour _CursorBehaviour;
    private void OnMouseDown()
    {
        _CursorBehaviour.HandleInstrumentChoose(_AssociatedInstrument);
    }
}
