using UnityEngine;
using InControl;
using System.Collections;

public class PauseScript : MonoBehaviour
{

    public GameObject PauseMenu;
    private bool paused;
    private int state = 0;
    InputDevice device;
    // Use this for initialization
    void Start()
    {

    }

    void Update()
    {
        if (Input.GetButtonDown("Escape") || (device.MenuWasPressed))
        {
            state = ((state + 1) % 2);
            paused = state == 0;
            PauseMenu.SetActive(paused);
            Time.timeScale = state % 2;
        }
    }
}
