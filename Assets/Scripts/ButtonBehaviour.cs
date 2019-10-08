using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonBehaviour : Activator
{

    [SerializeField]
    bool pressable;

    [SerializeField]
    Sprite spriteOn;
    [SerializeField]
    Sprite spriteOff;

    [SerializeField]
    AudioSource audio;
    [SerializeField]
    AudioClip soundOn;
    [SerializeField]
    AudioClip soundOff;

    [SerializeField]
    AudioSource audioRaw;
    //[SerializeField]
    //AudioClip soundRaw;

    // Start is called before the first frame update
    void Start()
    {
        Register();
        if (audio == null || audioRaw == null)
        {
            Debug.LogError("ButtonBehaviour audio/audioRaw is null!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Controller.Instance.ActiveCharacter == Controller.Instance.BlindCharacter && Controller.Instance.BlindCharacter.Walking)
        {
            // cant activate buttons while running
        }
        else if (pressable && Input.GetButtonDown("Jump")) //spacbar
        {
            Debug.Log("Switch");
            on = !on;
            audioRaw.Play(); // plays click
            PlaySound(); // plays beep           
            Tutorial.Instance.activateHint = false;
            Tutorial.Instance.heavyButtonHintReady = true;
        }

        if (spriteOn != null & spriteOff != null)
        {
            if (on)
            {
                GetComponent<SpriteRenderer>().sprite = spriteOn;
            }
            else
            {
                GetComponent<SpriteRenderer>().sprite = spriteOff;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject == Controller.Instance.ActiveCharacter.gameObject)
        {
            pressable = true;
        }
        else
        {
            pressable = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            pressable = false;
        }
    }

    private void PlaySound()
    {
        if (on)
        {
            audio.PlayOneShot(soundOn);
        }
        else
        {
            audio.PlayOneShot(soundOff);
        }
    }
}
