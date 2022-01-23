using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerSettings : MonoBehaviour
{
    private GameController gM;
    private PlayerMove pM;
    public Slider xSlider;
    public Slider ySlider;
    public Slider music;
    public Slider sFX;
    // Start is called before the first frame update
    void Start()
    {
        gM = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        pM = GameObject.FindWithTag("Player").GetComponent<PlayerMove>();
    }


   public void XSlider()
    {
        pM.newRotX = (float)(xSlider.value * 1.66666666667 * pM.rotX);
    }
    public void YSlider()
    {
        pM.newRotY = (float)(ySlider.value * (1.6666666666667) * pM.rotY);
    }
    public void ResumeButton()
    {
        gM.pauseUI.SetActive(false);
        gM.settingUI.SetActive(false);
        gM.UnPause();
    }
    public void SettingsButton()
    {
        gM.pauseUI.SetActive(false);
        gM.settingUI.SetActive(true);
    }
    public void BackFromSettings()
    {
        gM.pauseUI.SetActive(true);
        gM.settingUI.SetActive(false);
    }
    public void MusicVolume()
    {
        gM.music.volume = music.value;
    }
    public void sFXVolume()
    {
        pM.footSteps1.volume = sFX.value;
        pM.footSteps2.volume = sFX.value;
    }
}
