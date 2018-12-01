using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameManager;
using UnityEngine.UI;

public class NotificationFader : TransitionScreen
{
    [HideInInspector] public Text msg;

    private void Start()
    {
        msg = graphicToFade[0].gameObject.GetComponent<Text>();
    }


    public void ShowNotificationPanel()
    {
        StartCoroutine(NotificationRountine());
    }

    IEnumerator NotificationRountine()
    {
        FadeIn();
        yield return new WaitForSecondsRealtime(FadeInDuration);
        FadeOut();
    }



}
