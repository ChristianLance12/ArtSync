using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingTextAnimation : MonoBehaviour
{
    public float animTime;
    private Text text;
    void OnEnable()
    {
        text = GetComponent<Text>();
        StartCoroutine(Animate());
    }

    // Update is called once per frame
    private IEnumerator Animate()
    {
        text.text = "LOADING...";
        yield return new WaitForSeconds(animTime);
        text.text = "LOADING   ";
        yield return new WaitForSeconds(animTime);
        text.text = "LOADING.  ";
        yield return new WaitForSeconds(animTime);
        text.text = "LOADING.. ";
        yield return new WaitForSeconds(animTime);
        StartCoroutine(Animate());
    }
}
