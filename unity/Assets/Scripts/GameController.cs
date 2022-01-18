using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public bool editor;
    public bool paused;
    public bool viewing;
    public GameObject pauseUI;
    public GameObject viewtxt;
    public Text viewtxtText;
    public GameObject viewtxt2;
    public Text viewtxt2Text;
    public Material[] textures;
    public string[] images;
    public GameObject loadingScreen;
    public int loadedItems;
    public int totalItems;
    public List<GameObject> loadedArt = new List<GameObject>();
    public List<GameObject> loadedObj = new List<GameObject>();
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
   
        
    }

    // Update is called once per frame
    void Update()
    {
        if (viewing == false)
        {
            if (Input.GetKeyDown(KeyCode.Escape) && paused == false)
            {

                Pause();
                 #if !UNITY_EDITOR
                WebGLPluginJS.OnUnityPause();
#endif
                pauseUI.SetActive(true);
            }
            else if (Input.GetKeyDown(KeyCode.Escape) && paused)
            {

                UnPause();
                pauseUI.SetActive(false);
                #if !UNITY_EDITOR
                WebGLPluginJS.OnUnityUnpause();
#endif
            }
        }
        if (loadedItems >= totalItems && loadingScreen == enabled || totalItems == 0)
        {
            loadingScreen.SetActive(false);
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
 