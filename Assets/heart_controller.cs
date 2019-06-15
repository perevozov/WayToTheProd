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

    public void FadeOut()
    {
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
