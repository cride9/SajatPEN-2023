using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class AudioController : MonoBehaviour
{
    public Sprite audioOn;
    public Sprite audioOff;
    public GameObject audioButtonSFX;
    public GameObject audioButtonMusic;
    private Slider sfxSlider;
    private Slider musicSlider;

    private AudioSource audioSourceSFX;
    private AudioSource audioSourceMusic;
    void Start()
    {
        sfxSlider = GameObject.Find("SFXSlider").GetComponent<Slider>();
        musicSlider = GameObject.Find("MusicSlider").GetComponent<Slider>();
    }
    void Update()
    {
        GameObject projectileObject = GameObject.Find("Projectile");

        if (projectileObject is not null)
        {
            audioSourceSFX = projectileObject.GetComponent<AudioSource>();
            if (audioSourceSFX is not null)
            {
                audioSourceSFX.volume = sfxSlider.value;
            }
        }

        AudioListener mainCameraObject = GameObject.Find("Main Camera").GetComponent<AudioListener>();

        if (mainCameraObject is not null)
        {
            audioSourceMusic = mainCameraObject.GetComponent<AudioSource>();
            if (audioSourceMusic is not null)
            {
                audioSourceMusic.volume = musicSlider.value;
            }
        }
    }

    public void SFXAudioMuteButton()
    {
        if (audioSourceSFX.volume == 0)
        {
            sfxSlider.value = 1;
            audioButtonSFX.GetComponent<Image>().sprite = audioOn;
        }
        else
        {
            sfxSlider.value = 0;
            audioButtonSFX.GetComponent<Image>().sprite = audioOff;
        }
    }

    public void MusicAudioMuteButton()
    {
        if (audioSourceMusic.volume == 0)
        {
            musicSlider.value = 1;
            audioButtonMusic.GetComponent<Image>().sprite = audioOn;
        }
        else
        {
            musicSlider.value = 0;
            audioButtonMusic.GetComponent<Image>().sprite = audioOff;
        }
    }
}
