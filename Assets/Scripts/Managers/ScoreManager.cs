using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    [SerializeField] private EnemySpawner enemySpawner;

    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private Animator doorAnimator;


    public int scoreCount;
    public int scoreForLevel;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }


    // Update is called once per frame
    void Update()
    {
        scoreText.text = scoreCount.ToString("STARS 0");

        if(scoreCount >= scoreForLevel)
        {
            doorAnimator.SetTrigger("Finish");
            enemySpawner.enabled = false;
        }
    }

}
