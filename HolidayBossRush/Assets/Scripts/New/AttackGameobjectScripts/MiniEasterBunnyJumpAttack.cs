using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniEasterBunnyJumpAttack : MonoBehaviour
{
    [SerializeField] private Vector2 jumpStrength;
    [SerializeField] private int jumpCooldown;
    [SerializeField] private Animator animator;

    [SerializeField] private Sprite chargeJumpAnimation;

    private SpriteRenderer _spriteRenderer;
    private GameObject _player;
    private Rigidbody2D _rigidbody;

    private bool _grounded;
    private void Start()
    {
        _player = FindAnyObjectByType<newPlayerMovement>().gameObject;
        _rigidbody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(JumpAttack());
    }

    private void Update()
    {
        animator.SetFloat("yVelocity", _rigidbody.velocity.y);
        animator.SetBool("isGrounded", _grounded);
    }

    IEnumerator JumpAttack()
    {
        _spriteRenderer.sprite = chargeJumpAnimation;
        yield return new WaitForSeconds(jumpCooldown);
        if (_grounded) {
            _rigidbody.AddForce(new Vector2(
                _player.transform.position.x > transform.position.x ? jumpStrength.x * 1 * Random.Range(1.2f, 0.8f) : jumpStrength.x * -1 * Random.Range(1.2f, 0.8f)
                , jumpStrength.y), ForceMode2D.Impulse);
        }
        StartCoroutine(JumpAttack());
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            _grounded = true;
        }
        else
        {
            _grounded = false;
        }
    }
}
