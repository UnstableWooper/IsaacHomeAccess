using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class newPlayerHealth : MonoBehaviour
{

    [SerializeField] private Image healthBar;
    [SerializeField] private GameObject loseSprite;
    [SerializeField] private TMP_Text loseText;
    [SerializeField] private GameObject winSprite;
    [SerializeField] private Vector2 knockbackForce;
    [SerializeField] private float iFrames;
    [SerializeField] private float health;


    [SerializeField] bool imortal;

    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rigidbody;

    private float _iFramesTimer;
    private Vector2 _velocity;

    public Damage GameObjectDamage { get; set; }

    private void Update()
    {
        _iFramesTimer -= Time.deltaTime;

        healthBar.fillAmount = health / 5;
        healthBar.color = new Color(1 - (health / 5), 0 + (health / 5), 0);


        
        if (GameObjectDamage != null)
        {
            if (GameObjectDamage.collidingPlayer)
            {
                TakeDamage(GameObjectDamage.damage, GameObjectDamage.gameObject);
                if (GameObjectDamage.onCollionDestroy)
                    Destroy(GameObjectDamage.gameObject);
            }
        }

    }
    private void Start()
    {
        winSprite.SetActive(false);
        loseSprite.SetActive(false);
        _rigidbody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(int DamageDelt, GameObject gameobject)
    {
        Debug.Log("hit");

        if(_iFramesTimer < 0 )//&& gameObjectDamage.canDamage)
        {
            health -= DamageDelt;

            _iFramesTimer = iFrames;

            _velocity = _rigidbody.velocity;
            if (gameobject.transform.position.x < transform.position.x)
            {
                _velocity = new Vector2(knockbackForce.x, knockbackForce.y);
            }
            else if (gameobject.transform.position.x > transform.position.x)
            {
                _velocity = new Vector2(-knockbackForce.x, knockbackForce.y);
            }
            else
            {
                _velocity = new Vector2(0, knockbackForce.y);
            }
            _rigidbody.AddForce(_velocity, ForceMode2D.Impulse);
            StartCoroutine("FreezeVelocity");


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

    IEnumerator FreezeVelocity()
    {
        newPlayerMovement playerMovement = gameObject.GetComponent<newPlayerMovement>();
        playerMovement.FreezeVelocity = true;
        yield return new WaitForSeconds(0.2f);
        playerMovement.FreezeVelocity = false;
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

        loseText.text = 
        Destroy(gameObject);
    }

    public void win()
    {
        winSprite.SetActive(true);
        Destroy(gameObject);
    }
}
