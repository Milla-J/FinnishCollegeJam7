using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpManager : MonoBehaviour
{
    [SerializeField] private GameObject PopUpFrame;
    private bool _PopupOpen = false;
    private Labyrinth _OpenLabyrinth;

    private void Awake()
    {
        _OpenLabyrinth = null;
        _PopupOpen = false;
        PopUpFrame.SetActive(false);
    }
    public void HandleLabyrinthPopUp(Labyrinth labyrinthToShow)
    {
        if (_PopupOpen && _OpenLabyrinth!= labyrinthToShow)
        {
            _OpenLabyrinth = labyrinthToShow;
            _PopupOpen = true;

            _OpenLabyrinth.Show();
            PopUpFrame.SetActive(true);
            //add any cool visual effects
        }
        else if(_PopupOpen && _OpenLabyrinth == labyrinthToShow)
        {
            _OpenLabyrinth.Hide();
            PopUpFrame.SetActive(false);

            _OpenLabyrinth = null;
            _PopupOpen = false;
        }
        else if (!_PopupOpen)
        {
            _OpenLabyrinth = labyrinthToShow;
            _PopupOpen = true;

            _OpenLabyrinth.Show();
            PopUpFrame.SetActive(true);
            //add any cool visual effects
        }
        else
        {
            Debug.Log("WTH IS THIS CONDITION?!!!!");
        }
    }
}
