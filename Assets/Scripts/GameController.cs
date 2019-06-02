using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    //TODO
    // - Add [switchButton = GameController.GC.Switch] to ColorScript's Update at the very beginning
    // - Add [up = GameController.GC.Up;        to Movement script's Start() method at the very beginning
    //        left = GameController.GC.Left;
    //        right = GameController.GC.Right;]
    public static GameController GC;
    private bool startup = true;

    public KeyCode Up { get; set; }
    public KeyCode Right { get; set; }
    public KeyCode Left { get; set; }
    public KeyCode Switch { get; set; }
    public Sprite PlayerSkin { get; set; }
    public string TogglePlayerSkinName { get; set; }
    public KeyCode PauseMenuKey = KeyCode.Escape;

    public void Start()
    {
        AudioListener.volume = 1;
    }

    private void Awake()
    {
        if (GC == null)
        {
            DontDestroyOnLoad(gameObject);
            GC = this;
        }
        else if (GC != null)
        {
            Destroy(gameObject);
        }
        ParseKeyCodes();
        ConfigureSkin();
    }

    public void ParseKeyCodes()
    {
        Up = (KeyCode) System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("upKey", "Space"));
        Right = (KeyCode) System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("rightKey", "D"));
        Left = (KeyCode) System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("leftKey", "A"));
        Switch = (KeyCode) System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("switchKey", "K"));
    }
    
    public void ConfigureSkin()
    {
        TogglePlayerSkinName = PlayerPrefs.GetString("activeSkinName", "blueguy");
        SpriteRenderer sr = GameObject.FindWithTag("Player").GetComponent<SpriteRenderer>();
        ReceiveSkin r = new ReceiveSkin();
        r.ChangeSkin(sr);
    }
}

