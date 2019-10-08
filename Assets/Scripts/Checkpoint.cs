using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : Activator
{
    [SerializeField]
    int charactersPresent = 0;
    bool triggered = false;

    static Checkpoint currentCheckpoint = null;

    // Start is called before the first frame update
    void Start()
    {
        //Register();
    }

    // Update is called once per frame
    void Update()
    {
        if (!triggered && charactersPresent == 3)
        {
            triggered = true;
            on = true;
            currentCheckpoint = this;
            //UIController.instance.ChangeStateTo((int)UIController.UIState.ENDLEVEL);
            StartCoroutine(LevelController.Instance.LevelExitTransition());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            charactersPresent++;
            collision.GetComponent<Character>().AbleToFinishLevel = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            charactersPresent--;
            collision.GetComponent<Character>().AbleToFinishLevel = false;
        }
    }

    /// <summary>
    ///
    /// </summary>
    /// <returns> Position of the last activated checkpoint </returns>
    public static Vector3 GetPosition()
    {
        if (currentCheckpoint == null)
        {
            Debug.LogError("No current checkpoint specified!");
            return Vector3.zero;
        }
        else
        {
            return currentCheckpoint.transform.position; // TODO: improve position
        }
    }

    public static bool CurrentExists()
    {
        return currentCheckpoint != null;
    }
}
