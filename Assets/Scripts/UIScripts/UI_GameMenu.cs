using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_GameMenu : UIPanel
{
    public static UI_GameMenu instance;

    [SerializeField]
    List<Image> characters;

    [SerializeField]
    List<Image> characterBorder;

    [SerializeField]
    List<Image> markSpace;

    [SerializeField]
    Sprite VMark;
    [SerializeField]
    Sprite XMark;

    [SerializeField]
    Animator textAnimator;

    Color fadedWhite = new Color(1, 1, 1, 0.3f);
    int previousCharacter = -1;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        LevelController.levelChanged += OnLevelChange;

        foreach (var i in characterBorder)
            i.color = fadedWhite;

        foreach (var i in characters)
            i.color = fadedWhite;

        foreach (var i in markSpace)
            i.sprite = null;

    }

    public override void Activate()
    {
        base.Activate();
        OnLevelChange(LevelController.Instance.GetCurrentLevel());

    }

    public override void Deactivate()
    {
        base.Deactivate();
    }

    public void ChangeCharUI(Controller.CharacterType currentChar)
    {
        if (previousCharacter != -1)
        {
            characterBorder[previousCharacter].color = fadedWhite;
            characters[previousCharacter].color = fadedWhite;
        }

        characterBorder[(int)currentChar].color = Color.white;
        characters[(int)currentChar].color = Color.white;

        previousCharacter = (int)currentChar;
    }

    public void OnLevelChange(LevelController.Levels newLevel)
    {
        bool repeating = false;

        TextMeshProUGUI[] textFields = textAnimator.GetComponentsInChildren<TextMeshProUGUI>();
        foreach (var i in textFields)
        {
            if (i.text != "Level " + (int)newLevel)
                i.SetText("Level " + (int)newLevel);
            else
                repeating = true;
        }
        if (!repeating)
            textAnimator.SetTrigger("Start");
    }

    void Update()
    {
        foreach (var i in markSpace)
            i.color = Color.white;

        if (Controller.Instance != null)
        {

            if (Controller.Instance.DeafCharacter.AbleToFinishLevel)
                markSpace[0].sprite = VMark;
            else if ((!Controller.Instance.DeafCharacter.AbleToFinishLevel
                && Controller.Instance.DeafCharacter.TouchedByOtherCharacter) ||
                Controller.Instance.ActiveCharacter == Controller.Instance.DeafCharacter)

            {
                markSpace[0].sprite = null;
                markSpace[0].color = new Color(1, 1, 1, 0);
            }
            else
                markSpace[0].sprite = XMark;
            if (Controller.Instance.BlindCharacter.AbleToFinishLevel)
                markSpace[1].sprite = VMark;
            else
            {
                markSpace[1].sprite = null;
                markSpace[1].color = new Color(1, 1, 1, 0);
            }
            if (Controller.Instance.WheelchairCharacter.AbleToFinishLevel)
                markSpace[2].sprite = VMark;
            else
            {
                markSpace[2].sprite = null;
                markSpace[2].color = new Color(1, 1, 1, 0);

            }
        }
    }
}
