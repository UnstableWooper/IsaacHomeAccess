using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossHP : MonoBehaviour
{

    [SerializeField, Range(1, 1000)]public int maxHP;
    private PlayerProjectile _bulletProjectile;
    private BossController _brain;

    public int TrueBossHp;

    private void Awake()
    {
        _brain = GetComponent<BossController>();
    }
    
    private void Start()
    {
        TrueBossHp = maxHP;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Projectile"))
        {
            _bulletProjectile = other.GetComponent<PlayerProjectile>();
            TrueBossHp -= _bulletProjectile.projectileDamage;
            _bulletProjectile.DestroyBullet();
            _brain.StartCoroutine(nameof(BossController.DamageIndicatorCaller));
            if (TrueBossHp <= 0)
            {
               gameObject.SetActive(false);
                _brain.ResetAttemptCounter();
                Invoke(nameof(BossDefeated), 3);
            }
        }
    }


    private void BossDefeated()
    {
        if (gameObject.CompareTag("Boss"))
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            newPlayerHealth playerHPScript = player.GetComponent<newPlayerHealth>();
            playerHPScript.win();
        }
    }
}
