using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SlimUI.ModernMenu
{
    public class SceneController : MonoBehaviour
    {
        public void LoadScene(int scene)
        {
            GameObject settingsManagerGameObject = GameObject.FindGameObjectWithTag("UIManager");

            if (settingsManagerGameObject != null)
            {
                bool isHardcore = settingsManagerGameObject.GetComponent<UISettingsManager>().isHardcore;

                if (isHardcore)
                    SceneManager.LoadScene(scene + 3);
                else
                    SceneManager.LoadScene(scene);
            }
            else
                SceneManager.LoadScene(scene);
        }
    }
}

