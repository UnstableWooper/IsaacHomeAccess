using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BossController : MonoBehaviour
{
    [SerializeField] private AttackData[] attacks;

    [SerializeField] private float attackCooldown; //8
    [SerializeField] public SpriteRenderer spriteRenderer;

    private BossHP _bossHealth;
    public Color OgColor { private set; get; }
    public float AttackCooldownTimer { set; get; }
    public bool SecondPhase { private set; get; }

    public bool grounded;
    public bool bounceOffWall;

    private Rigidbody2D _rigidbody;
    //Shoot Attack
    //Roll Attack
    //Shockwave Attack

    [Header("Dialogue stuff?")]

    [SerializeField] private DialogueScript[] Dialogue;

    public Image DialogueImage;
    public TextMeshProUGUI dialogueTxt;
    private int dialogueCounter;    
    private bool inDialogue;
    private void Awake()
    {
        _bossHealth = GetComponent<BossHP>();
        OgColor = spriteRenderer.color;
        _rigidbody = GetComponent<Rigidbody2D>();
        inDialogue = true;
    }

    private void Start()
    {
        dialogueCounter = -1;
        DialogueImage.gameObject.SetActive(true);

    }
    private void Update()
    {
        if (inDialogue && Input.GetKeyDown(KeyCode.Space))
        {
            NextDialogue();
        }
        else
        {
            AttackCooldownTimer -= Time.deltaTime;
            if (_bossHealth.TrueBossHp <= _bossHealth.maxHP / 2)
            {
                if (gameObject.CompareTag("Boss"))
                {
                    SecondPhase = true;
                }
            }
        }


    }

    public void NextDialogue()
    {
        dialogueCounter++;
        if (dialogueCounter >= Dialogue.Length)
        {
            DialogueImage.gameObject.SetActive(false);
            inDialogue = false;
            StartCoroutine(Attack());
        }
        dialogueTxt.text = Dialogue[dialogueCounter].dialogue;
    }

    private void OnEnable()
    {
        if(!inDialogue)
            StartCoroutine(Attack());
    }
    IEnumerator Attack()
    {
        int totalChance = 0;
        #region PickRandomAttack
        foreach (AttackData Attack in attacks)
        {
            totalChance += Attack.randomChance;
        }
        int randChance = Random.Range(0, totalChance);
        int cumulativeChance = 0;

        foreach (AttackData Attack in attacks)
        {
            cumulativeChance += Attack.randomChance;
            if (randChance <= cumulativeChance)
            {
                Attack.attack.StartCoroutine("AttackWarn");
                break;
            }
        }

        #endregion

        yield return new WaitUntil(() => AttackCooldownTimer <= 0);
        AttackCooldownTimer = attackCooldown;
        StartCoroutine(Attack());
    }

    public IEnumerator DamageIndacatorCaller()
    {
        for (int i = 1; i < 2; i++)
        {
            spriteRenderer.color = Color.white;
            yield return new WaitForSeconds(0.175f);
            spriteRenderer.color = OgColor;
            yield return new WaitForSeconds(0.175f);
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ground"))
        {
            grounded = true;
        }

        if (other.CompareTag("Wall"))
        {
            if (bounceOffWall)
            {
                float velocityX = _rigidbody.velocity.x;     
                _rigidbody.velocity = new Vector2(-velocityX, _rigidbody.velocity.y);
            }
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        grounded = false;
    }
}