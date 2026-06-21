using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerAim : MonoBehaviour
{
    private BossController _controller;
    private SpriteRenderer _spriteRenderer;
    
    [SerializeField]private GameObject lazerBeam;
    [SerializeField]private RainbowLazerbeam rainbowLazerBeamScript;
    [SerializeField] private float rotationSpeed;
    
    [SerializeField] private Sprite idleAnimation;
    [SerializeField] private Sprite attackAnimation;

    public SpriteRenderer spriteRenderer;

    private GameObject _player;
    public void Attack()
    {
        _controller = GetComponentInParent<BossController>();
        _spriteRenderer = _controller.spriteRenderer;
        _player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(nameof(ShootLazer));
    }
    IEnumerator ShootLazer()
    {
        Vector3 direction = rainbowLazerBeamScript.ShootPos - transform.position;
        spriteRenderer.sprite = attackAnimation;
        //_controller.AttackWarn(Color.red);
        yield return new WaitForSeconds(rainbowLazerBeamScript.TrueAttackWarnLength);
        lazerBeam.SetActive(true);
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        yield return new WaitForSeconds(rainbowLazerBeamScript.attackLength);
        lazerBeam.SetActive(false);
        spriteRenderer.sprite = idleAnimation;
        //_controller.AttackWarn(Color.white);
    }
}
