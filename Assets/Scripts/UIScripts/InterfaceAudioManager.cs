using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterfaceAudioManager : MonoBehaviour
{
    public static InterfaceAudioManager Instance;

    [SerializeField]
    AudioSource audio;

    [SerializeField]
    AudioClip soundClick;
    [SerializeField]
    AudioClip soundHover;
    [SerializeField]
    AudioClip soundActivate;
    [SerializeField]
    AudioClip soundDeactivate;

    void Awake()
    {
        Instance = this;
    }

    public void Hover()
    {
        audio.PlayOneShot(soundHover);
    }

    public void Click()
    {
        audio.PlayOneShot(soundClick);
    }

    public void Activate()
    {
        audio.PlayOneShot(soundActivate);
    }

    public void Deactivate()
    {
        audio.PlayOneShot(soundDeactivate);
    }

    public void Toggle(bool on)
    {
        Debug.Log("Toggle: " + on);
        if (on)
        {
            Activate();
        }
        else
        {
            Deactivate();
        }
    }
}
