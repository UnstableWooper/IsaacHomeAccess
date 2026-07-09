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
                //StPatrickBoss.GetComponent<BossController>().attackCooldownTimer = 4;
                GameObject[] MiniStPatricks = GameObject.FindGameObjectsWithTag("MiniBoss");
                foreach (GameObject MiniStPatrick in MiniStPatricks)
                {
                    Destroy(MiniStPatrick);
                }
                Destroy(_realStPatrick.gameObject);
                lazerBeam.SetActive(false);
                StPatrickBoss.SetActive(true);
            }
        }
        if(_fakeStPatricks.Count <= 0)
        {
            GameObject[] fakeStPatricks = GameObject.FindGameObjectsWithTag("MiniBoss");
            foreach (GameObject fakeStPatrick in fakeStPatricks)
            {
                //fakeStPatrick.AddComponent<StPatrickExplosion>();
            }
        }
        else
        {
            for (int i = _fakeStPatricks.Count - 1; i >= 0; i--)
            {
                GameObject MiniStPatrick = _fakeStPatricks[i];

                if (MiniStPatrick.activeSelf == false)
                {
                    int spawnCount = UnityEngine.Random.Range(Mathf.RoundToInt(rangeOfGold.x), Mathf.RoundToInt(rangeOfGold.y));

                    for (int j = 1; j <= spawnCount; j++)
                    {
                        Instantiate(gold,
                            new Vector2(MiniStPatrick.transform.position.x, MiniStPatrick.transform.position.y),
                            Quaternion.Euler(Quaternion.identity.x, Quaternion.identity.y, UnityEngine.Random.Range(rangeOfAngle, -rangeOfAngle))
                        );
                    }

                    RemoveGameObject(MiniStPatrick);
                }
            }
        }
            
    }

    public void AddGameObject(GameObject FakeClone)
    {
        _fakeStPatricks.Add(FakeClone);
    }

    public void RemoveGameObject(GameObject FakeClone)
    {
        Debug.Log("Fake StPatrick is dead");    
        _fakeStPatricks.Remove(FakeClone);
        FakeClone.SetActive(false);
        //Destroy(FakeClone);
    }
}