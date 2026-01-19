using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniEasterBunnyJumpAttack : MonoBehaviour
{
    [SerializeField] private Vector2 jumpStrength;
    [SerializeField] private int jumpCooldown;

    private GameObject _player;
    private Rigidbody2D _rigidbody;
    private void Start()
    {
        _player = FindAnyObjectByType<newPlayerMovement>().gameObject;
        _rigidbody = GetComponent<Rigidbody2D>();
        StartCoroutine(JumpAttack());
    }
    IEnumerator JumpAttack()
    {
        yield return new WaitForSeconds(jumpCooldown);
        _rigidbody.AddForce(new Vector2(
        _player.transform.position.x > transform.position.x ? jumpStrength.x * 1 * Random.Range(1.2f, 0.8f) : jumpStrength.x * -1 * Random.Range(1.2f, 0.8f)
        , jumpStrength.y), ForceMode2D.Impulse);
        StartCoroutine(JumpAttack());
    }
}
