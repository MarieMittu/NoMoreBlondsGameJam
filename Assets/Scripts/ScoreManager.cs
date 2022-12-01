using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{

    public Text scoreText;
    public int score = 0;
    public int newscore = 0;

    private static ScoreManager _instance;

    private ScoreManager() { }

    public static ScoreManager Instance
    {
        get
        {
            if (_instance == null)
                _instance = new ScoreManager();

            return _instance;
        }
    }


    void Awake()
    {
        _instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        newscore = PlayerPrefs.GetInt("newscore", score);
        scoreText.text = score.ToString();
    }

    public void AddScore()
    {
        score += 10;
        scoreText.text = score.ToString();
        PlayerPrefs.SetInt("newscore", score);
        Debug.Log("SCORE: " + score);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
