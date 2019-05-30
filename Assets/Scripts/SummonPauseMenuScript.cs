using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SummonPauseMenuScript : MonoBehaviour
{
    [SerializeField] public Canvas Canvas;
    [SerializeField] public List<GameObject> Objects;
    private KeyCode summonKey;
    public bool menuActive;

    void Start()
    {
        summonKey = GameController.GC.PauseMenuKey;
        menuActive = false;
    }
    
    // Update is called once per frame
    void Update()
    {
        PauseGame();
    }

    public void PauseGame()
    {
        try
        {
            Canvas = GameObject.FindWithTag("PauseMenu").GetComponent<Canvas>();
        }catch(NullReferenceException e){}
    }
}
