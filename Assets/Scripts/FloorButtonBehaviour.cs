using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorButtonBehaviour : Activator
{
    [SerializeField]
    bool pressable = false;

    [SerializeField]
    int weightToPress = 2;

    [SerializeField]
    public int currentWeight = 0;

    [SerializeField]
    Sprite spriteOn;
    [SerializeField]
    Sprite spriteHalfOn;
    [SerializeField]
    Sprite spriteOff;

    [SerializeField]
    AudioSource audio;
    [SerializeField]
    AudioClip soundOn;
    [SerializeField]
    AudioClip soundHalfOn;
    [SerializeField]
    AudioClip soundOff;

    [SerializeField]
    AudioSource audioRaw;

    // Start is called before the first frame update
    void Start()
    {
        Register();
        //audio = GetComponent<AudioSource>();
        if (audio == null || audioRaw == null)
            Debug.LogError("FloorButton audio/audioRaw is null!");
    }

    // Update is called once per frame
    void Update()
    {
        pressable = currentWeight >= weightToPress;

        if (pressable) // to not continuously press the button
        {
            on = true;
            if (weightToPress > 1)
            {
                Tutorial.Instance.heavyButtonHint = false;
            }
        }
        else
        {
            on = false;
        }

        if (spriteOn != null & spriteOff != null)
        {
            if (on)
            {
                GetComponent<SpriteRenderer>().sprite = spriteOn;
            }
            else if (currentWeight > 0 && spriteHalfOn != null)
            {
                GetComponent<SpriteRenderer>().sprite = spriteHalfOn;
            }
            else
            {
                GetComponent<SpriteRenderer>().sprite = spriteOff;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            int characterWeight = collision.gameObject.GetComponent<Character>().Weight;
            currentWeight += characterWeight;
            //Debug.Log("Character enter: " + collision.gameObject.name + " curWeight: " + currentWeight);
            PlaySound();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            int characterWeight = collision.gameObject.GetComponent<Character>().Weight;
            currentWeight -= characterWeight;
            //Debug.Log("Character leave: " + collision.gameObject.name + " curWeight: " + currentWeight);
            PlaySound();
        }
    }

    private void PlaySound()
    {
        audioRaw.Play(); // plays click

        // Rest plays beep depending on weight

        if (currentWeight >= weightToPress)
        {
            audio.PlayOneShot(soundOn);
        }
        else if (currentWeight > 0)
        {
            audio.PlayOneShot(soundHalfOn);
        }
        else
        {
            audio.PlayOneShot(soundOff);
        }
    }
}
