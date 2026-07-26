using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Delete : MonoBehaviour
{
    public float deleteDelay;
    void Start()
    {
        Invoke("DeleteGameObject", deleteDelay);
    }

    public void DeleteGameObject()
    {
        Destroy(gameObject);
    }
}
