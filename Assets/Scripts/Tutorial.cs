using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public static Tutorial Instance;
    public bool switchHint = true;
    public bool activateHint = true;
    public bool heavyButtonHint = true;

    public bool activateHintReady = false;
    public bool heavyButtonHintReady = false;

    //AudioSource audio;
    //[SerializeField]
    //AudioClip mumbling;

    GameObject firstButton;

    float hintTimer = 0f;

    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        //audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        hintTimer += Time.deltaTime;

        if (switchHint && hintTimer > 10f)
        {
            Speechbubble.Instance.Display("Looks like i need help!\n(Switch Characters with 1,2,3)");
            //audio.PlayOneShot(mumbling);
            switchHint = false;
        }

        if (!activateHintReady && Controller.Instance.ActiveCharacter.gameObject == Controller.Instance.BlindCharacter.gameObject)
        {
            hintTimer = 0f;
            activateHintReady = true;
        }

        if (activateHint && activateHintReady && hintTimer > 10f)
        {
            Speechbubble.Instance.Display("Mabye that orange lever does something...\n(Interact with Space)");
            //audio.PlayOneShot(mumbling);
            activateHint = false;
        }

        if (heavyButtonHintReady && heavyButtonHint && hintTimer > 20f)
        {
            Speechbubble.Instance.Display("That red button on the floor \n seems difficult to press...\n(Stand on it together)");
            //audio.PlayOneShot(mumbling);
            heavyButtonHint = false;
        }
    }
}
