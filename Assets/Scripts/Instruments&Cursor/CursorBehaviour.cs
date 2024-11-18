using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorBehaviour : MonoBehaviour
{
    [SerializeField] private Camera _MainCamera;
    int interactableMask = 1 << 6;
    
    //Getters
    public Instrument _CurrentInstrument { get; private set; }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
            TryToDetectInstrumentHolder();
    }

    private void TryToDetectInstrumentHolder()
    {
        Ray ray = _MainCamera.ScreenPointToRay(Input.mousePosition);

        Debug.DrawRay(ray.origin, ray.direction * 100f, Color.red);

        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, 100f, interactableMask);

        if (hit)
        {
            try
            {
                Debug.Log("GOT!");
                try
                {
                    AudioManager.instance.PlayAudio(SFXType.PickUp);
                }
                catch(System.Exception ex)
                {
                    Debug.LogWarning(ex);
                }
                var instrument = hit.transform.GetComponent<InstrumentHolder>().AssociatedInstrument;
                HandleInstrumentChoose(instrument);
            }
            catch (System.Exception ex)
            {
                Debug.LogWarning("Can't get Instrument component from " + hit.transform.name + "     Exeption: " + ex);
            }
        }
    }

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
        //Cursor.visible = false;

    }
    private void DropCurrentInstrument()
    {
        _CurrentInstrument.Deactivate();
        _CurrentInstrument = null;
        Debug.Log("Drop instrument");
        //Cursor.visible = true;
    }
}
