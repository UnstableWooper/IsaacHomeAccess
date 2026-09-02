using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossHP : MonoBehaviour
{

    [SerializeField, Range(1, 1000)]public int maxHP;

    [SerializeField] private bool DontDestroy;
    [SerializeField] private bool immortal;

    private BulletProjectile _projectile;
    private BossController _brain;

    public int trueBossHp;
    private void Start()
    {
        _brain = GetComponent<BossController>();
        trueBossHp = maxHP;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Projectile"))
        {
            _brain = GetComponent<BossController>();
            _projectile = other.GetComponent<BulletProjectile>();
            trueBossHp -= _projectile.projectileDamage;
            _projectile.DestroyBullet();
            _brain.StartCoroutine("DamageIndicatorCaller");
            if (trueBossHp <= 0 && !immortal)
            {
                if (!DontDestroy && !gameObject.CompareTag("Boss"))
                    Destroy(gameObject);
                else if(!gameObject.CompareTag("Boss"))
                    gameObject.SetActive(false);
                else
                {                
                    DeathAnimation deathAnimation = GetComponent<DeathAnimation>();
                    deathAnimation.Death();
                    Invoke(nameof(BossDefeated), 3);
                    _brain.ResetAttemptCounter();
                }
            }
        }
    }


    private void BossDefeated()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        newPlayerHealth playerHPScript = player.GetComponent<newPlayerHealth>();
        playerHPScript.win();
    }
}
