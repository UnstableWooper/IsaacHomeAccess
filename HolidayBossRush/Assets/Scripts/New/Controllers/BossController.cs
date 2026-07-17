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

    [SerializeField] private bool miniBoss;

    public bool SecondPhase;

    public GameObject _player;

    private Animator _animator;

    private BossHP _bossHealth;
    public Color OgColor { private set; get; }
    public float attackCooldownTimer;
    

    public bool grounded;
    public bool bounceOffWall;
    public bool faceTowardPlayer;

    private Rigidbody2D _rigidbody;

    [SerializeField] public bool hardMode;

    [Header("DialogueStruct stuff?")]

    [TextArea(3, 10)]
    [SerializeField]private string[] Dialogue;
    [TextArea (3, 10)]
    [SerializeField]private string[] RepeatDialogue;
    [SerializeField]private float dialogueTypingSpeed;
    
    public GameObject DialogueImage;
    public TextMeshProUGUI dialogueTxt;

    public float trueDialogueTypingSpeed;
    private int dialogueCounter;
    private bool stillTyping;
    private bool inDialogue;

    private string repeat_dialogue;
    private static int _how_many_times_fought;
    private bool _fighting_again;
    private bool _end_fighting_again_dialogue;

    public bool DontDoDialouge;

    private void Awake()
    {
        _player = FindAnyObjectByType<newPlayerMovement>().gameObject;
        _bossHealth = GetComponent<BossHP>();
        _rigidbody = GetComponent<Rigidbody2D>();
        dialogueCounter = -1;

        if(!DontDoDialouge) inDialogue = true;

        OgColor = Color.white;

        _animator = gameObject.GetComponent<Animator>();
    }

    private void Start()
    {

        if (!DontDoDialouge)
        {
            DialogueImage.gameObject.SetActive(true);
            StartDialogue();
        }

        if (hardMode)
        {
            _bossHealth.TrueBossHp = Mathf.RoundToInt(_bossHealth.TrueBossHp * 1.5f);
            attackCooldown = attackCooldown * 0.5f;
            if(_animator != null)
                _animator.speed = 1.5f;
        }

        attackCooldownTimer =  hardMode ? 4 : 8;
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

        if (Input.GetKey(KeyCode.RightShift))
        {
            dialogueCounter = Dialogue.Length;
        }


        if (inDialogue && Input.GetKeyDown(KeyCode.Space))
        {
            StartDialogue();
        }
        else
        {
            attackCooldownTimer -= Time.deltaTime;

            if (!miniBoss)
            {
                if (_bossHealth.TrueBossHp <= _bossHealth.maxHP / 2)
                {
                    if (gameObject.CompareTag("Boss"))
                    {
                        SecondPhase = true;
                    }
                }
            }
        }

        if (faceTowardPlayer)
        {
            if (transform.position.x < _player.transform.position.x & grounded)
            {
                spriteRenderer.flipX = true;
            }
            else if (grounded)
            {
                spriteRenderer.flipX = false;
            }
        }   
    }

    public void StartDialogue()
    {
        if (!stillTyping)
        {
            dialogueCounter++;
            StartCoroutine(NextDialogue());
            if (_fighting_again)
            {
                _end_fighting_again_dialogue = true;
            }
        }
    }

    public IEnumerator NextDialogue()
    {
        #region data for dialogue
        newPlayerMovement playerMovement = _player.GetComponent<newPlayerMovement>();
        playerMovement.enabled = false;
        PlayerGun playerGun = _player.GetComponent<PlayerGun>();
        playerGun.enabled = false;
        #endregion

        if (_how_many_times_fought >= 1)
        {
            _fighting_again = true;
            dialogueTxt.text = "";
            stillTyping = true;
            int rand = Random.Range(0, RepeatDialogue.Length);
            repeat_dialogue = RepeatDialogue[rand];
            repeat_dialogue = repeat_dialogue.Replace("(attempt_count)", _how_many_times_fought.ToString());

            if (_end_fighting_again_dialogue)
            {
                playerMovement.enabled = true;
                playerGun.enabled = true;
                DialogueImage.gameObject.SetActive(false);
                inDialogue = false;
                _how_many_times_fought++;
                yield return new WaitForSeconds(5f);
                StartCoroutine(Attack());
                _end_fighting_again_dialogue = false;
            }
            else
            {
                foreach (char DialogueCharacters in repeat_dialogue)
                {
                    yield return new WaitForSeconds(trueDialogueTypingSpeed);
                    dialogueTxt.text += DialogueCharacters.ToString();
                }
            }
            stillTyping = false;
        }
        else
        {
            #region dialogue
            dialogueTxt.text = "";
            stillTyping = true;

            if (dialogueCounter >= Dialogue.Length)
            {
                playerMovement.enabled = true;
                playerGun.enabled = true;
                DialogueImage.gameObject.SetActive(false);
                inDialogue = false;
                _how_many_times_fought++;
                StartCoroutine(Attack());
            }
            else
            {
                foreach (char DialogueCharacters in Dialogue[dialogueCounter])
                {
                    yield return new WaitForSeconds(trueDialogueTypingSpeed);
                    dialogueTxt.text += DialogueCharacters.ToString();
                }
            }
            stillTyping = false;
            #endregion
        }
    }

    private void OnEnable()
    {
        if(!inDialogue)
            StartCoroutine(Attack());

        attackCooldownTimer = 5;
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

        yield return new WaitUntil(() => attackCooldownTimer <= 0);
        attackCooldownTimer = attackCooldown;
        StartCoroutine(Attack());
    }

    public void ResetAttemptCounter()
    {
        _how_many_times_fought = 0;
    }

    public IEnumerator DamageIndicatorCaller()
    {
        for (int i = 1; i < 2; i++)
        {
            spriteRenderer.color = Color.gray;
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
                spriteRenderer.flipX = spriteRenderer.flipX ? false : true;
                float velocityX = _rigidbody.velocity.x;     
                _rigidbody.velocity = new Vector2(-velocityX, _rigidbody.velocity.y);
            }
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        grounded = false;
    }


    public void AttackWarn(Color color)
    {
        spriteRenderer.color = color;
        OgColor = color;
    }
    
    public void AttackCooldownAddTimer(float cooldown)
    {
        attackCooldownTimer += cooldown;
    }   
}