using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : MonoBehaviour
{
    private bool movable = true;

    private float angle,lastDeltaAngle,lastTouchX;


    
    void Update()
    {
        // Road Turn
        if (movable && Tap.GetIsTapped())
        {
            float mouseX = this.GetMouseX();
            lastDeltaAngle = lastTouchX - mouseX;
            angle += lastDeltaAngle * 360 * 1.7f;
            lastTouchX = mouseX;
        }
        else if (lastDeltaAngle != 0)
        {
            lastDeltaAngle -= lastDeltaAngle * 5 * Time.deltaTime;
            angle += lastDeltaAngle * 360 * 1.7f;
        }

        transform.eulerAngles = new Vector3(0, 0, angle);
        // Road Turn End
    }

    private float GetMouseX()
    {
        return Input.mousePosition.x / (float)Screen.width;
    }
}
