using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlaySound : MonoBehaviour
{
    [SerializeField] private bool isMusic = false;
    private AudioSource audioSource;

    private float origanalVolume;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        origanalVolume = audioSource.volume;

        GetComponent<AudioSource>().volume = VolumeModifier(origanalVolume);
    }

    public void PlayAnimationSound(AudioClip soundEffect)
    {
        audioSource.PlayOneShot(soundEffect);
    }

    public float VolumeModifier(float sound)
    {
        float modifiedvolume;

        if (isMusic)
        {
            modifiedvolume = sound * PlayerPrefs.GetFloat("MusicVolume");
        }
        else
        {
            modifiedvolume = sound * PlayerPrefs.GetFloat("SFXVolume");
        }

        return modifiedvolume;
    }

}
