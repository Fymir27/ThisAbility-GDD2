using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Speechbubble : MonoBehaviour
{
    AudioSource audioTalking;

    [SerializeField]
    AudioClip[] talking;
    [SerializeField]
    AudioClip[] calling;

    public static Speechbubble Instance;

    [SerializeField]
    GameObject bubblePrefab;

    [SerializeField]
    Vector3 offset;

    GameObject activeBubble;

    string[] startText = { "Show them, ", "Go on, ", "Help me out, ", "I need your help, ", "Your turn, " };

    string previousCharName = "";

    Transform charTransform;


    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        audioTalking = GetComponent<AudioSource>();

        if (audioTalking == null)
        {
            Debug.LogError("Speechbubble: No Audio source found!");
        }
    }

    void DisplayInternal(string text, float time, AudioClip sound)
    {
        if (activeBubble != null)
            Destroy(activeBubble);

        activeBubble = Instantiate(bubblePrefab, charTransform.position + offset, Quaternion.identity, transform);

        var textMesh = activeBubble.GetComponentInChildren<TextMeshPro>();

        textMesh.text = text;

        activeBubble.GetComponent<FollowPositionOnly>().Init(charTransform, offset);

        audioTalking.PlayOneShot(sound);

        Destroy(activeBubble, time);
    }

    string GenerateRandomText()
    {
        int r = Random.Range(0, startText.Length);
        return startText[r];
    }

    public void SetCurrentCharTransform(Transform transform)
    {
        if (transform != null)
            charTransform = transform;
    }

    /// <summary>
    /// displays random message (character switch) for 1s
    /// </summary>
    public void Display()
    {
        if (previousCharName == Controller.Instance.ActiveCharacter.charName)
            return;

        previousCharName = Controller.Instance.ActiveCharacter.charName;

        string text = GenerateRandomText();

        text += Controller.Instance.ActiveCharacter.charName;

        AudioClip call = calling[Random.Range(0, calling.Length)];

        DisplayInternal(text, 1f, call);
    }

    /// <summary>
    /// displays user defined message
    /// </summary>
    /// <param name="text">message to display</param>
    public void Display(string text, float time = 8f, bool withAudio = true)
    {
        charTransform = Controller.Instance.ActiveCharacter.transform;

        AudioClip talk = null;

        if (withAudio)
        {
            talk = talking[Random.Range(0, talking.Length)];
        }

        DisplayInternal(text, time, talk);
    }
}
