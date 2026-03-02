using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [SerializeField] private int scene;
    public void Clicked()
    {
        SceneManager.LoadScene(scene - 1);
    }
}
