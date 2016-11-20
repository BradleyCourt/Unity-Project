using UnityEngine;
using System.Collections;

public class PauseScript : MonoBehaviour
{

    public GameObject PauseMenu;
    private bool paused;
    private int state = 0;
    // Use this for initialization
    void Start()
    {

    }

    void Update()
    {
        if (Input.GetButtonDown("Escape"))
        {
            state = ((state + 1) % 2);
            paused = state == 0;
            PauseMenu.SetActive(paused);
            Time.timeScale = state % 2;
        }
    }
}
