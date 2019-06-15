using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    public Rigidbody2D Rigidbody2D;
    public float Speed = 1f;

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
        Rigidbody2D.velocity = Vector2.down * Speed;
    }

    public void StartFading()
    {
        Debug.Log("i'm gonna die!!");
        Speed = 0;

        StartCoroutine("Fade");
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
