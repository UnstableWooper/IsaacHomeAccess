using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StPatrickCloneManager : MonoBehaviour
{
    [SerializeField] GameObject StPatrickBoss;

    private GameObject RealStPatrick;
    private void Update()
    {
        if (RealStPatrick == null)
        {
            RealStPatrick = GameObject.FindGameObjectWithTag("Real");
        }
        else
        {
            BossHP RealStPatrickHP = RealStPatrick.GetComponent<BossHP>();
            if(RealStPatrickHP.TrueBossHp <= 0)
            {
                StPatrickBoss.SetActive(true);
                Destroy(RealStPatrick.gameObject);
                GameObject[] MiniStPatricks = GameObject.FindGameObjectsWithTag("MiniBoss");
                foreach(GameObject MiniStPatrick in MiniStPatricks)
                {
                    Destroy(MiniStPatrick);
                }
            }
        }
    }
}   
