using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMusic : MonoBehaviour
{
    [SerializeField] private AudioClip soundEffect;

    private PlaySound playSound;

    // Start is called before the first frame update
    void Awake()
    {
        playSound = GetComponent<PlaySound>();
    }

    // Update is called once per frame
    void Start()
    {
        playSound.PlayAnimationSound(soundEffect);
    }
}
