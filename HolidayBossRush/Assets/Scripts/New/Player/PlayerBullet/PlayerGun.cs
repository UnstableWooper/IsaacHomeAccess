using UnityEngine;

public class PlayerGun : MonoBehaviour
{

    [SerializeField] private float cooldown;
    [SerializeField] private float bulletSpeed;

    [Header("OtherSettings")]

    [SerializeField] private GameObject bullet;
    [SerializeField] Transform shootPoint;

    [SerializeField] private Animator playerAnimator;

    private float _attackCooldown;


    private void Start()
    {
        
    }
    void Update()
    {
        _attackCooldown -= Time.deltaTime;

        if(Input.GetButtonDown("Shoot") & _attackCooldown < 0)
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        playerAnimator.SetTrigger("Attacked");
        _attackCooldown = cooldown;

        Vector3 MousePos = Input.mousePosition;
        Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(MousePos);

        worldMousePos.z = 0;

        Vector2 shootPosition = (Vector2)(worldMousePos - shootPoint.position);

        GameObject Bullet = Instantiate(bullet, shootPoint.position, Quaternion.identity);
        BulletProjectile Projectile = Bullet.GetComponent<BulletProjectile>();

        if (Projectile != null)
        {
            Projectile.ShootProjectile(shootPosition, bulletSpeed);
        }
    }
}
