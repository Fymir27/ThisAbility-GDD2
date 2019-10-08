using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlindCharacter : Character
{
    public bool Walking;

    [SerializeField]
    float movementSpeed;

    // in direction x
    float movement;

    // Use this for initialization
    void Start()
    {
        charName = "Athos";
    }

    // Update is called once per frame
    void Update()
    {

        if (!Controlled)
        {
            GetComponent<AutoAnimator>().StopAnimation();
            return;
        }

        if (Walking)
        {
            if (!audioWalking.isPlaying)
            {
                audioWalking.Play();
            }

            GetComponent<AutoAnimator>().StartAnimation();
            return;
        }
        else
        {
            audioWalking.Stop();
            GetComponent<AutoAnimator>().StopAnimation();
        }

        Vector2 vel = rb.velocity;
        //float inputDir = Input.GetAxis("Horizontal");

        var scale = transform.localScale;

        float hor = Input.GetAxis("Horizontal");

        if (hor > 0)
        {
            scale.x = 1;
            movement = movementSpeed;
            Walking = true;
            Controller.Instance.SwitchingAllowed = false;
        }
        else if (hor < 0)
        {
            scale.x = -1;
            movement = -movementSpeed;
            Walking = true;
            Controller.Instance.SwitchingAllowed = false;
        }

        transform.localScale = scale;

    }

    private void FixedUpdate()
    {
        var vel = rb.velocity;
        if (Walking)
        {
            vel.x = movement;

        }
        else
        {
            //vel = Vector3.zero;
        }

        rb.velocity = vel;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Ground"))
        {
            //Debug.Log("On Collision Enter Blind guy (not ground)");
            Walking = false;
            Controller.Instance.SwitchingAllowed = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Walking = false;
            Controller.Instance.SwitchingAllowed = true;
        }
    }

}
