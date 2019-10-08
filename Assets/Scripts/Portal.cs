using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : Activatable
{
    public Portal Destination;

    [SerializeField]
    List<GameObject> subjects;

    [SerializeField]
    Sprite spriteOn;
    [SerializeField]
    Sprite spriteOff;

    AudioSource audio;
    [SerializeField]
    AudioClip soundOn;
    [SerializeField]
    AudioClip soundContinuous;
    [SerializeField]
    AudioClip soundOff;

    // Start is called before the first frame update
    void Start()
    {
        var renderer = GetComponent<SpriteRenderer>();
        renderer.sprite = spriteOff;
        audio = GetComponent<AudioSource>();
        subjects = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckStatus();

        if (on)
        {
            if (!audio.isPlaying)
            {
                audio.clip = soundContinuous;
                audio.Play();
            }
        }

        if (on && Input.GetButtonDown("Jump"))
        {
            foreach (var subject in subjects)
            {
                if (subject == Controller.Instance.ActiveCharacter.gameObject)
                {
                    Vector3 offs = transform.position - subject.transform.position;

                    Vector3 destinationPos = Destination.transform.position - offs;

                    subject.transform.position = destinationPos;
                }
            }
        }
    }

    protected override void OnActivate()
    {
        //var renderer = GetComponent<SpriteRenderer>();
        //renderer.sprite = spriteOn;
        audio.PlayOneShot(soundOn);
        StartCoroutine(DelayedOpen());
    }

    protected override void OnDeactivate()
    {
        //var renderer = GetComponent<SpriteRenderer>();
        //renderer.sprite = spriteOff;
        audio.Stop();
        audio.PlayOneShot(soundOff);
        StartCoroutine(DelayedClose());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            subjects.Add(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            subjects.Remove(collision.gameObject);
        }
    }

    IEnumerator DelayedOpen()
    {
        yield return new WaitForSeconds(.1f);
        var renderer = GetComponent<SpriteRenderer>();
        renderer.sprite = spriteOn;
        GetComponentInChildren<ParticleSystem>().Play();
    }

    IEnumerator DelayedClose()
    {
        yield return new WaitForSeconds(.1f);
        var renderer = GetComponent<SpriteRenderer>();
        renderer.sprite = spriteOff;
    }
}
