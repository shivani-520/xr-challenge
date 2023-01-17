using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TMP_Text scoreText;
    public int scoreCount;

    public static ScoreManager instance;

    private void Awake()
    {
        instance = this;
    }


    // Update is called once per frame
    void Update()
    {
        scoreText.text = scoreCount.ToString("STARS 0");
    }

}
