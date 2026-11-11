using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class BossDescription : MonoBehaviour
{
    [SerializeField] private string bossDescription;
    [SerializeField] private int scene;

    private TMP_Text _descriptionText;

    private string _grade;
    private string _time;
    private string _health;
    private string _accuracy;

    private void Awake()
    {
        _descriptionText = GetComponent<TMP_Text>();
    }

    private void Update()
    {
        if(PlayerPrefs.GetString("Scene" + scene + "GradeTracker") != null)
        {
            _grade = 
                PlayerPrefs.GetString("Scene"+scene+"GradeTracker");
            _time =
                PlayerPrefs.GetString("Scene"+scene+"TimeTracker");
            _health =
                PlayerPrefs.GetString("Scene" + scene + "HealthTracker");
            _accuracy =
                PlayerPrefs.GetString("Scene" + scene + "AccuracyTracker");
        }
        else
        {
            _grade = "Unknown";
            _time = "Unknown";
            _health = "Unknown";
            _accuracy = "Unknown";
        }

        _descriptionText.text =
            bossDescription + "\n" +
            "Grade: " + _grade + "\n" +
            "Time: " + _time + "\n" +
            "Health: " + _health + "\n" +
            "Accuracy: " + _accuracy;
    }
}
