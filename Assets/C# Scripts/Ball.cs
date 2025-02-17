﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private static float z;
    private float height = 0.58f;
    public static float speed = 8;
    private bool move, isRising;
    private static Color currentColor;
    public static int WallCount = 0;
    public static Color Color { get { return currentColor; } set { currentColor = value; } }
    private float lerpAmount;

    public static int score = 0;

    private SpriteRenderer splash;

    private MeshRenderer mesRenderer;

    private AudioSource failSound, hitSound, levelCompleteSound;

    void Start()
    {
        move = false;
        Color = GameController.instance.hitColor;
        
    }

    private void Awake()
    {
        failSound = GameObject.Find("FailSound").GetComponent<AudioSource>();
        hitSound = GameObject.Find("HitSound").GetComponent<AudioSource>();
        levelCompleteSound = GameObject.Find("LevelComplete").GetComponent<AudioSource>();

        mesRenderer = GetComponent<MeshRenderer>();

        splash = transform.GetChild(0).GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
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
                if (WallCount >= GameObject.FindGameObjectsWithTag("HitWall").Length)
                {
                    score++;
                    WallCount--;
                    GameObject pointDisplay = Instantiate(Resources.Load("PointDisplay"),
                        transform.position, Quaternion.identity) as GameObject;
                    pointDisplay.transform.SetParent(transform);
                    if (score % 10 == 0)
                        pointDisplay.GetComponent<PointDisplay>().SetText("Perfect " + score);
                    else
                        pointDisplay.GetComponent<PointDisplay>().SetText(score.ToString());
                }
                hitSound.Play();
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
        Tap.isTapped = false;
        move = false;
        failSound.Play();
        splash.transform.position = new Vector3(0, 0.6f, Ball.z - 0.06f);
        splash.transform.eulerAngles = new Vector3(0, 0, UnityEngine.Random.value * 360);
        splash.color = currentColor;
        splash.enabled = true;
        mesRenderer.enabled = false;

        yield return new WaitForSeconds(1.5f);

        Camera.main.GetComponent<CameraFollow>().Flash();
        splash.enabled = false;
        mesRenderer.enabled = true;
        Ball.z = 0;
    }
    IEnumerator PlayNewLevel()
    {
        Camera.main.GetComponent<CameraFollow>().enabled = false;
        levelCompleteSound.Play();
        yield return new WaitForSeconds(1.5F);

        move = false;
        Camera.main.GetComponent<CameraFollow>().Flash();
        PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level") + 1);
        Camera.main.GetComponent<CameraFollow>().enabled = true;
        GameController.instance.GenerateLevel();
        Ball.z = 0;
    }
}
