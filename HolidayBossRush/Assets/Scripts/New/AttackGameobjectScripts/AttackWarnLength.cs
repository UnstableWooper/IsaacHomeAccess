using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackWarnLength : MonoBehaviour
{
    [SerializeField, Range(0, 3)] private float DespawnTimer;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Despawn());
    }

    IEnumerator Despawn()
    {
        yield return new WaitForSeconds(DespawnTimer);
        Destroy(gameObject);
    }
}
