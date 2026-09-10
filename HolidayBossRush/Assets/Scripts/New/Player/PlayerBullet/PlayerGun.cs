using UnityEngine;

public class PlayerGun : MonoBehaviour
{

    [SerializeField] private float cooldown;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float archHeight;

    [Header("OtherSettings")]

    [SerializeField] private GameObject bullet;

    [SerializeField] private Animator playerAnimator;

    private Vector3 _shootPoint;
    private Vector3 _targetPosition;

    private float _attackCooldown;

    private bool _holding;

    void Update()
    {
        _attackCooldown -= Time.deltaTime;

        if (Input.GetButtonDown("Shoot") & _attackCooldown < 0)
        {
            Shoot();
            _holding = true;
        }
        else if (Input.GetButtonUp("Shoot"))
            _holding = false;


        if (_holding && _attackCooldown < 0 - 0.25f)
            Shoot();
    }

    private void Shoot()
    {
        _attackCooldown = cooldown;

        playerAnimator.SetTrigger("Attacked");

        Vector3 MousePos = Input.mousePosition;
        Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(MousePos);

        worldMousePos.z = 0;

        GameObject BulletSpawn = Instantiate(bullet, transform.position, Quaternion.identity);

        BulletProjectile BulletSpawnProjectile = BulletSpawn.GetComponent<BulletProjectile>();

        BulletSpawnProjectile.StartProjectile(worldMousePos, bulletSpeed, archHeight);
    }
}
