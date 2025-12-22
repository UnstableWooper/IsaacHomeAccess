using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public bool hasClaws;
    public bool hasDash;

    public GameObject cameraObject;
    private bool cameraLockX;
    private bool cameraLockY;

    public GameObject areaStart;
    public List<GameObject> areaOff = new List<GameObject>();

    public Rigidbody2D rb;
    public GameObject player;

    public int dashReset;
    public float dashLength;
    public float speed = 7;
    public float jumpHight = 12;
    public bool wallJump;
    public float wallJumpTimes;

    public int facing;
    public Vector3 direction;

    private RaycastHit2D hit;

    public bool onGround;
    public float jumpTimer;
    public bool startJump;

    // Start is called before the first frame update
    void Start()
    {
        cameraObject.GetComponent<Camera>().orthographicSize = 5f;

        foreach (GameObject areasOff in areaOff)
        {
            areasOff.SetActive(false);
        }
        areaStart.SetActive(true);
        
        cameraObject.transform.position = new Vector3(0,0, -10);
        transform.position = new Vector2(0,0);
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        hasDash = FindAnyObjectByType<PlayerItems>().clawUpgrade;
        hasClaws = FindAnyObjectByType<PlayerItems>().clawUpgrade;
        if (cameraLockY == true)
        {
            cameraObject.transform.position = new Vector3(cameraObject.transform.position.x, player.transform.position.y, -10);
        }
        if (cameraLockX == true)
        {
            cameraObject.transform.position = new Vector3(player.transform.position.x, cameraObject.transform.position.y, -10);
        }

        direction = FindAnyObjectByType<PlayerAttack>().direction;

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            facing = -1;

            transform.Translate(Vector2.left * Time.deltaTime * speed);

            transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);

        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            facing = 1;

            transform.Translate(Vector2.right * Time.deltaTime * speed);

            transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);

        }

        //        if (Input.GetKeyDown(KeyCode.Z) && onGround == true)         Jump();
        Jump();

        if (hasClaws == true)
        {
            if(Input.GetKeyDown(KeyCode.Z)) WallJump();
        }
        if (hasDash == true)
        {
            if (Input.GetKeyDown(KeyCode.C)) Dash();
        }
    }

    public void Dash()
    {
        if(dashReset >= 0)
        {
            Vector3 velocity = rb.velocity;
            velocity.x = dashLength * facing;
            rb.velocity = velocity;
            dashReset--;
        }
    }

    public void Jump()
    {
        hit = Physics2D.CircleCast(rb.position, 1f, Vector2.down, LayerMask.GetMask("Ground"));
        if(hit.collider != null)
        {
            if(Input.GetKeyDown(KeyCode.Z) && onGround == true)
            {
                Vector3 velocity = rb.velocity;
                velocity.y = jumpHight;
                rb.velocity = velocity;
                startJump = true;
                Invoke("ResetJump", 0.5f);
            }
            dashReset = 1;
        }
        if (startJump && Input.GetKey(KeyCode.Z))
        {
            Vector3 velocity = rb.velocity;
            velocity.y = jumpHight;
            rb.velocity = velocity;
        }
    }

    public void WallJump()
    {
        if (Input.GetKeyDown(KeyCode.Z) && wallJump == true && wallJumpTimes >= 1)
        {
            wallJumpTimes--;
            Vector3 velocity = rb.velocity;
            velocity.y = jumpHight * 1;
            velocity.x = 2 * facing * -1;
            rb.velocity = velocity;
        }
    }

    public void ResetJump()
    {
        startJump = false;
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Ground"))
        {
            wallJumpTimes = 10;
            onGround = true;
            jumpTimer = 0;
        }

        if (other.collider.CompareTag("Wall"))
        {
            wallJump = true;
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.collider.CompareTag("Ground"))
        {
            onGround = true;
            jumpTimer = 0;
        }

    }
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.collider.CompareTag("Ground"))
        {
            onGround = false;
        }

        if (other.collider.CompareTag("Wall"))
        {
            wallJump = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("CameraTriggerPosX"))
        {
            cameraLockX = true;
        }

        if (other.CompareTag("CameraTriggerPosY"))
        {
            cameraLockY = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {

        if (other.CompareTag("CameraTriggerPosX"))
        {
            cameraLockX = false;
        }

        if (other.CompareTag("CameraTriggerPosY"))
        {
            cameraLockY = false;
        }
    }
}
