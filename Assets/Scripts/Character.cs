using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base class for all three characters
/// </summary>
public class Character : MonoBehaviour
{
    [SerializeField]
    protected AudioSource audioWalking;

    public bool Controlled = false;
    public int Weight; // for pressing buttons

    [SerializeField]
    GameObject Marker;

    [SerializeField]
    // y offset for spawning in regards to spawn points
    protected float yOffs = -0.53f;

    protected Rigidbody2D rb;
    protected Collider2D coll;

    Vector2 startPosition; // for resetting the level

    public bool AbleToFinishLevel { get; set; }

    public string charName = "";

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        startPosition = transform.position;
        AbleToFinishLevel = false;

        if (audioWalking == null)
        {
            Debug.LogError("Character AudioWalking is null!");
        }
    }

    public void Activate()
    {
        Controlled = true;
        rb.bodyType = RigidbodyType2D.Dynamic;
        coll.isTrigger = false;
        Marker.SetActive(true);
    }

    public void Deactivate()
    {
        Controlled = false;
        rb.velocity = Vector3.zero;
        rb.bodyType = RigidbodyType2D.Kinematic;
        coll.isTrigger = true;
        Marker.SetActive(false);
    }

    public void ResetPosition()
    {

        //if (Checkpoint.CurrentExists())
        //{
        //    transform.position = Checkpoint.GetPosition();
        //}
        //else
        //{
        //    transform.position = startPosition;
        //}
        int lvl = (int)LevelController.Instance.GetCurrentLevel();

        if (lvl == 0)
        {
            transform.position = startPosition;
        }
        else
        {
            transform.position = Controller.Instance.levelStart[lvl].transform.position + new Vector3(0f, yOffs);
        }

        transform.localScale = Vector3.one;
    }

}
