using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class PanicLevel : MonoBehaviour
{
    private float timeForFullPanic = 60f;
    [SerializeField] private Transform panicLevelTransform = null; 

    private void Start()
    {
        panicLevelTransform.localScale = new Vector3(0f, 1f);
    }

    private void Update()
    {
        ChangePanic(Time.deltaTime/timeForFullPanic);
    }

    /// <param name="normalizeValue">Value set from 0 to 1</param>
    [Button]
    public void ChangePanic(float normalizeValue)
    {
        var localScale = panicLevelTransform.localScale;

        localScale = new Vector3(localScale.x + normalizeValue, localScale.y);

        if (localScale.x > 1)
            localScale.x = 1;
        else if (localScale.x < 0)
            localScale.x = 0;
        
        
        panicLevelTransform.localScale = localScale;
    }

    public void AddTimeForFullPanic(float additionalTimeForFullPanic)
    {
        timeForFullPanic += additionalTimeForFullPanic;
    }

    public void RemoveTimeForFullPanic(float additionalTimeForFullPanic)
    {
        timeForFullPanic -= additionalTimeForFullPanic;
    }

    /// <summary>
    /// Panic level from 0 to 1
    /// </summary>
    /// <returns></returns>
    public float GetPanicLevel
        => panicLevelTransform.localScale.x;
}
