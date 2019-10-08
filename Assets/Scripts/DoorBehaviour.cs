using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBehaviour : Activatable
{
    AudioSource audio;

    [SerializeField]
    AudioClip soundOn;
    [SerializeField]
    AudioClip soundOff;

    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
        if (audio == null)
        {
            Debug.LogError("Door: No Audio source found!");
        }
    }

    private void Update()
    {
        CheckStatus(); // implemented in base class
    }

    protected override void OnActivate()
    {
        Debug.Log("Door OnActivate");
        // door disappears/opens
        //gameObject.SetActive(false);
        audio.PlayOneShot(soundOn);
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
    }

    protected override void OnDeactivate()
    {
        Debug.Log("Door OnDeactivate");
        // door appears/closes again
        //gameObject.SetActive(true);
        audio.PlayOneShot(soundOff);
        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<Collider2D>().enabled = true;
    }
}
