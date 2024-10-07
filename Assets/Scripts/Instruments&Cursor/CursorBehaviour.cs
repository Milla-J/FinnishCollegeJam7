using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorBehaviour : MonoBehaviour
{
    public Instrument instrument;

    [SerializeField] private LayerMask _interactableLayer;


    private bool _holdingInstrument = false;

    private void Awake()
    {

    }

    private void Update()
    {
        if (_holdingInstrument)
            return;

        if(Input.GetMouseButtonDown(0)) //If mouse left clicked
        {
            Debug.Log(" left clicked");
            TryToDetectInstrument();
        }
    }

    //Cursor private methods
    /// <summary>
    /// Shooting raycast and trying to get an instrument
    /// </summary>
    private void TryToDetectInstrument()
    {
        var mousePos = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        ray.direction *= 100f; // making ray longer
        if (Physics.Raycast(ray, out RaycastHit hitInfo))
        {
            try
            {
                Debug.Log("GOT!");
                hitInfo.transform.GetComponent<Instrument>().Take();
            }
            catch
            {
                Debug.LogWarning("Can't get Instrument component from " + hitInfo.transform.name);
            }
        }
    }



    private void TakeInstrument(Instrument instrument)
    {
        Cursor.visible = false;
        instrument.Take();
        Debug.Log("Took instrument");
    }

}
