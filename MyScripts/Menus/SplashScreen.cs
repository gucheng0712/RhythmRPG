using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Menus;
using UnityEngine.UI;

[RequireComponent(typeof(TransitionScreen))]
public class SplashScreen : MonoBehaviour
{
    [SerializeField] TransitionScreen m_splashScreenFader;
    [SerializeField] TransitionScreen m_artistsCreditScreenFader;
    [SerializeField] TransitionScreen m_designerCreditScreenFader;
    [SerializeField] TransitionScreen m_programmerCreditScreenFader;
    [SerializeField] TransitionScreen m_songCreditScreenFader;
    [SerializeField] LoadingScreen m_LoadingScreen;
    [SerializeField] float m_delay = 1.0f;

    void Awake()
    {
        // m_splashScreenFader = GetComponent<TransitionScreen>();
    }

    void Start()
    {
        m_splashScreenFader.FadeIn();
    }
    public void OnSplashScreenPressed()
    {
        Debug.Log("pressed");
        StartCoroutine(LoadToStartMenuRoutine());
        GetComponentInChildren<Button>().interactable = false;
    }

    IEnumerator LoadToStartMenuRoutine()
    {
        yield return new WaitForSeconds(m_delay);
        m_splashScreenFader.FadeOut();
        yield return new WaitForSeconds(m_splashScreenFader.FadeOutDuration);
        m_artistsCreditScreenFader.FadeIn();
        yield return new WaitForSeconds(m_artistsCreditScreenFader.FadeInDuration);
        m_artistsCreditScreenFader.FadeOut();
        yield return new WaitForSeconds(m_artistsCreditScreenFader.FadeOutDuration);
        m_designerCreditScreenFader.FadeIn();
        yield return new WaitForSeconds(m_designerCreditScreenFader.FadeInDuration);
        m_designerCreditScreenFader.FadeOut();
        yield return new WaitForSeconds(m_designerCreditScreenFader.FadeOutDuration);
        m_programmerCreditScreenFader.FadeIn();
        yield return new WaitForSeconds(m_programmerCreditScreenFader.FadeInDuration);
        m_programmerCreditScreenFader.FadeOut();
        yield return new WaitForSeconds(m_programmerCreditScreenFader.FadeOutDuration);
        m_songCreditScreenFader.FadeIn();
        yield return new WaitForSeconds(m_songCreditScreenFader.FadeInDuration);
        m_songCreditScreenFader.FadeOut();
        yield return new WaitForSeconds(m_songCreditScreenFader.FadeOutDuration);

        LevelLoader.LoadLevel("StartMenu", Menu_StartMenu.Instance, m_LoadingScreen, false);

        Destroy(gameObject, 5);
    }


}
