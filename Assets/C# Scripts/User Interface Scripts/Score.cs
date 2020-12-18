using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    private Text scoreText;
    private Text bestScoreText;
    
    void Awake()
    {
        scoreText = transform.GetChild(1).GetComponent<Text>();
        bestScoreText = transform.GetChild(0).GetComponent<Text>();
    }

    
    void Update()
    {
        if (Ball.GetZ() == 0)
        {
            bestScoreText.gameObject.SetActive(true);
            scoreText.gameObject.SetActive(false);
        }
        else
        {
            bestScoreText.gameObject.SetActive(false);
            scoreText.gameObject.SetActive(true);
        }
        scoreText.text = Ball.score.ToString();
        if (Ball.score > PlayerPrefs.GetInt("HighScore",0))
            PlayerPrefs.SetInt("HighScore",Ball.score);
        bestScoreText.text = PlayerPrefs.GetInt("HighScore", 0).ToString();
    }
}
