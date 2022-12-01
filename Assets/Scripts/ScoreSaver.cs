using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreSaver : MonoBehaviour
{
    public Text scoreText;

    public int score = ScoreManager.Instance.newscore;



    // Start is called before the first frame update
    void Start()
    {
        score = PlayerPrefs.GetInt("newscore", 0);
        scoreText.text = "SCORE: " + score.ToString();
    }
}
