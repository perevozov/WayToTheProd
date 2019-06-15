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
        Rigidbody2D.velocity = Vector2.down * Speed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
