using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.UIElements;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadWithIndex : MonoBehaviour
{
    [SerializeField] private Animator animator = null;

    public void LoadSceneWithIndex(int index)
    {
        if (index >= 0)
        {
            SceneManager.LoadScene(index);
        }
    }

    public void ActivateTriggerWithName(string name)
    {
        if (name != null && animator != null)
        {
            animator.SetTrigger(name);
        }
    }

    public void QuitApplication()
    {
        Application.Quit();
    }

    public void ResumeGame()
    {
        // TODO
        // - First: Pause the Game in movement script by pressing ESCAPE and by using [Time.timeScale = 0]. This should active the Pause-Menu (not the scene, the panel).
        // - Resume the Game by pressing ESCAPE again and by using [Time.timeScale = 1]. This should hide the Pause-Menu again.
    }

    public void ProgressToNextLevel()
    {
        int index = SceneManager.GetActiveScene().buildIndex;
        if (index < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(index + 1);
        }
        else
        {
            SceneManager.LoadScene(0);
        }
    }
}
