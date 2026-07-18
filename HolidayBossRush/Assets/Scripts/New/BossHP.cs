using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossHP : MonoBehaviour
{

    [SerializeField, Range(1, 1000)]public int maxHP;

    [SerializeField] private bool DontDestroy;

    private BulletProjectile _projectile;
    private BossController _brain;

    public int TrueBossHp;
    private void Start()
    {
        _brain = GetComponent<BossController>();
        TrueBossHp = maxHP;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Projectile"))
        {
            _brain = GetComponent<BossController>();
            _projectile = other.GetComponent<BulletProjectile>();
            TrueBossHp -= _projectile.projectileDamage;
            _projectile.DestroyBullet();
            _brain.StartCoroutine("DamageIndicatorCaller");
            if (TrueBossHp <= 0)
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
        Debug.Log("WORKING");
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        newPlayerHealth playerHPScript = player.GetComponent<newPlayerHealth>();
        playerHPScript.win();
    }
}
