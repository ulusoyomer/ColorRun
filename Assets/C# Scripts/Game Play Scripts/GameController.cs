using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance { get; set; }

    private GameObject finishLine;

    public Color[] colors;
    [HideInInspector]
    public Color hitColor, failColor;

    private int wallsSpawnNumber = 11;
    private float z = 7;

    private bool colorBump;

    void Awake()
    {
        instance = this;
        GenerateColors();
        finishLine = Instantiate(Resources.Load("FinishLine") as GameObject, transform.position, Quaternion.identity);
        finishLine.tag = "FinishLine";
    }

    void Start()
    {   
        SpawnWalls();
    }

    void GenerateColors()
    {
        hitColor = colors[Random.Range(0, colors.Length)];
        failColor = colors[Random.Range(0, colors.Length)];
        while (failColor == hitColor)
            failColor = colors[Random.Range(0, colors.Length)];

        Ball.Color = hitColor;

    }
    void SpawnWalls()
    {
        for (int i = 0; i < wallsSpawnNumber; i++)
        {
            GameObject wall;

            if (Random.value <= 0.2 && !colorBump)
            {
                colorBump = true;
                wall = Instantiate(Resources.Load("ChangeColor") as GameObject, transform.position, Quaternion.identity);
            }
            else
            {
                wall = Instantiate(Resources.Load("Wall") as GameObject, transform.position, Quaternion.identity);
            }

            wall.transform.SetParent(GameObject.Find("Road").transform);
            wall.transform.localPosition = new Vector3(0,0,z);
            float randomRotation = Random.Range(0, 360);
            wall.transform.localRotation = Quaternion.Euler(new Vector3(0,0, randomRotation));

            z += 7;
            finishLine.transform.localPosition = new Vector3(0, 0, z);
        }       
    }
}
