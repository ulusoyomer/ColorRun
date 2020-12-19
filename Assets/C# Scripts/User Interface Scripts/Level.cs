using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level : MonoBehaviour
{
    private Image currrentTickBoxImage;
    private Image endLevel;
    private Image progression;
    private Image[] alwaysColoredImages = new Image[3];

    private Text endLevelText;
    private Text startLevelText;
    private Text currentTickBoxText;

    [SerializeField]
    private Text levelCompleteMessage;

    private RectTransform currentTickBox;

    private Color color;
    void Awake()
    {
        alwaysColoredImages[0] = base.transform.GetChild(0).GetComponent<Image>();
        alwaysColoredImages[1] = base.transform.GetChild(1).GetComponent<Image>();
        alwaysColoredImages[2] = base.transform.GetChild(3).GetComponent<Image>();
        endLevel = base.transform.GetChild(4).GetComponent<Image>();
        progression = base.transform.GetChild(2).GetChild(0).GetComponent<Image>();

        endLevelText = endLevel.transform.GetChild(0).GetComponent<Text>();
        startLevelText = base.transform.GetChild(3).GetChild(0).GetComponent<Text>();



        currentTickBox = base.transform.GetChild(2).GetChild(1).GetComponent<RectTransform>();
        currrentTickBoxImage = currentTickBox.GetComponent<Image>();
        currentTickBoxText = currentTickBox.GetChild(0).GetComponent<Text>();
    }


    void Update()
    {
        if (progression.fillAmount != 1)
            SetProgression(Ball.GetZ() / GameController.instance.GetFinisLineDistance());
        else if (progression.fillAmount >= 1 && Ball.GetZ() == 0)
        {
            SetProgression(0);
        }
        UpdateColors();
        startLevelText.text = PlayerPrefs.GetInt("Level").ToString();
        endLevelText.text = (PlayerPrefs.GetInt("Level") + 1).ToString();
    }

    private void SetProgression(float presentage)
    {
        progression.fillAmount = presentage;
        currentTickBox.anchorMin = new Vector2(presentage, 0);
        currentTickBox.anchorMax = currentTickBox.anchorMin;
        currentTickBoxText.text = Mathf.RoundToInt(presentage * 100) + " %";
    }

    private void UpdateColors()
    {
        color = Ball.Color;
        if (progression.fillAmount == 1)
        {
            endLevel.color = this.color;
            endLevelText.color = Color.white;

            levelCompleteMessage.gameObject.SetActive(true);
            levelCompleteMessage.color = color;
            levelCompleteMessage.text = "Level " + PlayerPrefs.GetInt("Level") + " Complete !";
        }
        else
        {
            endLevel.color = Color.white;
            endLevelText.color = color;
            levelCompleteMessage.gameObject.SetActive(false);
        }

        foreach (Image image in alwaysColoredImages)
        {
            image.color = color;
        }

        progression.color = color;
        currrentTickBoxImage.color = color;
    }

}
