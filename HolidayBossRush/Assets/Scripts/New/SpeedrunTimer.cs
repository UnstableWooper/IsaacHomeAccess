using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using Unity.VisualScripting;
public class SpeedrunTimer : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI speedrunTimer;

    private float _time;
    void Update()
    {
        _time = PlayerPrefs.GetFloat("SpeedrunTime");
        _time += Time.deltaTime;

        speedrunTimer.text += Time.deltaTime.ToString("00:00.00");

        TimeSpan time = TimeSpan.FromSeconds(PlayerPrefs.GetFloat("SpeedrunTime", 0));
        speedrunTimer.text = string.Format("{0:00}:{1:00}:{2:00}",
            time.Minutes,
            time.Seconds,
            time.Milliseconds / 10);

        PlayerPrefs.SetFloat("SpeedrunTime", _time);
        PlayerPrefs.Save();
    }

    public void ResetTimer()
    {
        PlayerPrefs.SetFloat("SpeedrunTime", 0);
        PlayerPrefs.Save();
    }
}
