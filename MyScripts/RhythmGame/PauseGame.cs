using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameManager;

public class PauseGame : MonoBehaviour
{

    public GameObject pauseMenu;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    public void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            Pause();
        }
    }

    public void OnApplicationFocus(bool focus)
    {
        if (focus == false && GameManager_Menu.Instance.gameState == GameState.Running)
        {
            Pause();
        }
    }

    public void Pause()
    {
        if (GameManager_Menu.Instance.gameState == GameState.Running)
        {
            pauseMenu.SetActive(true);
        }
        else
        {
            pauseMenu.SetActive(false);
        }

        GameManager_Menu.Instance.TransformGameState();
    }
}
