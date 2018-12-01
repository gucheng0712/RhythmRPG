using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransitionScreen : MonoBehaviour
{
    [SerializeField] protected MaskableGraphic[] graphicToFade;

    [SerializeField] float m_fadeInDuration = 1.0f;
    [SerializeField] float m_fadeOutDuration = 1.0f;


    public float FadeInDuration { get { return m_fadeInDuration; } set { m_fadeInDuration = value; } }
    public float FadeOutDuration { get { return m_fadeOutDuration; } set { m_fadeOutDuration = value; } }

    private void Awake()
    {
        if (graphicToFade.Length == 0)
            graphicToFade = GetComponentsInChildren<MaskableGraphic>();
        SetAlpha(0);
    }



    void Fade(float targetAlpha, float duration)
    {
        foreach (var graphic in graphicToFade)
        {
            if (graphic != null)
            {
                graphic.CrossFadeAlpha(targetAlpha, duration, true);
            }
        }
    }

    public void FadeOut()
    {
        SetAlpha(1);
        Fade(0, FadeOutDuration);
    }

    public void FadeIn()
    {
        SetAlpha(0);
        Fade(1, FadeInDuration);
    }

    public void SetAlpha(float alpha)
    {
        foreach (var graphic in graphicToFade)
        {
            if (graphic != null)
            {
                graphic.canvasRenderer.SetAlpha(alpha);
            }
        }
    }

}
