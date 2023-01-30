using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private Animator transitionAnimator;

    public void StartButton()
    {
        StartCoroutine(Delay());
    }

    public void RestartButton()
    {
        StartCoroutine(DelayRestart());
    }

    public void ExitButton()
    {
        Application.Quit();
    }

    IEnumerator Delay()
    {
        transitionAnimator.SetTrigger("end");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    IEnumerator DelayRestart()
    {
        transitionAnimator.SetTrigger("end");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Level1");
    } 

}
