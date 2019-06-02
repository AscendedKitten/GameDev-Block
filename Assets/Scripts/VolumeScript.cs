using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeScript : MonoBehaviour
{
    [SerializeField] private Slider slider;

    public void Start()
    {
        if (slider != null)
        {
            slider.onValueChanged.AddListener(delegate { SliderValueChanged();});
        }
    }

    private void SliderValueChanged()
    {
        AudioListener.volume = slider.value / slider.maxValue;
    }
}
