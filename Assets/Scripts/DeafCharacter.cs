using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeafCharacter : Character
{
    public bool TouchedByOtherCharacter = false;

    [SerializeField]
    float movementSpeed;

    // Use this for initialization
    void Start()
    {
        charName = "Aramis";
        yOffs -= 0.13f;
    }

    private void Update()
    {

        if (rb.velocity.x != 0)
        {
            if (!audioWalking.isPlaying)
            {
                audioWalking.Play();
            }
            GetComponent<AutoAnimator>().StartAnimation();
        }
        else
        {
            audioWalking.Stop();
            GetComponent<AutoAnimator>().StopAnimation();
        }

        /*
        var hit = Physics2D.BoxCast(transform.position, GetComponent<Collider2D>().bounds.size, 0, Vector2.zero);

        if(hit.collider.gameObject.CompareTag("Player"))
        {
            TouchedByOtherCharacter = true;
        }
        else
        {
            TouchedByOtherCharacter = false;
        }
        */
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (!Controlled)
        {
            GetComponent<AutoAnimator>().StopAnimation();
            return;
        }

        Vector2 vel = rb.velocity;
        float inputDir = Input.GetAxis("Horizontal");

        var scale = transform.localScale;

        if (inputDir > 0)
        {
            scale.x = 1;
        }
        else if (inputDir < 0)
        {
            scale.x = -1;
        }

        transform.localScale = scale;

        vel.x = movementSpeed * inputDir;

        rb.velocity = vel;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            TouchedByOtherCharacter = false;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            TouchedByOtherCharacter = true;
        }
    }
}
