using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public static LevelController Instance;

    public delegate void OnLevelChange(Levels currentLevel);
    public static event OnLevelChange levelChanged;

    /// <summary>
    /// LEVEL0,
    /// LEVEL1,
    /// LEVEL2,
    /// LEVEL3,
    /// LEVEL4,
    /// LEVEL5
    /// </summary>
    public enum Levels
    {
        LEVEL0,
        LEVEL1,
        LEVEL2,
        LEVEL3,
        LEVEL4,
        LEVEL5
    }

    public AudioSource audioWin;

    Levels currentLevel = Levels.LEVEL0;

    void Awake()
    {
        if (Instance == null) Instance = this;
    }

    public void NextLevel()
    {       
        int lvl = (int)currentLevel;
        if (lvl + 1 >= Controller.Instance.levelStart.Length)
        {
            UIController.instance.ChangeStateTo((int)UIController.UIState.CREDITS);
            currentLevel = Levels.LEVEL0;
            return;
        }
        lvl++;
        currentLevel = (Levels)lvl;

        Debug.Log("Next Level! " + currentLevel);

        StartCoroutine(LevelEntryTransition());
    }

    public Levels GetCurrentLevel()
    {
        return currentLevel;
    }

    void SpawnCharacters(int lvl)
    {
        var spawnPos = Controller.Instance.levelStart[lvl].transform.position;
        Controller.Instance.WheelchairCharacter.gameObject.transform.position = spawnPos;
        Controller.Instance.BlindCharacter.gameObject.transform.position = spawnPos;
        Controller.Instance.DeafCharacter.gameObject.transform.position = spawnPos;
    }

    public IEnumerator LevelExitTransition()
    {
        audioWin.Play();

        Controller.Instance.cameraFollow = false;
        Controller.Instance.SwitchingAllowed = false;
        Controller.Instance.ActiveCharacter.Deactivate();

        //Vector3 walkTowards = Controller.Instance.BlindCharacter.gameObject.transform.position + new Vector3(5, 0);
        Controller.Instance.WheelchairCharacter.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(3, 0);
        Controller.Instance.BlindCharacter.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(3, 0);
        Controller.Instance.DeafCharacter.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(3, 0);

        yield return new WaitForSeconds(1);

        UIController.instance.ChangeStateTo((int)UIController.UIState.ENDLEVEL);

        Controller.Instance.WheelchairCharacter.gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        Controller.Instance.BlindCharacter.gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        Controller.Instance.DeafCharacter.gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
    }

    public IEnumerator LevelEntryTransition()
    {
        Controller.Instance.cameraFollow = true;

        UIController.instance.ChangeStateTo((int)UIController.UIState.GAME);

        levelChanged(currentLevel);

        Controller.Instance.DeafCharacter.ResetPosition();
        Controller.Instance.BlindCharacter.ResetPosition();
        Controller.Instance.WheelchairCharacter.ResetPosition();

        Controller.Instance.WheelchairCharacter.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(4, 0);
        Controller.Instance.BlindCharacter.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(3, 0);
        Controller.Instance.DeafCharacter.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(2, 0);

        yield return new WaitForSeconds(.8f);

        Controller.Instance.WheelchairCharacter.gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        Controller.Instance.BlindCharacter.gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        Controller.Instance.DeafCharacter.gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;

        Controller.Instance.SwitchingAllowed = true;
        Controller.Instance.ActiveCharacter.Activate();
    }
}
