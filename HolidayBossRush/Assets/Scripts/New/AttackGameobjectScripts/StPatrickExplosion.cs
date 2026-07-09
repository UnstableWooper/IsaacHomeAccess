using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StPatrickExplosion : MonoBehaviour
{
    private StPatrickCloneManager cloneManager;
    void Start()
    {
        //GameObject gameManager = GameObject.FindGameObjectWithTag("GameManager");
        //cloneManager = gameManager.GetComponent<StPatrickCloneManager>();
        //cloneManager.AddGameObject(this.gameObject);
    }

    private void Update()
    {
        if (this.gameObject.activeSelf == false)
        {
            //cloneManager.RemoveGameObject(this.gameObject);
            //Destroy(this);
        }
        
    }   
}
