using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{

    public static ScoreManager instance;
    public TextMeshPro ScoreText;

    private int score = 0;

    public int Score
    {
        get => score; set
        {
            score = value;
            ScoreText.text = "Score: " + score.ToString();
        }
    }

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    
}
