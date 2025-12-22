using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TrappedGrub : MonoBehaviour
{
    public int grubsCollected;

    public TextMeshProUGUI grubCount;
    // Start is called before the first frame update
    void Start()
    {
        grubsCollected = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Attack"))
        {
            grubsCollected += 1;
            Destroy(gameObject);
            grubCount.text = "You have collected: " + grubsCollected.ToString() + " Grub";
        }
    }
}
