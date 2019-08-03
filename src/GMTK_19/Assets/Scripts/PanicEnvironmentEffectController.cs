using System;
using Sirenix.OdinInspector;
using UnityEngine;

[RequireComponent(typeof(Collider2D), typeof(Rigidbody2D))]
public class PanicEnvironmentEffectController : MonoBehaviour
{
    [Required] [SerializeField] private PanicLevel panicLevel = null;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag(PrefsName.ReducerPanic))
        {
            var environment = other.GetComponent<PanicEnvironmentEffect>();
            panicLevel.ChangePanic(environment.reducer);
            environment.CollectEnvironmentReducer();
        }

        if (other.transform.CompareTag(PrefsName.VelocityPanic))
            panicLevel.SetTimeForFullPanic(other.GetComponent<PanicEnvironmentEffect>().timeForFullPanic);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.CompareTag(PrefsName.VelocityPanic))
            panicLevel.ResetTimeForFullPanic();
    }
}
