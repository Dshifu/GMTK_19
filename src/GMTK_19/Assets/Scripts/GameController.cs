using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    [SerializeField] private GameObject gameOver = null;
    [SerializeField] private GameObject hapyEnd = null;
    [SerializeField] private Animator _fadingAnimator = null;
    
    [HideInInspector]
    public GameObject player;
    [HideInInspector]
    public PanicLevel panicLevel;
    
    private void Awake()
    {
        Instance = this;
    }
    
    [Button]
    public void GameOver(bool isWin)
    {
        var characterMovementController = player.GetComponent<CharacterMovementController>();
        characterMovementController.characterRotationSpeedMultiplier = 0;
        characterMovementController.characterVerticalSpeedMultiplier = 0;
        panicLevel.isGameOver = true;
        
        StartCoroutine(StartFading(isWin));
    }

    private IEnumerator StartFading(bool isWin)
    {
        _fadingAnimator.SetBool(PrefsName.AnimatorState.StartFading, true);
        _fadingAnimator.SetBool(PrefsName.AnimatorState.StartUnFading, false);
        yield return new WaitForSeconds(2f);
        
        if(isWin)
            hapyEnd.SetActive(true);
        else
            gameOver.SetActive(true);
        
        yield return new WaitForSeconds(3f);
        
        SceneManager.LoadScene(0);
    }
}
