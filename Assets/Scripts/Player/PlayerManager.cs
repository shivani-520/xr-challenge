using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    private TransitionManager transitions;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        transitions = TransitionManager.instance;
    }

    public GameObject player;

    private void Update()
    {
        if(player == null)
        {
            transitions.StartCoroutine(transitions.SameScene());
        }
    }

}
