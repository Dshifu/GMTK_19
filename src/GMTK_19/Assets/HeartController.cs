using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartController : MonoBehaviour
{
    [SerializeField] private PanicLevel panicLevel = null;
    private Animator animator = null;
    private float MINIMUM_HEART_RATE_MULTIPLIER = 0.3f;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        float heartRateMultiplier = Mathf.Max(MINIMUM_HEART_RATE_MULTIPLIER, panicLevel.GetPanicLevel);
        if (heartRateMultiplier >= 1)
            heartRateMultiplier = 0;
        animator.SetFloat(PrefsName.AnimatorState.HeartRate, heartRateMultiplier);
    }
}