using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject popup; //reference to the maze/robot popup
    public bool popupOpen = false;

    public Slider slider; //Reference to the satisfaction meter
    private float satisfaction; //satisfaction level
    public float delay = 0.5f;
    private bool stopSatisfactionLowering = false;

    public bool fixing = false;

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

    public void StopLowerSatisfaction()
    {
        Debug.Log("Stopping coroutine");
        stopSatisfactionLowering = true;
    }

    public IEnumerator LowerSatisfactio()
    {
        while (!stopSatisfactionLowering)
        {
            if (!fixing && satisfaction > slider.minValue)
            {
                satisfaction -= 0.02f;
                slider.value = satisfaction;
            }
            yield return new WaitForSeconds(delay);
        }

        stopSatisfactionLowering = false;
    }

    public void AddToSatisfaction()
    {
        if (satisfaction < slider.maxValue)
        {
            satisfaction += 0.2f;
            slider.value = satisfaction;
        }
    }
}
