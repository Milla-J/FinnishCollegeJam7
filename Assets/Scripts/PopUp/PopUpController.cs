using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpController : MonoBehaviour
{
    //[SerializeField] private PopUp _popUp;

    private bool _PopupOpen;

    private Labyrinth _OpenedLabyrinth;

    private void Awake()
    {
        _OpenedLabyrinth = null;
        _PopupOpen = false;
    }

    /// <summary>
    /// Performs opening or closing of the maze-PopUp depending on the current conditions
    /// </summary>
    /// <param name="labyrinthToShow"></param>
    public void HandleLabyrinthPopUp(Labyrinth labyrinthToShow)
    {
        if (_PopupOpen && _OpenedLabyrinth != labyrinthToShow)
        {
            //_OpenedLabyrinth.Hide();
            //_OpenedLabyrinth = labyrinthToShow;
            //_OpenedLabyrinth.Show();

            _OpenedLabyrinth.SetActiveFalse();
            _OpenedLabyrinth = labyrinthToShow;
            _OpenedLabyrinth.SetActiveTrue();

            _PopupOpen = true;

            //add any cool visual effects
        }
        else if(_PopupOpen && _OpenedLabyrinth == labyrinthToShow)
        {
            _OpenedLabyrinth.Hide();
            _OpenedLabyrinth = null;

            _PopupOpen = false;

            //add any cool visual effects
        }
        else if (!_PopupOpen)
        {
            _OpenedLabyrinth = labyrinthToShow;
            _OpenedLabyrinth.Show();

            _PopupOpen = true;

            //add any cool visual effects
        }
        else
        {
            Debug.Log("WTH IS THIS CONDITION?!!!!");
        }
    }

    public void CloseIfMatches(Labyrinth labyrinth)
    {
        if(_OpenedLabyrinth == labyrinth)
        {
            _OpenedLabyrinth.Hide();
            _OpenedLabyrinth = null;

            _PopupOpen = false;
            //add any cool visual effects
        }
    }
}
