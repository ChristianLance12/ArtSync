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
    public AudioSource btnClk;
    // Start is called before the first frame update
    void Start()
    {
        gM = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        pM = GameObject.FindWithTag("Player").GetComponent<PlayerMove>();
    }


   public void XSlider()
    {
        pM.newRotX = (float)(xSlider.value * 18.18181818181818);
        Click();
    }
    public void YSlider()
    {
        pM.newRotY = (float)(ySlider.value * 27.27272727272727);
        Click();
    }
    public void ResumeButton()
    {
        gM.pauseUI.SetActive(false);
        gM.settingUI.SetActive(false);
        gM.UnPause();
        Click();
    }
    public void SettingsButton()
    {
        gM.pauseUI.SetActive(false);
        gM.settingUI.SetActive(true);
        Click();
    }
    public void BackFromSettings()
    {
        gM.pauseUI.SetActive(true);
        gM.settingUI.SetActive(false);
        Click();
    }
    public void MusicVolume()
    {
        gM.music.volume = music.value/20;
        Click();
    }
    public void sFXVolume()
    {
        pM.footSteps1.volume = sFX.value/20;
        pM.footSteps2.volume = sFX.value/20;
        btnClk.volume = sFX.value / 20;
        gM.hoverUI.volume = sFX.value / 20;
        gM.gameObject.GetComponent<ArtLoad>().placeSnd.volume = sFX.value / 20;
        gM.gameObject.GetComponent<ObjFromStream>().placeSnd.volume = sFX.value / 20;
        gM.gameObject.GetComponent<SmallObjFromStream>().placeSnd.volume = sFX.value / 20;
        Click();

    }
    public void Click()
    {
        btnClk.Play();
    }
}
