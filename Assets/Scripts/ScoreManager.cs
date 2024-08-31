using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    private Text _scoreText;
    public int score = 10;

    private void Awake()
    {
        _scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
        MakeSingleton();
    }

    // Start is called before the first frame update
    void Start()
    {
        AddScore(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (_scoreText == null)
        {
            _scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
            _scoreText.text = score.ToString();
        }
    }
    private void MakeSingleton()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void AddScore(int amount)
    {
        score += amount;
        if (score > PlayerPrefs.GetInt("HighSocre", 0))
        {
            PlayerPrefs.SetInt("HighSocre", 0);
        }

        _scoreText.text = score.ToString();
    }

    public void ResetScore()
    {
        score = 0;
    }
}

