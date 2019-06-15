using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    public Rigidbody2D Rigidbody2D;
    public float VerticalSpeed = 1f;
    public float HorizontalSpeed = 0f;

    private float LeftBound = -12f;
    private float RightBound = 5f;

    // Start is called before the first frame update
    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        Rigidbody2D.velocity = new Vector2(HorizontalSpeed, VerticalSpeed * -1f);


        if (transform.position.x < LeftBound || transform.position.x > RightBound)
        {
            Destroy(this.gameObject);
        }
    }

    public void OnPlayerAttack(int attackType)
    {
        Debug.Log("i'm hit with attack " + attackType);
        VerticalSpeed = 0;

        if(attackType == 1)
        {
            StartCoroutine("Fade");
        }
        else if(attackType == 2)
        {
            float mid = RightBound - (RightBound - LeftBound) / 2;
            Debug.Log("mid: " + mid + "; pos: " + transform.position.x);
            if(transform.position.x < mid) 
            {
                HorizontalSpeed = -8;
                transform.Rotate(0, 0, 270);
            }
            else
            {
                HorizontalSpeed = 8;
                transform.Rotate(0, 0, 90);
            }

        }
        else if(attackType == 3)
        {
            // play animation
        }


    }

    IEnumerator Fade()
    {
        for (float f = 1f; f >= 0; f -= 0.1f)
        {
            Color c = GetComponent<Renderer>().material.color;
            c.a = f;
            GetComponent<Renderer>().material.color = c;
            yield return new WaitForSeconds(.1f);
        }
        Destroy(this.gameObject);
    }
}
