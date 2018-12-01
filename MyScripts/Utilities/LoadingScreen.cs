using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


// Reference: https://www.youtube.com/watch?v=YMj2qPq9CP8
public class LoadingScreen : MonoBehaviour
{

    [SerializeField] Slider m_slider;

    public void PlayTransition(string sceneName)
    {
        StartCoroutine(LoadAsynchronously(sceneName));
    }

    IEnumerator LoadAsynchronously(string sceneName)
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(sceneName);

        while (!op.isDone)
        {
            float progress = Mathf.Clamp01(op.progress / 0.9f);
            // Debug.Log(progress);
            m_slider.value = progress;
            yield return null;
        }
        Destroy(gameObject);
    }
}
