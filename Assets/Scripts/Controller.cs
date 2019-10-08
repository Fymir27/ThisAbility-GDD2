using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Linq;

public class Controller : MonoBehaviour
{
    /// <summary>
    /// Singleton, only one Instance of this should exist at any time!
    /// call with Controller.Instance.Foo() from anywhere
    /// </summary>
    public static Controller Instance;

    /// <summary>
    /// 0 - BLIND
    /// 1 - DEAF
    /// 2 - WHEELCHAIR
    /// </summary>
    public enum CharacterType
    {
        Deaf,
        Blind,
        Wheelchair
    }
    CharacterType currentCharacter = CharacterType.Deaf;
    CharacterType nextCharacter = CharacterType.Wheelchair;

    public Character ActiveCharacter;
    public Character NextCharacter;

    [SerializeField]
    Vector3 cameraOffset = Vector3.zero;

    public bool cameraFollow = false;

    [SerializeField]
    // initialize in editor!
    List<Character> CharacterList;

    [SerializeField]
    GameObject BonusPrefab;

    [SerializeField]
    GameObject blindCamera;

    public DeafCharacter DeafCharacter;
    public BlindCharacter BlindCharacter;
    public WheelchairCharacter WheelchairCharacter;

    public bool SwitchingAllowed = true;

    public List<System.Tuple<Activator, bool>> Activators;
    public List<Vector3> BonusObjectives;

    // Spawn positions
    public GameObject[] levelStart;

    private void Awake()
    {
        Instance = this;

        Activators = new List<System.Tuple<Activator, bool>>();
        BonusObjectives = new List<Vector3>();

        /*
        var colliderD = DeafCharacter.GetComponent<Collider2D>();
        var colliderB = BlindCharacter.GetComponent<Collider2D>();
        var colliderW = WheelchairCharacter.GetComponent<Collider2D>();

        Physics2D.IgnoreCollision(colliderB, colliderD);
        Physics2D.IgnoreCollision(colliderD, colliderW);
        Physics2D.IgnoreCollision(colliderW, colliderB);
        */
    }

    // Start is called before the first frame update
    void Start()
    {
        ChangeCharacter(DeafCharacter);
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetButtonDown("Fire1"))
        {
            //ChangeCharacter(NextCharacter);
        }

        if (Input.GetButtonDown("Cancel") || Input.GetKey(KeyCode.R))
        {
            ResetLevel();
            Score.Instance.IncrementTimesRestarted();
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (DeafCharacter.TouchedByOtherCharacter)
            {
                ChangeCharacter(DeafCharacter);
                Tutorial.Instance.switchHint = false;
            }
            else
                Speechbubble.Instance.Display("Can't Switch!", 1f, false);

        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ChangeCharacter(BlindCharacter);
            Tutorial.Instance.switchHint = false;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ChangeCharacter(WheelchairCharacter);
            Tutorial.Instance.switchHint = false;
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            LevelController.Instance.NextLevel();
        }

        if (cameraFollow)
        {
            Camera.main.transform.position = ActiveCharacter.gameObject.transform.position + cameraOffset;
        }
    }

    void ChangeCharacter(Character newCharacter)
    {
        if (!SwitchingAllowed)
        {
            Speechbubble.Instance.Display("Can't Switch!", 1f, false);
            return;
        }

        ActiveCharacter?.Deactivate();

        if (ActiveCharacter != null)
            Speechbubble.Instance.SetCurrentCharTransform(ActiveCharacter.transform);
        else
            Speechbubble.Instance.SetCurrentCharTransform(transform);

        ActiveCharacter = newCharacter;

        Speechbubble.Instance.Display();

        UpdateNextCharacter();

        DetermineCurrentCharacterEnum(newCharacter);

        MoveCamera();

        ChangePostProcessing();

    }
    void DetermineCurrentCharacterEnum(Character charType)
    {
        if (charType.GetType() == typeof(DeafCharacter))
            currentCharacter = CharacterType.Deaf;
        if (charType.GetType() == typeof(BlindCharacter))
            currentCharacter = CharacterType.Blind;
        if (charType.GetType() == typeof(WheelchairCharacter))
            currentCharacter = CharacterType.Wheelchair;

        UI_GameMenu.instance.ChangeCharUI(currentCharacter);
    }

    void UpdateNextCharacter()
    {
        if (ActiveCharacter == DeafCharacter)
        {
            NextCharacter = WheelchairCharacter;
            nextCharacter = CharacterType.Wheelchair;
        }
        else if (ActiveCharacter == BlindCharacter)
        {
            if (BlindCharacter.Walking)
                return;

            if (DeafCharacter.TouchedByOtherCharacter)
            {
                NextCharacter = DeafCharacter;
                nextCharacter = CharacterType.Deaf;
            }
            else
            {
                NextCharacter = WheelchairCharacter;
                nextCharacter = CharacterType.Wheelchair;
            }
        }
        else if (ActiveCharacter == WheelchairCharacter)
        {
            if (DeafCharacter.TouchedByOtherCharacter)
            {
                NextCharacter = DeafCharacter;
                nextCharacter = CharacterType.Deaf;
            }
            else
            {
                NextCharacter = BlindCharacter;
                nextCharacter = CharacterType.Blind;
            }
        }
        else
        {
            throw new System.Exception("What Character are you then??");
        }
    }

    public void ResetLevel()
    {
        foreach (var tuple in Activators)
        {
            tuple.Item1.on = tuple.Item2;
        }

        foreach (var pos in BonusObjectives)
        {
            Instantiate(BonusPrefab, pos, Quaternion.identity, this.transform);
        }

        Score.Instance.ResetBonusObjective();

        Input.ResetInputAxes();

        WheelchairCharacter.gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        BlindCharacter.gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        DeafCharacter.gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;

        SwitchingAllowed = true;

        ChangeCharacter(DeafCharacter);

        BlindCharacter.Walking = false;

        DeafCharacter.ResetPosition();
        BlindCharacter.ResetPosition();
        WheelchairCharacter.ResetPosition();

        WheelchairCharacter.MovementSpeed = Mathf.Abs(WheelchairCharacter.MovementSpeed);
    }



    //Reason for moving the camera movement to here is that it looks like 
    //CharacterChange.cs is a prototype script Since it changes characters with KeyCode.Q.

    /// <summary>
    /// Instead of having the camera movement in the update function of 
    /// CharacterChange.cs class, moved it to here and made it ease out.
    /// </summary>
    void MoveCamera()
    {
        cameraFollow = false;
        Vector3 moveToPos = ActiveCharacter.gameObject.transform.position + cameraOffset;

        iTween.MoveTo(Camera.main.gameObject, iTween.Hash("position", moveToPos,
            "time", 1f,
            "easetype", iTween.EaseType.easeInExpo,
            "oncompletetarget", gameObject,
            "oncomplete", "EnableCameraFollow"));
    }

    void EnableCameraFollow()
    {
        cameraFollow = true;
        ActiveCharacter.Activate();
    }

    void ChangePostProcessing()
    {
        if (currentCharacter == CharacterType.Blind)
        {
            PostProcessingController.instance.BlindPPActivate();
            blindCamera.SetActive(true);
        }
        else
        {
            PostProcessingController.instance.OtherPPActivate();
            blindCamera.SetActive(false);
        }
    }


}
