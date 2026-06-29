using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class BossGroundChecker : MonoBehaviour
{
    [SerializeField] private Transform pos1, pos2;


    private BossController _bossController;
    public LayerMask LayerMask;

    private void Start()
    {
        _bossController = GetComponent<BossController>();
    }

    private void Update()
    {
        RaycastHit2D hit = Physics2D.Linecast(pos1.position, pos2.position, LayerMask);

        Debug.DrawLine(pos1.position, pos2.position, UnityEngine.Color.red, 10000);

        if (!hit)
        {
            return;
        }
        else if(hit.collider.gameObject.CompareTag("Ground"))
        {
            //_bossController.grounded = true;
            Debug.Log("On ground");
        }
    }

}
