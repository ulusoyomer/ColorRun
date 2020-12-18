using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance { get; set; }
    private GameObject finishLine;
    private float maxSpeed = 20;
    public Color[] colors;
    [HideInInspector]
    public Color hitColor, failColor;

    private static int wallsSpawnNumber = 11;
    private float z = 7;

    private bool colorBump;

    void Awake()
    {
        instance = this;
        GenerateColors();
        finishLine = Instantiate(Resources.Load("FinishLine") as GameObject, transform.position, Quaternion.identity);
        finishLine.tag = "FinishLine";
        PlayerPrefs.SetInt("Level", 1);
    }

    void Start()
    {
        SpawnWalls();
        
    }

    public void GenerateLevel()
    {
        if (PlayerPrefs.GetInt("Level") >= 1 && PlayerPrefs.GetInt("Level") <= 4)
        {
            wallsSpawnNumber += 2;
            if (maxSpeed >= Ball.speed)
                Ball.speed += 0.25f;
        }
        else if (PlayerPrefs.GetInt("Level") >= 5 && PlayerPrefs.GetInt("Level") <= 10)
        {
            wallsSpawnNumber += 3;
            if (maxSpeed >= Ball.speed)
                Ball.speed += 0.50f;
        }
        else
        {
            wallsSpawnNumber += 4;
            if (maxSpeed >= Ball.speed)
                Ball.speed += 0.75f;
        }
        DeleteWalls();
        z = 7;
        colorBump = false;
        SpawnWalls();
        Ball.WallCount = wallsSpawnNumber;
    }

    private void DeleteWalls()
    {
        var wallList = GameObject.FindGameObjectsWithTag("Wall");
        var colorRingList = GameObject.FindGameObjectsWithTag("ColorRing");
        if (wallList.Length > 1)
        {
            foreach (var item in wallList)
            {
                Destroy(item.transform.gameObject);
            }
            if (colorRingList.Length > 0)
            {
                foreach (var item2 in colorRingList)
                {
                    Destroy(item2.gameObject);
                }
            }
        }

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

            if (Random.value <= 0.2 && !colorBump && PlayerPrefs.GetInt("Level") > 2)
            {
                colorBump = true;
                wall = Instantiate(Resources.Load("ChangeColor") as GameObject, transform.position, Quaternion.identity);
                z += 7;
            }
            else if (Random.value <= 0.2 && PlayerPrefs.GetInt("Level") > 5)
            {
                wall = Instantiate(Resources.Load("Wall") as GameObject, transform.position, Quaternion.identity);
                z += 5;
            }
            else if (i >= wallsSpawnNumber - 3 && !colorBump && PlayerPrefs.GetInt("Level") > 2)
            {
                colorBump = true;
                wall = Instantiate(Resources.Load("ChangeColor") as GameObject, transform.position, Quaternion.identity);
                z += 7;
            }
            else
            {
                wall = Instantiate(Resources.Load("Wall") as GameObject, transform.position, Quaternion.identity);
                z += 7;
            }

            wall.transform.SetParent(GameObject.Find("Road").transform);
            wall.transform.localPosition = new Vector3(0, 0, z);
            float randomRotation = Random.Range(0, 360);
            wall.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, randomRotation));

            finishLine.transform.localPosition = new Vector3(0, 0, z + 7);
        }
        Ball.WallCount = wallsSpawnNumber;
    }
}
