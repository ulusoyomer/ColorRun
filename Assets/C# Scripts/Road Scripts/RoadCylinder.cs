using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadCylinder : MonoBehaviour
{
    private GameObject road;

    void Start()
    {
        
    }
    private void Awake()
    {
        road = GameObject.Find("Road");
    }
    void Update()
    {
        transform.eulerAngles = new Vector3(0, 0, road.gameObject.transform.eulerAngles.z % 25);
    }
}
