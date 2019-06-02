using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ChangeSkinScript : MonoBehaviour
{
    private ToggleGroup tg;
    private Toggle toggle;
    private Sprite _sprite;
    
    public void ChangeSkin()
    {
        tg = GameController.GC.GetComponent<ToggleGroup>();
        IEnumerable<Toggle> toggles = tg.ActiveToggles();

        if (toggles != null)
        {
            foreach (Toggle t in toggles)
            {
                if (toggles.Count() == 1)
                {
                    toggle = t;
                    _sprite = t.transform.GetChild(1).GetComponent<Image>().sprite;
                    PlayerPrefs.SetString("activeSkinName", _sprite.name);
                    GameController.GC.ConfigureSkin();
                }
            }
        }
    }
}
