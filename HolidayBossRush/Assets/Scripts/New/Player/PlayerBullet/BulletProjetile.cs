using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProjectile : MonoBehaviour
{
    [SerializeField] private GameObject effect;
    [SerializeField] private GameObject bullet;

    [SerializeField] private float launchAngle;

    public int projectileDamage = 1;

    private float _speed;

    private Rigidbody2D _rigidbody2D;

    private Vector3 _shootPoint;
    private Vector3 _targetPosition;

    private float _distance;
    private float _archHeight;

    private float _startTime;

    private bool Stop;

    public void StartProjectile(Vector3 MousePos, float Speed, float ArchHeight, float Accuracy)
    {
        Stop = false;
        _archHeight = ArchHeight;
        _speed = Speed;
        _shootPoint = transform.position;
        _targetPosition = new Vector3(MousePos.x + UnityEngine.Random.Range(-Accuracy, Accuracy), MousePos.y + UnityEngine.Random.Range(-Accuracy, Accuracy),MousePos.z);
        _distance = Vector3.Distance(_shootPoint, _targetPosition);
        _startTime = Time.time;
    }

    private void Update()
    {
        float timeDistance = (Time.time - _startTime) * _speed / _distance;

        Vector3 direction = (_targetPosition - _shootPoint).normalized;
        Vector3 currentPos = _shootPoint + direction * (_distance * timeDistance);

        float height = _archHeight * 4 * timeDistance * (1 - timeDistance);
        currentPos.y += height;

        if(!Stop) transform.position = currentPos;
    }

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        
        effect.SetActive(false);
        bullet.SetActive(true);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            DestroyBullet(0);
        }
    }

    public void DestroyBullet(int AddHits)
    {
        PlayerGun playerGun = FindAnyObjectByType<PlayerGun>();
        playerGun.Hits(AddHits);

        Stop = true;
        _rigidbody2D.constraints = RigidbodyConstraints2D.FreezePosition;
        effect.SetActive(true);
        bullet.SetActive(false);
        gameObject.tag = "Untagged";
        Destroy(gameObject, 1);
    }
}
