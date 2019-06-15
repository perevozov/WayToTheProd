using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plr : MonoBehaviour
{
    public float Speed = 7f;
    public float MaxSpeed = 5f;
    public Animator Animator;
    public GameObject bugPrefab;

    private bool m_FacingRight = true;
    private bool jumping = false;
    private bool attack1Pressed = false;
    private bool attack2Pressed = false;
    private bool attack3Pressed = false;

    public float hMove = 0;

    public bool isGrounded = false;

    private float currentSpeed = 0f;
    private float verticalSpeed = 0f;

    private GameObject nearBug;
    private Rigidbody2D Rigidbody2D;

    private float generatorTimer = 1f;

    private float maxBugs = 4;
    private float spawnedBugs = 0;

    private bool isLocked = false;

    private GameObject[] hearts;

    private int currentHitPoints = 3;

    private bool isDying = false;


    // Start is called before the first frame update
    void Start()
    {
        hearts = GameObject.FindGameObjectsWithTag("heart");

        Rigidbody2D = GetComponent<Rigidbody2D>();

        StartCoroutine("CreateEnemy");
    }

    IEnumerator CreateEnemy()
    {
        float x = 0;
        while (spawnedBugs < maxBugs)
        {
            x = Random.Range(-12f, 6f);
            generatorTimer = Random.Range(1f, 5f);
            Instantiate(bugPrefab, new Vector3(x, 1f, 0), new Quaternion(0, 0, 180, 0));
            spawnedBugs++;
            yield return new WaitForSeconds(generatorTimer);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!isLocked)
        {
            hMove = Input.GetAxisRaw("Horizontal") * Speed;
         
            attack1Pressed = Input.GetKeyDown(KeyCode.Alpha1);
            attack2Pressed = Input.GetKeyDown(KeyCode.Alpha2);
            attack3Pressed = Input.GetKeyDown(KeyCode.Alpha3);
        }

        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, Vector2.down, 2f);

        bool collision = false;
        // If it hits something...
        foreach(RaycastHit2D hit in hits)
        {
            if(hit.collider.gameObject == gameObject) 
                continue;

            collision = true;
        }
        isGrounded = collision;

        if (isGrounded && Input.GetButtonDown("Jump") && !isLocked)
        {
            jumping = true;
        }

        currentSpeed = Mathf.Abs(Rigidbody2D.velocity.x);
        verticalSpeed = Rigidbody2D.velocity.y;

        Animator.SetFloat("Speed", currentSpeed);
        Animator.SetFloat("VertSpeed", verticalSpeed);
        Animator.SetBool("Jumping", (!isGrounded && Mathf.Abs(verticalSpeed) > 0.01));

    }

    private void FixedUpdate()
    {

        if (isGrounded)
        {
            if(jumping)
            {
                Rigidbody2D.AddForce(new Vector2(0, 120f));
                jumping = false;
            }

            if(currentSpeed < MaxSpeed)
            {
                Rigidbody2D.AddForce(new Vector2(hMove, 0));
            }
        }

        if (attack1Pressed && nearBug)
        {
            nearBug.SendMessage("OnPlayerAttack", 1);
        }
        else if (attack2Pressed && nearBug)
        {
            nearBug.SendMessage("OnPlayerAttack", 2);
        }
        else if (attack3Pressed && nearBug && isGrounded)
        {
            nearBug.SendMessage("OnPlayerAttack", 3);
            StartCoroutine(Fight());
        }


        if (hMove > 0 && !m_FacingRight)
        {
            Flip();
        }
        else if (hMove < 0 && m_FacingRight)
        {
            Flip();
        }
    }

    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        m_FacingRight = !m_FacingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.tag);

        if(collision.tag == "bug")
        {
            nearBug = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "bug")
        {
            if (nearBug == other.gameObject)
            {
                nearBug = null;
            }
        }
    }

    public void DecreaseHealth()
    {
        Debug.Log("Descrease health");


        currentHitPoints--;
        if (currentHitPoints >= 0)
        {
            DisableHeart(currentHitPoints);
        }

        if(currentHitPoints == 0)
        {
            Die();
            Debug.Log("die!");
        }

        Debug.Log("Current HP: " + currentHitPoints);
    }

    private void DisableHeart(int heartNumber)
    {
        hearts[heartNumber].SendMessage("Off");
    }

    private void Die()
    {
        isDying = true;
        isLocked = true;
        Animator.SetBool("IsDying", true);

        Rigidbody2D.velocity = new Vector2(8, 0);
        StartCoroutine("RollOut");
    }

    IEnumerator Fight()
    {
        Rigidbody2D.velocity = new Vector2(0, 0);
        Animator.SetBool("IsFight", true);
        isLocked = true;
        yield return new WaitForSeconds(1);
        isLocked = false;
        Animator.SetBool("IsFight", false);
    }

    IEnumerator RollOut()
    {

        for (float i = 0; i <= 30; i++)
        {
            transform.Rotate(new Vector3(0, 0, 30f * i));

            yield return new WaitForSeconds(.1f);
        }
        Destroy(this.gameObject);
    }
}
