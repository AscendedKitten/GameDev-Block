using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReceiveSkinOnStartup : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private void Awake()
    {
        spriteRenderer = GameObject.FindWithTag("Player").GetComponent<SpriteRenderer>();
        Sprite x = Resources.Load<Sprite>("Skins/"+GameController.GC.TogglePlayerSkinName);
        spriteRenderer.sprite = x;
    }
}
