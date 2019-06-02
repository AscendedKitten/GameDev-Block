using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEnd : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {

            int currentIndex = SceneManager.GetActiveScene().buildIndex;

            if (currentIndex == 6)
                SceneManager.LoadScene(0);
            else
                SceneManager.LoadScene(currentIndex + 1);
        } 
    }
}
