using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorBehaviour : MonoBehaviour
{
    public Instrument _CurrentInstrument { get; private set; }


    //Cursor public methods
    public void HandleInstrumentChoose(Instrument instrument)
    {
        if(_CurrentInstrument != null && _CurrentInstrument != instrument)
        {
            DropCurrentInstrument();
            PickUpNewInstrument(instrument);
        }
        else if(_CurrentInstrument == instrument)
        {
            DropCurrentInstrument();
        }
        else // If no instrument in hand
        {
            PickUpNewInstrument(instrument); 
        }
    }

    //Cursor private methods
    private void PickUpNewInstrument(Instrument instrument)
    {
        _CurrentInstrument = instrument;
        _CurrentInstrument.Activate();
        Debug.Log("Took instrument");
        Cursor.visible = false;

    }
    private void DropCurrentInstrument()
    {
        _CurrentInstrument.Deactivate();
        _CurrentInstrument = null;
        Debug.Log("Drop instrument");
        Cursor.visible = true;
    }
}
