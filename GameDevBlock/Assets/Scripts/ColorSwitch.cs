using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ColorScript : MonoBehaviour
{

    [Header("Tilemaps")]
    [SerializeField] private Tilemap primary_tilemap;
	[SerializeField] private Tilemap secondary_tilemap;

    private TilemapRenderer primary_tilemapRenderer;
    private TilemapRenderer secondary_tilemapRenderer;

	[Header("Settings")]
	[SerializeField] private KeyCode switchButton;

	private bool primaryColorActive = true;

	private bool inSwitch = false;
	
	// Use this for initialization
	void Start ()
	{
        if (primary_tilemap != null && secondary_tilemap != null)
        {
            if (primary_tilemap.GetComponent<TilemapRenderer>() != null && secondary_tilemap.GetComponent<TilemapRenderer>() != null)
            {
                primary_tilemapRenderer = primary_tilemap.GetComponent<TilemapRenderer>();
                secondary_tilemapRenderer = secondary_tilemap.GetComponent<TilemapRenderer>();
            }
            else
            {
                throw new Exception("The tilemaps do not have tilemaprenderer");
            }
        }
        else
        {
            throw new Exception("No tilemaps have been supplied.");
        }
		primary_tilemapRenderer.enabled = true;
		secondary_tilemapRenderer.enabled = false;
	}
	
	// Update is called once per frame
	void Update ()
	{
        if (Input.GetKeyDown(switchButton))
		{
			if (!inSwitch)
			{
				inSwitch = true;
				if (primaryColorActive) //when the primary color is active
				{
					//deactivate the primary color
					primaryColorActive = false;
				}
				else if(!primaryColorActive) //when the primary color is not active
				{
					//activate primary color
					primaryColorActive = true;
				}

                if (primaryColorActive)
                {
                    //deactivate the secondary color when the primary color is active
                    //Primary color is by default active
                    //Secondary color is by default deactived
                    primary_tilemapRenderer.enabled = true;
                    secondary_tilemapRenderer.enabled = false;
                }
                else if(!primaryColorActive)
                {
                    //deactivate the primary color when the secondary color is active
                    primary_tilemapRenderer.enabled = false;
                    secondary_tilemapRenderer.enabled = true;
                }
            }
		}

        if (Input.GetKeyUp(switchButton))
		{
			if (inSwitch)
			{
				inSwitch = false;
			}
		}
	}
}
