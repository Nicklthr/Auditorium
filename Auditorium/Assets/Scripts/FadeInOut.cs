using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeInOut : MonoBehaviour
{
    public CanvasGroup CanvasGroup;
    public bool fadein = false;
    public bool fadeout = false;

    public float TimeToFade;

    // Update is called once per frame
    void Update()
    {
        if(fadein == true)
        {
                if(CanvasGroup.alpha < 1)
            {
                CanvasGroup.alpha += TimeToFade * Time.deltaTime;
                if(CanvasGroup.alpha >= 1)
                {
                    fadein = false;
                }
            }
        }
        if (fadeout == true)
        {
            if (CanvasGroup.alpha > 0)
            {
                CanvasGroup.alpha -= TimeToFade * Time.deltaTime;
                if (CanvasGroup.alpha == 0)
                {
                    fadeout = false;
                }
            }
        }
    }

    public void FadeIn()
    {
        fadein = true;
    }

    public void FadeOut()
    {
        fadeout = true;
    }
}
