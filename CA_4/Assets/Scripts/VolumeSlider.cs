using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    public Slider slider;
    public AudioSource audioSource;
    
    // Start is called before the first frame update
    void Start()
    {
        slider.onValueChanged.AddListener((v) =>
        {
            audioSource.volume = v;
            GlobalVariables.musicVolume = v;
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
