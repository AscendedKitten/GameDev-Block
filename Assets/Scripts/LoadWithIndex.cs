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
            if (index == 0)
            {
                SummonPauseMenuScript.Resume();
            }
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
        //TODO
        //Dont forget to add the SummonPauseMenuScript to the MainCamera in each Scene you want to have a pause menu.
        SummonPauseMenuScript.Resume();
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
