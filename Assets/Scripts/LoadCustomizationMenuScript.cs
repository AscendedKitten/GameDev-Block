using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class LoadCustomizationMenuScript : MonoBehaviour
{
    [SerializeField] private List<Image> objects;

    private void OnEnable()
    {
        LoadRightToggle();
    }

    void LoadRightToggle()
    {
        foreach (Image skinObject in objects)
        {
            if (skinObject != null)
            {
                if (skinObject.sprite.name.Equals(GameController.GC.TogglePlayerSkinName))
                {
                    skinObject.GetComponentInParent<Toggle>().isOn = true;
                }
            }
        }
    }
}
