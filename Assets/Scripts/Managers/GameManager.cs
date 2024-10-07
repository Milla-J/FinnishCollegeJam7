using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject popup; //reference to the maze/robot popup
    public bool popupOpen = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OpenPopup()
    {
        if (!popupOpen)
        {
            popup.SetActive(true);
            popupOpen = true;
            //add any cool visual effects
        }
    }
}
