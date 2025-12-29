using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class easterBunnyController : MonoBehaviour
{
    [SerializeField] private AttackData[] attacks;

    [SerializeField] private int jumpsBeforeAttack;
    [SerializeField] public SpriteRenderer spriteRenderer;
    private BossHP _bossHealth;
    private Color _ogColor;
    public float AttackCooldownTimer { set; get; }
    public bool SecondPhase { private set; get; }
    // Start is called before the first frame update

    private void Awake()
    {
        _bossHealth = GetComponent<BossHP>();
        _ogColor = spriteRenderer.color;
    }
    void Start()
    {
        
    }
}
