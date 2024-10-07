using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorBehaviour : MonoBehaviour
{
    public Instrument instrument;
    private void Awake()
    {
        TakeInstrument(instrument);
    }
    private void TakeInstrument(Instrument instrument)
    {
        Cursor.visible = false;
        instrument.Take();
    }
}
