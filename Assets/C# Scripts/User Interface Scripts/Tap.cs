using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tap : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private static bool isTapped;
    public static bool GetIsTapped()
    {
        return isTapped;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        isTapped = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isTapped = false;
    }
}
