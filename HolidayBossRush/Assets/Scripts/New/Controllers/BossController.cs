using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BossController : MonoBehaviour
{
    
    [SerializeField] private AttackData[] attacks;

    [SerializeField] private float attackCooldown; //8
    [SerializeField] public SpriteRenderer spriteRenderer;

    private GameObject _player;

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

    [Header("DialogueStruct stuff?")]

    [SerializeField]private string[] Dialogue;
    [SerializeField]private float dialogueTypingSpeed;

    public GameObject DialogueImage;
    public TextMeshProUGUI dialogueTxt;

    public float trueDialogueTypingSpeed;
    private int dialogueCounter;
    private bool stillTyping;
    public bool inDialogue;
    private void Awake()
    {
        _player = FindAnyObjectByType<newPlayerMovement>().gameObject;
        _bossHealth = GetComponent<BossHP>();
        OgColor = spriteRenderer.color;
        _rigidbody = GetComponent<Rigidbody2D>();
        dialogueCounter = -1;
        inDialogue = true;
    }

    private void Start()
    {
        DialogueImage.gameObject.SetActive(true);
        StartDialogue();

    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            trueDialogueTypingSpeed = dialogueTypingSpeed / 2;
        }
        else
        {
            trueDialogueTypingSpeed = dialogueTypingSpeed;
        }

        if (inDialogue && Input.GetKeyDown(KeyCode.Space))
        {
            StartDialogue();
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

    public void StartDialogue()
    {
        if (!stillTyping)
        {
            dialogueCounter++;
            StartCoroutine(NextDialogue());
        }
    }

    public IEnumerator NextDialogue()
    {
        #region
        newPlayerMovement playerMovement = _player.GetComponent<newPlayerMovement>();
        playerMovement.enabled = false;
        PlayerGun playerGun = _player.GetComponent<PlayerGun>();
        playerGun.enabled = false;
        #endregion Player
        dialogueTxt.text = "";
        stillTyping = true;
        foreach (char DialogueCharacters in Dialogue[dialogueCounter])
        {
            yield return new WaitForSeconds(trueDialogueTypingSpeed);
            dialogueTxt.text += DialogueCharacters.ToString();
        }
        stillTyping = false;

        if (dialogueCounter >= Dialogue.Length)
        {
            playerMovement.enabled = true;
            playerGun.enabled = true;
            DialogueImage.gameObject.SetActive(false);
            inDialogue = false;
            StartCoroutine(Attack());
        }
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