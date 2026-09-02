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

    private bool _holding;

    private void Start()
    {
        
    }
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
            Projectile.ShootProjectile(shootPosition, bulletSpeed, worldMousePos);
        }

        CalculateArcVelocity(transform.position, worldMousePos, 20);
    }

    Vector3 CalculateArcVelocity(Vector3 start, Vector3 target, float angle)
    {
        Vector3 dir = target - start;
        float h = dir.y;
        dir.y = 0;
        float x = dir.magnitude;

        float alpha = angle * Mathf.Deg2Rad;
        float g = Mathf.Abs(Physics2D.gravity.y);


        float rootTerm = (g * x * x) / (2 * Mathf.Pow(Mathf.Cos(alpha), 2) * (x * Mathf.Tan(alpha) - h));

        if (rootTerm <= 0)
        {
            return Vector3.zero;
        }

        float v = Mathf.Sqrt(rootTerm);

        // Reconstruct the 2D velocity vector combining horizontal and vertical parts
        Vector3 velocity = dir.normalized * v * Mathf.Cos(alpha);
        velocity.y = v * Mathf.Sin(alpha);

        return velocity;
    }
}
