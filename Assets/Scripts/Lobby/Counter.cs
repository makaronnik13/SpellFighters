using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Counter : MonoBehaviour
{
    public Slider SliderVisual;
    public TextMeshProUGUI ValueText;
    public AudioSource Sound;

    private void Start()
    {
        HideTimer();
    }

    private void HideTimer()
    {
        foreach (Transform t in transform)
        {
            t.gameObject.SetActive(false);
        }
    }

    private void ShowTimer()
    {
        foreach (Transform t in transform)
        {
            t.gameObject.SetActive(true);
        }
    }

    public void StartCount(int v, Action p)
    {
        ShowTimer();
        StartCoroutine(Count(v,p));
    }

    private IEnumerator Count(int v, Action p)
    {
        float timer = 0;
        float secondsTimer = 0;

        while (timer<=v)
        {
            secondsTimer += Time.deltaTime;
            if (secondsTimer>=1)
            {
                Sound.PlayOneShot(Sound.clip);
                secondsTimer = 0;
            }
            timer += Time.deltaTime;
            ValueText.text = Mathf.RoundToInt(v - timer)+"";
            SliderVisual.value = timer / v;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        HideTimer();
        p.Invoke();
    }
}
