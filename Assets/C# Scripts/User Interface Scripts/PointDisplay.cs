using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointDisplay : MonoBehaviour
{

    private TextMesh textMesh;

    void Awake()
    {
        textMesh = GetComponent<TextMesh>();
    }

    public void SetText(string text)
    {
        textMesh.text = text;
        textMesh.color = Color.white;
    }

    void Update()
    {
        transform.localPosition = new Vector3(0, 0, Ball.GetZ());
        Destroy(gameObject, 0.30f);
    }
}
