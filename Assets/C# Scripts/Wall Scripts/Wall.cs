using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    private GameObject wallFragment;
    private GameObject perfectStar;
    private GameObject wall1, wall2;

    private float rotationZ;
    private float rotationZMax = 180;


    void Awake()
    {
        wallFragment = Resources.Load("Wall Fragment") as GameObject;
        perfectStar = Resources.Load("PerfectStar") as GameObject;
    }
    private void Start()
    {
        SpawnWallFragments();
    }
    void SpawnWallFragments()
    {
        wall1 = new GameObject();
        wall2 = new GameObject();

        wall1.name = "Wall1";
        wall2.name = "Wall2";



        wall1.transform.SetParent(transform);
        wall2.transform.SetParent(transform);


        wall1.transform.localPosition = Vector3.zero;
        wall2.transform.localPosition = Vector3.zero;

        

        for (int i = 0; i < 100; i++)
        {
            GameObject WallF = Instantiate(wallFragment, transform.position, Quaternion.Euler(0, 0, rotationZ));
            rotationZ += 3.6f;

            if (rotationZ <= rotationZMax)
            {
                WallF.transform.SetParent(wall1.transform);
                WallF.tag = "Hit";
            }
            else
            {
                WallF.transform.SetParent(wall2.transform);
                WallF.tag = "Fail";
            }
        }


        wall1.transform.localRotation = Quaternion.Euler(Vector3.zero);
        wall2.transform.localRotation = Quaternion.Euler(Vector3.zero);
        wall1.tag = "HitWall";
        wall2.tag = "FailWall";
        var wallFragmentChild = wall1.transform.GetChild(25).gameObject;
        AddStar(wallFragmentChild);
    }


    void AddStar(GameObject wallFragmentChild)
    {
        var star = Instantiate(perfectStar, transform.position, Quaternion.identity);
        star.transform.SetParent(wallFragmentChild.transform);
        star.transform.localPosition = new Vector3(0.05f,0.75f, -0.06f);
    }


}
