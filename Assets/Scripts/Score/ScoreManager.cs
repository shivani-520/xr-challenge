using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TMP_Text scoreText;
    public int scoreCount;

    public static ScoreManager instance;

    public int scoreForLevel;

    public GameObject finishDoor;

    private void Awake()
    {
        instance = this;
    }


    // Update is called once per frame
    void Update()
    {
        scoreText.text = scoreCount.ToString("STARS 0");

        if(scoreCount >= scoreForLevel)
        {
            finishDoor.SetActive(true);
        }
    }

}
