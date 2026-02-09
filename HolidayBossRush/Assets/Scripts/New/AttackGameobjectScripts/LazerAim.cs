using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerAim : MonoBehaviour
{
    [SerializeField]private GameObject lazerBeam;
    [SerializeField]private RainbowLazerbeam rainbowLazerBeamScript;
    [SerializeField] private float rotationSpeed;

    private GameObject _player;
    public void Attack()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(nameof(ShootLazer));
    }

    IEnumerator ShootLazer()
    {
        Vector3 direction = rainbowLazerBeamScript.ShootPos - transform.position;
        yield return new WaitForSeconds(rainbowLazerBeamScript.TrueAttackWarnLength);
        lazerBeam.SetActive(true);
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        yield return new WaitForSeconds(rainbowLazerBeamScript.attackLength);
        lazerBeam.SetActive(false);
    }
}
