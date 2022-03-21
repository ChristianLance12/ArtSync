using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class LoadMusic : MonoBehaviour
{
    public AudioSource music;
    public string URL;
    void Start()
    {
        if (URL != null)
        {
            LoadSong(URL);
        }
    }
    public void LoadSong(string URL)
    {
        string[] words = URL.Split('.');
        string fT = words[words.Length - 1];
        Debug.Log(fT);
        if (fT == "mp3")
        {
            StartCoroutine(LoadMP3Coroutine(URL));
        }
       else if (fT == "wav")
        {
            StartCoroutine(LoadWAVCoroutine(URL));
        }

    }
        
    IEnumerator LoadMP3Coroutine(string URL)
    {
        using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(URL, AudioType.MPEG))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.Log(www.error);
            }
            else
            {
                music.clip = DownloadHandlerAudioClip.GetContent(www);
                yield return 0;
                music.Play();
            }
        }
    }
    IEnumerator LoadWAVCoroutine(string URL)
    {
        using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(URL, AudioType.WAV))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.Log(www.error);
            }
            else
            {
                music.clip = DownloadHandlerAudioClip.GetContent(www);
                yield return 0;
                music.Play();
            }
        }
    }
}
