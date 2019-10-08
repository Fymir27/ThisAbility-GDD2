using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoAnimator : MonoBehaviour
{
    [SerializeField]
    float timeToSwitch;

    [SerializeField]
    Sprite[] sprites;

    [SerializeField]
    int currentSprite;

    float timePassed;

    bool stop = true;

    // Start is called before the first frame update
    void Start()
    {
        currentSprite = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (stop)
        {
            return;
        }

        timePassed += Time.deltaTime;

        if (timePassed >= timeToSwitch)
        {
            timePassed = 0f;
            currentSprite = (currentSprite + 1) % sprites.Length;
            GetComponent<SpriteRenderer>().sprite = sprites[currentSprite];
        }
    }

    public void StopAnimation()
    {
        stop = true;
        GetComponent<SpriteRenderer>().sprite = sprites[0];
    }

    public void StartAnimation()
    {
        stop = false;
    }
}
