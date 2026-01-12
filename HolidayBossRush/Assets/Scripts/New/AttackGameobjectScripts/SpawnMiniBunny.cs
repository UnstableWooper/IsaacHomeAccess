using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMiniBunny : MonoBehaviour
{
    [SerializeField] public float TimeBeforeHatch;

    [SerializeField] public GameObject miniBunny;
    void Start()
    {
        StartCoroutine("SpawnBunny");
    }

    IEnumerator SpawnBunny()
    {
        yield return new WaitForSeconds(TimeBeforeHatch);
        Instantiate(miniBunny, transform.position , Quaternion.identity);
        Destroy(gameObject);
    }
}
