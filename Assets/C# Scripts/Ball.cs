using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private static float z;
    private float height = 0.58f, speed = 6;
    private bool move;
    private static Color currentColor;
    public static Color Color { get { return currentColor; } set { currentColor = value; } }

    private MeshRenderer mesRenderer;

    void Start()
    {
        move = false;
    }

    private void Awake()
    {
        mesRenderer = GetComponent<MeshRenderer>();
    }

    void Update()
    {
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
    }

    private void OnTriggerEnter(Collider target)
    {

        switch (target.tag)
        {
            case "Hit":
                print("We Hit th Wall");
                break;
            case "Fail":
                print("Game Over");
                break;
            case "FinishLine":
                print("Finish The game");
                break;
            default:
                break;
        }
    }

}
