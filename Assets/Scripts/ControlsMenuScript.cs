using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class ControlsMenuScript : MonoBehaviour
{
    private Event keyEvent;
    private Text buttonText;
    private KeyCode newKeyCode;
    private Transform gridLayout;

    private bool waitingForKey;

    void Start()
    {
        gridLayout = GameObject.FindWithTag("ControlsMenuGrid").transform;
        waitingForKey = false;
        for (int i = 0; i < gridLayout.childCount; i++)
        {
            if (gridLayout.GetChild(i).name.Equals("RightInput"))
            {
                gridLayout.GetChild(i).GetComponentInChildren<Text>().text = GameController.GC.Right.ToString();
            }
            else if (gridLayout.GetChild(i).name.Equals("LeftInput"))
            {
                gridLayout.GetChild(i).GetComponentInChildren<Text>().text = GameController.GC.Left.ToString();
            }
            else if (gridLayout.GetChild(i).name.Equals("UpInput"))
            {
                gridLayout.GetChild(i).GetComponentInChildren<Text>().text = GameController.GC.Up.ToString();
            }
            else if (gridLayout.GetChild(i).name.Equals("SwitchInput"))
            {
                gridLayout.GetChild(i).GetComponentInChildren<Text>().text = GameController.GC.Switch.ToString();
            }
        }
    }

    private void OnGUI()
    {
        keyEvent = Event.current;
        if (keyEvent.isKey && waitingForKey)
        {
            newKeyCode = KeyCode.Escape;
            newKeyCode = keyEvent.keyCode;
            waitingForKey = false;
        }
    }

    public void StartAssignment(string keyName)
    {
        if (!waitingForKey)
        {
            StartCoroutine(AssignKey(keyName));
        }
    }

    public void SendText(Text text)
    {
        buttonText = text;
    }

    IEnumerator WaitForKey()
    {
        while (!keyEvent.isKey)
        {
            yield return null;
        }
    }


    public IEnumerator AssignKey(string keyName)
    {
        waitingForKey = true;
        
        CursorLockMode state = Cursor.lockState;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        


        yield return WaitForKey();
        if (newKeyCode != KeyCode.Escape &&
            newKeyCode != GameController.GC.Right &&
            newKeyCode != GameController.GC.Left &&
            newKeyCode != GameController.GC.Up &&
            newKeyCode != GameController.GC.Switch)
        {
            switch (keyName)
            {
                case "right":
                    GameController.GC.Right = newKeyCode;
                    buttonText.text = GameController.GC.Right.ToString();
                    PlayerPrefs.SetString("rightKey", GameController.GC.Right.ToString());
                    break;
                case "left":
                    GameController.GC.Left = newKeyCode;
                    buttonText.text = GameController.GC.Left.ToString();
                    PlayerPrefs.SetString("leftKey", GameController.GC.Left.ToString());
                    break;
                case "up":
                    GameController.GC.Up = newKeyCode;
                    buttonText.text = GameController.GC.Up.ToString();
                    PlayerPrefs.SetString("upKey", GameController.GC.Up.ToString());
                    break;
                case "switch":
                    GameController.GC.Switch = newKeyCode;
                    buttonText.text = GameController.GC.Switch.ToString();
                    PlayerPrefs.SetString("switchKey", GameController.GC.Switch.ToString());
                    break;
            }
        }
        Cursor.lockState = state;
        Cursor.visible = true;

        yield return null;
    }
}