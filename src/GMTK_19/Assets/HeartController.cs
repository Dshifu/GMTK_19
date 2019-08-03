using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartController : MonoBehaviour
{
    [SerializeField] private PanicLevel panicLevel = null;
    private Animator animator = null;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        animator.SetFloat(PrefsName.AnimatorState.HeartRate, panicLevel.GetPanicLevel);
    }
}