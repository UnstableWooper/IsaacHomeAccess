using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StPatrickCloneManager : MonoBehaviour
{
    [SerializeField] GameObject StPatrickBoss;
    [SerializeField] GameObject lazerBeam;

    [SerializeField] Vector2 rangeOfGold;
    [SerializeField] int rangeOfAngle;
    [SerializeField] private GameObject gold;

    private GameObject _realStPatrick;
    public List<GameObject> _fakeStPatricks = new List<GameObject>();
    private void Update()
    {
        if (_realStPatrick == null)
        {
            _realStPatrick = GameObject.FindGameObjectWithTag("Real");
        }
        else
        {
            BossHP RealStPatrickHP = _realStPatrick.GetComponent<BossHP>();
            if (RealStPatrickHP.TrueBossHp <= 0)
            {
                StPatrickBoss.SetActive(true);
                Destroy(_realStPatrick.gameObject);
                GameObject[] MiniStPatricks = GameObject.FindGameObjectsWithTag("MiniBoss");
                foreach (GameObject MiniStPatrick in MiniStPatricks)
                {
                    Destroy(MiniStPatrick);
                    lazerBeam.SetActive(false);
                    foreach (GameObject fakeStPatricks in _fakeStPatricks)
                    {
                        _fakeStPatricks.Remove(fakeStPatricks);
                    }
                }
            }

            if (_fakeStPatricks.Count <= 1)
            {
                _fakeStPatricks.Add(GameObject.FindGameObjectWithTag("MiniBoss"));
            }
            else
            {
                foreach (GameObject MiniStPatrick in _fakeStPatricks)
                {
                    if (MiniStPatrick.activeSelf == false)
                    {
                        Destroy(MiniStPatrick.gameObject);
                        for (int i = 1; i <= UnityEngine.Random.Range(Mathf.RoundToInt(rangeOfGold.x), Mathf.RoundToInt(rangeOfGold.y)); i++)
                        {
                            Instantiate(gold, new Vector2(MiniStPatrick.transform.position.x,
                                MiniStPatrick.transform.position.y), Quaternion.Euler(Quaternion.identity.x,
                                Quaternion.identity.y, UnityEngine.Random.Range(rangeOfAngle, -rangeOfAngle)));
                        }
                    }
                }
            }
        }
    }
}