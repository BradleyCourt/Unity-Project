using UnityEngine;
using InControl;
using System.Collections;

public class PauseScript : MonoBehaviour
{
    public GameObject PauseMenu;
    InputDevice device;

    private bool paused;
    private int state = 1;


    // Use this for initialization
    void Start()
    {

    }

    void Update()
    {
        device = InputManager.ActiveDevice;
        if (Input.GetButtonDown("Escape") || device.MenuWasPressed)
        {
            pause();
        }
    }
    public void pause()
    {
            state = ((state + 1) % 2);
            paused = state == 0;
            PauseMenu.SetActive(paused);
            Time.timeScale = state % 2;
    }
}
