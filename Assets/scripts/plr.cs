using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plr : MonoBehaviour
{
    public float Speed = 5f;
    public float MaxSpeed = 5f;
    public Animator Animator;

    private bool m_FacingRight = true;
    private bool jumping = false;

    public float hMove = 0;

    public bool isGrounded = false;
    private Rigidbody2D Rigidbody2D;

    // Start is called before the first frame update
    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        hMove = Input.GetAxisRaw("Horizontal") * Speed;


        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, Vector2.down, 0.5f);

        bool collision = false;
        // If it hits something...
        foreach(RaycastHit2D hit in hits)
        {
            if(hit.collider.gameObject == gameObject) 
                continue;

            collision = true;
        }
        isGrounded = collision;

        if (Input.GetButtonUp("Jump") && isGrounded)
        {
            jumping = true;
        }


    }

    private void FixedUpdate()
    {
        float currentSpeed = Mathf.Abs(Rigidbody2D.velocity.x);
        if (isGrounded)
        {
            if(jumping)
            {
                Rigidbody2D.AddForce(new Vector2(0, 70f));
                jumping = false;
            }

            if(currentSpeed < MaxSpeed)
            {
                Rigidbody2D.AddForce(new Vector2(hMove, 0));
            }

        }
        else
        {
            //Animator.SetFloat("Speed", 0);
        }


        if (hMove > 0 && !m_FacingRight)
        {
            Flip();
        }
        else if (hMove < 0 && m_FacingRight)
        {
            Flip();
        }

        Animator.SetFloat("Speed", currentSpeed);
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
}
