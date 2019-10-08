using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelchairCharacter : Character
{
    [SerializeField]
    public float MovementSpeed;

    void Start()
    {
        charName = "Porthos";
        yOffs -= 0.4f;
    }

    private void Update()
    {
        if (TurnAround.Possible && Input.GetButtonDown("Jump"))
        {
            // flip sprite
            var scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;

            MovementSpeed *= -1;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!Controlled)
        {
            GetComponent<AutoAnimator>().StopAnimation();
            return;
        }

        float inputDir = Input.GetAxis("Horizontal");

        var scale = transform.localScale;

        // if the input direction matches where you're allowed to go
        if (inputDir * MovementSpeed > 0)
        {
            Vector2 vel = rb.velocity;
            vel.x = MovementSpeed * Mathf.Abs(inputDir);
            rb.velocity = vel;

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

    }


}
