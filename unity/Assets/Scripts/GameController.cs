using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public bool paused;
    public bool viewing;
    public GameObject pauseUI;
    public GameObject viewtxt;
    public GameObject viewtxt2;
    public Material marble;
    public string[] images;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        marble = Resources.Load<Material>("Textures/Marble");
        
    }

    // Update is called once per frame
    void Update()
    {
        if (viewing == false)
        {
            if (Input.GetKeyDown(KeyCode.Escape) && paused == false)
            {

                Pause();
                pauseUI.SetActive(true);
            }
            else if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Mouse0) && paused)
            {

                UnPause();
                pauseUI.SetActive(false);
            }
        }
    }
    public void Pause()
    {
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0;
        paused = true;
    } 
    public void UnPause()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1;
        paused = false;
    }
}
 