using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class newPlayerHealth : MonoBehaviour
{

    [SerializeField] private Image healthBar;
    [SerializeField] private GameObject loseSprite;
    [SerializeField] private GameObject winSprite;
    [SerializeField] private Vector2 knockbackForce;
    [SerializeField] private float iFrames;
    [SerializeField] private float health;


    [SerializeField] bool imortal;

    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rigidbody;

    private float _iFramesTimer;
    private Vector2 _velocity;

    private void Update()
    {
        _iFramesTimer -= Time.deltaTime;

        healthBar.fillAmount = health / 5;
        healthBar.color = new Color(1 - (health / 5), 0 + (health / 5), 0);
    }
    private void Start()
    {
        winSprite.SetActive(false);
        loseSprite.SetActive(false);
        _rigidbody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Damage(int Damage, Vector3 otherPos)
    {
        if(_iFramesTimer <= 0)
        {
            health -= Damage;
            _iFramesTimer = iFrames;
            _velocity = _rigidbody.velocity;
            if (otherPos.x < transform.position.x) _velocity = new Vector2(knockbackForce.x, knockbackForce.y);
            else _velocity = new Vector2(-knockbackForce.x, knockbackForce.y);
            _rigidbody.velocity = _velocity;
            if (health > 0)
            {
                StartCoroutine(DamageDisplay());
            }
            else if(!imortal)
            {
                lose();
            }
        }
    }

    private IEnumerator DamageDisplay()
    {
        while(_iFramesTimer >= 0)
        {
            _spriteRenderer.color = Color.gray;
            yield return new WaitForSeconds(0.175f);
            _spriteRenderer.color = Color.white;
            yield return new WaitForSeconds(0.175f);
        }
    }

    private void lose()
    {
        loseSprite.SetActive(true);
        Destroy(gameObject);
    }

    public void win()
    {
        winSprite.SetActive(true);
        Destroy(gameObject);
    }
}
