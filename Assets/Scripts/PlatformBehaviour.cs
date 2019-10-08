using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Linq;

public class PlatformBehaviour : Activatable
{
    AudioSource audioMoving;

    [SerializeField]
    Transform anchor1;

    [SerializeField]
    Transform anchor2;

    [SerializeField, Range(.1f, 1f)]
    float speed;

    [SerializeField]
    bool moving = false;

    [SerializeField]
    Collider2D[] barriersAnchor1;
    [SerializeField]
    Collider2D[] barriersAnchor2;

    [SerializeField]
    bool disableBarriersAnchor1;
    [SerializeField]
    bool disableBarriersAnchor2;

    float t = 0f;

    Rigidbody2D rb;

    List<GameObject> charactersOnPlatform;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        charactersOnPlatform = new List<GameObject>();

        foreach (var barrier in barriersAnchor2)
        {
            barrier.enabled = true;
        }

        audioMoving = GetComponent<AudioSource>();
        if (audioMoving == null)
        {
            Debug.LogError("Platform: No Audio source found!");
        }
    }

    private void Update()
    {
        CheckStatus();
    }

    void FixedUpdate()
    {
        if (!moving)
        {
            audioMoving.Stop();
        }
        else if (on)
        {
            transform.position = Vector2.Lerp(anchor1.position, anchor2.position, t);
            t = Mathf.Clamp01(t + speed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector2.Lerp(anchor1.position, anchor2.position, t);
            t = Mathf.Clamp01(t - speed * Time.deltaTime);
        }

        if (moving)
        {
            if (!audioMoving.isPlaying)
            {
                audioMoving.Play();
            }

            if (t == 0f)
            {
                foreach (var barrier in barriersAnchor1)
                {
                    barrier.enabled = false;
                }

                Controller.Instance.SwitchingAllowed = true;
                moving = false;
            }
            else if (t == 1f)
            {
                foreach (var barrier in barriersAnchor2)
                {
                    barrier.enabled = false;
                }

                Controller.Instance.SwitchingAllowed = true;
                moving = false;
            }
            else
            {
                Controller.Instance.SwitchingAllowed = false;
            }
        }

        if (!moving)
        {
            if (disableBarriersAnchor1)
            {
                foreach (var barrier in barriersAnchor1)
                {
                    barrier.enabled = false;
                }
            }

            if (disableBarriersAnchor2)
            {
                foreach (var barrier in barriersAnchor2)
                {
                    barrier.enabled = false;
                }
            }
        }
    }

    protected override void OnActivate()
    {
        moving = true;

        foreach (var barrier in barriersAnchor1)
        {
            barrier.enabled = true;
        }
    }

    protected override void OnDeactivate()
    {
        moving = true;

        foreach (var barrier in barriersAnchor2)
        {
            barrier.enabled = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.parent = transform;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.parent = null;
        }
    }
}
