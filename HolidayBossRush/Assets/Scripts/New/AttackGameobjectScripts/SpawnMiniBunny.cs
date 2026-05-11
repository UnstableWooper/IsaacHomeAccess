using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMiniBunny : MonoBehaviour
{
    [SerializeField] public float TimeBeforeHatch;

    [SerializeField] public GameObject miniBunny;

    [SerializeField] private SpriteRenderer sp;
    [SerializeField] private Sprite[] sprites;
    void Start()
    {
        StartCoroutine("SpawnBunny");
        sp = GetComponent<SpriteRenderer>();
        sp.sprite = sprites[Random.Range(0, sprites.Length)];
    }

IEnumerator SpawnBunny()
    {
        yield return new WaitForSeconds(TimeBeforeHatch);
        Instantiate(miniBunny, new Vector2(transform.position.x, transform.position.y + .5f) , Quaternion.identity);
        Destroy(gameObject);
    }
}
