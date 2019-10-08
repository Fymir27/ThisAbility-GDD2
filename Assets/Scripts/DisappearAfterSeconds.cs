using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearAfterSeconds : MonoBehaviour
{
    [SerializeField]
    float timeToLive;

    float timeAlive = 0f;

    // Update is called once per frame
    void Update()
    {
        timeAlive += Time.deltaTime;

        if (timeAlive >= timeToLive)
        {
            gameObject.SetActive(false);
            timeAlive = 0f;
        }
    }

    private void OnDisable()
    {
        timeAlive = 0f;
    }
}
