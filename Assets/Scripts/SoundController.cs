using System;
using UnityEngine;
using UnityEngine.UI;

public class SoundController : MonoBehaviour
{
    public static SoundController SC;
    private void Awake()
    {
        if (SC == null)
        {
            DontDestroyOnLoad(gameObject);
            SC = this;
        }
        else if (SC != null)
        {
            Destroy(gameObject);
        }
    }
}