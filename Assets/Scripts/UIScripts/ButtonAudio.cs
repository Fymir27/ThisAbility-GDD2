using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonAudio : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        InterfaceAudioManager.Instance.Click();
    }

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        InterfaceAudioManager.Instance.Hover();
    }
}
