using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour
{
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayAnimationSound(AudioClip soundEffect)
    {
        Debug.Log("working");
        audioSource.PlayOneShot(soundEffect);
    }
}
