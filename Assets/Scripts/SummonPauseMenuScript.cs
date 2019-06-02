using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class SummonPauseMenuScript : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public static RectTransform PauseMenuUI;

    private void Start()
    {
        FindPauseMenuUI();
    }

    private void Update()
    {
        if (Input.GetKeyDown(GameController.GC.PauseMenuKey))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    void FindPauseMenuUI()
    {
        PauseMenuUI = GameObject.FindWithTag("PauseMenuCanvas").transform.GetChild(0).GetComponent<RectTransform>();
    }

    public static void Resume()
    {
        RopeSystem.hookEnabled = true;
        PauseMenuUI.gameObject.SetActive(false);
        Time.timeScale = 1;
        GameIsPaused = false;
    }

    public void Pause()
    {
        RopeSystem.hookEnabled = false;
        PauseMenuUI.gameObject.SetActive(true);
        Time.timeScale = 0;
        GameIsPaused = true;
    }
}
