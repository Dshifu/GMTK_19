﻿using System;
using Sirenix.OdinInspector;
using UnityEngine;

[RequireComponent(typeof(Collider2D), typeof(Rigidbody2D))]
public class PanicEnvironmentEffectController : MonoBehaviour
{
    [Required] public PanicLevel panicLevel = null;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.CompareTag(PrefsName.ReducerPanic))
        {
            var environment = other.GetComponent<PanicEnvironmentEffect>();
            panicLevel.ChangePanic(environment.reducer);
            environment.CollectEnvironmentReducer();
        }

        if (other.transform.CompareTag(PrefsName.VelocityPanic))
            panicLevel.AddTimeForFullPanic(other.GetComponent<PanicEnvironmentEffect>().timeForFullPanic);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.transform.CompareTag(PrefsName.VelocityPanic))
            panicLevel.RemoveTimeForFullPanic(other.GetComponent<PanicEnvironmentEffect>().timeForFullPanic);
    }
}
