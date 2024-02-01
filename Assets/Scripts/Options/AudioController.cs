using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class AudioController : MonoBehaviour
{
    public Sprite audioOn;
    public Sprite audioOff;
    public GameObject audioButton;
    public Slider slider;

    public AudioSource audioSource;
    void Start()
    {
        GameObject projectileObject = GameObject.Find("Projectile");

        if (projectileObject != null)
        {
            audioSource = projectileObject.GetComponent<AudioSource>();
        }
    }
    void Update()
    {
        //audioSource.volume = slider.value;
    }

    public void AudioButton()
    {
        if (audioSource.volume == 1)
        {
            slider.value = 0;
            audioButton.GetComponent<Image>().sprite = audioOff;
        }
        else
        {
            slider.value = 1;
            audioButton.GetComponent<Image>().sprite = audioOn;
        }
    }
}
