using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plr : MonoBehaviour
{
    public float Speed = 5f;
    public Animator Animator;

    private bool m_FacingRight = true;

    public float hMove = 0;

    public bool isGrounded = false;

    // Start is called before the first frame update
    void Start()
    {
        
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
            {
                continue;
            }
            collision = true;
            Debug.Log(hit.collider.gameObject.name);
        }
        isGrounded = collision;
    }

    private void FixedUpdate()
    {
        if(isGrounded)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(hMove, 0);
            Animator.SetFloat("Speed", Mathf.Abs(hMove));
        }
        else
        {
            Animator.SetFloat("Speed", 0);
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
}
