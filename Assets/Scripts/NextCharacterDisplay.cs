using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NextCharacterDisplay : MonoBehaviour
{
    Text text;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        var contr = Controller.Instance;

        if(contr.NextCharacter == contr.DeafCharacter)
        {
            text.text = "Deaf";
        }
        else if (contr.NextCharacter == contr.BlindCharacter)
        {
            text.text = "Blind";
        }
        else if (contr.NextCharacter == contr.WheelchairCharacter)
        {
            text.text = "Wheelchair";
        }
    }
}
