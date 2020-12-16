using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private static float z;
    private float height = 0.58f;
    public static float speed = 8;
    private bool move,isRising;
    private static Color currentColor;
    public static Color Color { get { return currentColor; } set { currentColor = value; } }
    private float lerpAmount;


    private MeshRenderer mesRenderer;

    void Start()
    {
        move = false;
    }

    private void Awake()
    {
        mesRenderer = GetComponent<MeshRenderer>();
    }

    void FixedUpdate()
    {
        if (!move)
        {
            Ball.z = 0;
        }
        if (Tap.GetIsTapped())
            move = true;
        if (move)
            Ball.z += speed * 0.025f;

        transform.position = new Vector3(0, height, Ball.z);
        UpdateColor();
    }


    public static float GetZ()
    {
        return Ball.z;
    }

    void UpdateColor()
    {
        mesRenderer.sharedMaterial.color = currentColor;
        if (isRising)
        {
            currentColor = Color.Lerp(mesRenderer.material.color, GameObject.FindGameObjectWithTag("ColorRing")
                .GetComponent<ColorBump>().GetColor(), lerpAmount);
            lerpAmount += Time.deltaTime;
        }
        if (lerpAmount >= 1)
        {
            isRising = false;
        }
    }

    private void OnTriggerEnter(Collider target)
    {

        switch (target.tag)
        {
            case "Hit":
                Destroy(target.transform.parent.gameObject);
                break;
            case "Fail":
                StartCoroutine(GameOver());
                break;
            case "ColorRing":
                lerpAmount = 0;
                isRising = true;
                break;
            case "FinishLine":
                StartCoroutine(PlayNewLevel());
                break;
            default:
                break;
        }
    }
    IEnumerator GameOver()
    {
        move = false;
        yield break;
    }
    IEnumerator PlayNewLevel()
    {
        Camera.main.GetComponent<CameraFollow>().enabled = false;
        yield return new WaitForSeconds(1.5F);
        move = false;
        Camera.main.GetComponent<CameraFollow>().enabled = true;
        GameController.instance.GenerateLevel();
    }
}
