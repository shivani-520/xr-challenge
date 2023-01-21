using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyType : MonoBehaviour
{
    public enum EnemyTypes { Easy, Medium, Hard }

    public EnemyTypes whatTypeIsThisEnemy;

    private Animator enemyAnimator;


    private void Awake()
    {
        enemyAnimator = GetComponent<Animator>();
    }

    public void CorrectAnimation()
    {
        switch (whatTypeIsThisEnemy)
        {
            case EnemyTypes.Easy:
                enemyAnimator.SetTrigger("EasyHit");
                Debug.Log("EasyEnemy");
                break;
            case EnemyTypes.Medium:
                enemyAnimator.SetTrigger("MediumHit");
                Debug.Log("MediumEnemy");
                break;
            case EnemyTypes.Hard:
                enemyAnimator.SetTrigger("HardHit");
                Debug.Log("HardEnemy");
                break;
            default:
                break;
        }
    }

}
