using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class heart_controller : MonoBehaviour
{
    public bool IsOn = true;
    public Animator Animator;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        Animator.SetBool("IsOn", IsOn);
    }

    public void Off()
    {
        IsOn = false;
    }
}
