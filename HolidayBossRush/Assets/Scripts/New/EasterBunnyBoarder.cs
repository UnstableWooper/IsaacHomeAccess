using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasterBunnyBoarder : MonoBehaviour
{
    [SerializeField] private GameObject[] fallingEggBoarder;
    [SerializeField] private GameObject attackWarnBoarder;

    private void Start()
    {
        StartCoroutine("StartAttack");
    }

    private IEnumerator StartAttack()
    {
        Instantiate(attackWarnBoarder, new Vector2(-10, -1.25f), Quaternion.identity);
        Instantiate(fallingEggBoarder[Random.RandomRange(1,fallingEggBoarder.Length)], new Vector2(-10, 10), Quaternion.identity);
        Instantiate(attackWarnBoarder, new Vector2(10, -1.25f), Quaternion.identity);
        Instantiate(fallingEggBoarder[Random.RandomRange(1, fallingEggBoarder.Length)], new Vector2(10, 10), Quaternion.identity);

        yield return new WaitForSeconds(5f);
        StartCoroutine("StartAttack");
    }
}
