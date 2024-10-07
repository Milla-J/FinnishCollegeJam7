using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject popup; //reference to the maze/robot popup
    public bool popupOpen = false;

    public Slider slider; //Reference to the satisfaction meter
    private float satisfaction; //satisfaction level
    public float delay = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        satisfaction = slider.value;
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

    public IEnumerator LowerSatisfactio()
    {
        while (true)
        {
            satisfaction -= 0.1f;
            slider.value = satisfaction;

            yield return new WaitForSeconds(delay);
        }
    }
}
