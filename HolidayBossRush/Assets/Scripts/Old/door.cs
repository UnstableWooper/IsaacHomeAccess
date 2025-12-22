using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class door : MonoBehaviour
{
    public GameObject player;
    public GameObject areaOn;
    public List<GameObject> areaOff = new List<GameObject>();

    public GameObject cameraObject;
    public GameObject map;
    public List<GameObject> lastMapPin = new List<GameObject>();
    public GameObject mapPin;

    public float playerPosX;
    public float playerPosY;

    public float cameraPosX;
    public float cameraPosY;
    public float cameraSize;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            foreach (GameObject areasOff in areaOff) areasOff.SetActive(false);
            foreach (GameObject lastMapPin in lastMapPin) lastMapPin.SetActive(false);

            areaOn.SetActive(true);
            mapPin.SetActive(true);

            player.transform.position = new Vector2(playerPosX, playerPosY);
            cameraObject.transform.position = new Vector3(cameraPosX, cameraPosY, -10);
            cameraObject.GetComponent<Camera>().orthographicSize = cameraSize;
        }
    }
}

