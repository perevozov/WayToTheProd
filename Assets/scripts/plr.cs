using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plr : MonoBehaviour
{
    public float Speed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        if (Input.GetAxis("Jump") > 0)
        {
            Debug.Log("start jump");
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 50);
        }

        GetComponent<Rigidbody2D>().velocity = new Vector2(h, 0) * Speed;
    }

}
