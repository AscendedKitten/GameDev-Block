using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public  class ReceiveSkin : ScriptableObject
{
    private SpriteRenderer sr;

    public void ChangeSkin(SpriteRenderer spriteRenderer)
    {
        Sprite x = Resources.Load<Sprite>("Skins/"+GameController.GC.TogglePlayerSkinName);
        spriteRenderer.sprite = x;
    }
}
