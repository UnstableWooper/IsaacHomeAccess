using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItems : MonoBehaviour
{
    public GameObject playerOnBench;
    public GameObject player;
    public GameObject attackUpgrader1;
    public GameObject attackUpgrader2;
    public GameObject clawUpgrader;
    public GameObject dashUpgrader;
    public GameObject mapItem;
    public GameObject bench;
    private GameObject key;

    public GameObject map;

    public TMPro.TextMeshProUGUI healthLable;

    private bool collideWithDashUpgrade;
    private bool collideWithBench;
    private bool collideWithClawUpgrade;
    public bool collideWithMap;
    private bool collideWithAttackUpgrade1;
    private bool collideWithAttackUpgrade2;
    private bool collideWithKey;

    public int keys;
    public bool usingBench;
    public bool usingMap;
    private bool hasMap;
    public bool clawUpgrade;
    public bool dashUpgrade;

    public int attackDamage = 2;
    public int takeDamage;

    public int healthPoints;
    public int healthPointsMax;

    public float invaluability;

    public bool checkMap;
    public bool checkSitting;

    public Vector2 playerPosBeforBench;

    public BoxCollider2D playerCollider;
    public PlayerMovement playerScript;
    public SpriteRenderer playerColor;
    // Start is called before the first frame update
    void Start()
    {
        healthPoints = healthPointsMax;

        playerColor.enabled = true;
        playerOnBench.SetActive(false);
        hasMap = false;
        clawUpgrade = false;

        playerCollider = this.GetComponent<BoxCollider2D>();
        playerColor = this.GetComponent<SpriteRenderer>();
        playerScript = this.GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        healthLable.text = healthPoints.ToString();
        if (Input.GetKeyDown(KeyCode.UpArrow) && collideWithDashUpgrade)
        {
            dashUpgrade = true;
            Destroy(dashUpgrader);
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) && collideWithKey)
        {
            keys++;
            Destroy(key);
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) && collideWithClawUpgrade)
        {
            clawUpgrade = true;
            Destroy(clawUpgrader);
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) && collideWithClawUpgrade)
        {
            clawUpgrade = true;
            Destroy(clawUpgrader);
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) && collideWithMap)
        {
            hasMap = true;
            Destroy(mapItem);
        }

        if (Input.GetKeyDown(KeyCode.M) && hasMap && !checkMap)
        {
            checkMap = true;
            usingMap = true;
            map.SetActive(true);
        }
        else if
        (Input.GetKeyDown(KeyCode.M) && hasMap && checkMap)
        {
            checkMap = false;
            usingMap = false;
            map.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) && collideWithAttackUpgrade1 && attackDamage == 2)
        {
            attackDamage = 4;
            Destroy(attackUpgrader1);
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) && collideWithAttackUpgrade2 && attackDamage == 4)
        {
            attackDamage = 7;
            Destroy(attackUpgrader2);
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) && !checkSitting && collideWithBench)
        {
            playerPosBeforBench = new Vector2(player.transform.position.x, player.transform.position.y);
            playerOnBench.SetActive(true);
            playerCollider.enabled = false;
            playerColor.enabled = false;
            playerScript.enabled = false;
            usingBench = true;
            checkSitting = true;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow) && checkSitting)
        {
            player.transform.position = playerPosBeforBench;
            playerOnBench.SetActive(false);
            playerCollider.enabled = true;
            playerColor.enabled = true;
            playerScript.enabled = true;
            usingBench = false;
            checkSitting = false;
        }
        player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -0.1f);

        invaluability -= Time.deltaTime;
        if (invaluability <= 0)
        {

            playerColor.color = Color.black;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == dashUpgrader)
        {
            collideWithDashUpgrade = true;
        }

        if (other.CompareTag("Key")) 
        {
            collideWithKey = true;
            key = other.gameObject;
        }

        if (other.gameObject == bench)
        {
            collideWithBench = true;
        }

        if (other.gameObject == attackUpgrader1)
        {
            collideWithAttackUpgrade1 = true;
        }

        if (other.gameObject == attackUpgrader2)
        {
            collideWithAttackUpgrade2 = true;
        }

        if (other.gameObject == clawUpgrader)
        {
            collideWithClawUpgrade = true;
        }

        if (other.gameObject == mapItem)
        {
            collideWithMap = true;
        }

        if (other.CompareTag("WeakEnemy") && invaluability <= 0)
        {
            healthPoints -= 1;
            invaluability = 2;

            playerColor.color = Color.red;

            if (healthPoints <= 0)
            {
                Dead();
            }
        }
        if (other.CompareTag("StrongEnemy") && invaluability <= 0)
        {
            healthPoints -= 2;
            invaluability = 2;

            playerColor.color = Color.magenta;

            if (healthPoints <= 0)
            {
                Dead();
            }
        }
    }
    private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Key"))
            {
                collideWithKey = false;
            }

            if (other.gameObject == dashUpgrader)
            {
                collideWithDashUpgrade = false;
            }

            if (other.gameObject == bench)
            {
                collideWithBench = false;
            }

            if (other.gameObject == clawUpgrader)
            {
                collideWithClawUpgrade = false;
            }

            if (other.gameObject == attackUpgrader1)
            {
                collideWithAttackUpgrade1 = false;
            }

            if (other.gameObject == attackUpgrader2)
            {
                collideWithAttackUpgrade2 = false;
            }

            if (other.gameObject == mapItem)
            {
                collideWithMap = false;
            }
        }

    public void Dead()
    {
        player.SetActive(false);
        Respawn();
    }
    public void Respawn()
    {
        player.SetActive(true);
        player.transform.position = playerPosBeforBench;
        healthPoints = healthPointsMax;
    }
}
