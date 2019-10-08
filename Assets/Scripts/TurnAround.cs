using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnAround : MonoBehaviour
{
    // to be checked by wheelchair character
    // gets set when he is touching this object's collider
    public static bool Possible;

    //[SerializeField]
    //bool possibleView;

    //private void Update()
    //{
    //    possibleView = Possible;
    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == Controller.Instance.WheelchairCharacter.gameObject)
        {
            Possible = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == Controller.Instance.WheelchairCharacter.gameObject)
        {
            Possible = false;
        }
    }
}
