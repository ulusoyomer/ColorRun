﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallFragment : MonoBehaviour
{
    private MeshRenderer meshRenderer;

    void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    void Start()
    {
        if (gameObject.tag == "Hit")
        {
            meshRenderer.material.color = GameController.instance.hitColor;
        }
        else
        {
            meshRenderer.material.color = GameController.instance.failColor;
        }
    }
}
