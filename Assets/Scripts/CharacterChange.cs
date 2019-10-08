using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterChange : MonoBehaviour
{
    public List<Transform> disabled;
    public Transform mainCamera;
    Vector3 currentFocus;
    public Vector3 cameraOffset = new Vector3(0, 0, 0);

    int currentCharacter = 0;

    void Start()
    {
        currentFocus = cameraOffset + disabled[currentCharacter].position;
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            currentCharacter++;
            if (currentCharacter > 2) currentCharacter = 0;
        }

       
       // mainCamera.transform.position = disabled[currentCharacter].position + cameraOffset;

    }
}
