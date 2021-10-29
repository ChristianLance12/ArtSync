using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingTextAnimation : MonoBehaviour
{
    private Text text;
    void Start()
    {
        text = GetComponent<Text>();
        StartCoroutine(Animate());
    }

    // Update is called once per frame
    private IEnumerator Animate()
    {
        text.text = "LOADING...";
        yield return new WaitForSeconds(0.5f);
        text.text = "LOADING   ";
        yield return new WaitForSeconds(0.5f);
        text.text = "LOADING.  ";
        yield return new WaitForSeconds(0.5f);
        text.text = "LOADING.. ";
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(Animate());
    }
}
