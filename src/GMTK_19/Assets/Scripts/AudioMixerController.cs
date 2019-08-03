using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Audio;

public class AudioMixerController : MonoBehaviour
{
    [Required]
    [SerializeField] private AudioMixer audioMixer;
    [Required]
    [SerializeField] private PanicLevel panicLevel;

    private const float HighVolumeLimit = 0f;
    private const float LowVolumeLimit = -80f;

    private const float Coef = 1280f;
    
    private const float MediumPanicLevel = 0.25f;
    private const float HighPanicLevel = 0.7f;
    private const float MediationPanicLevel = 0.15f;

    private void Start()
    {
        audioMixer.SetFloat(PrefsName.LowPanicVolume, HighVolumeLimit);
        audioMixer.SetFloat(PrefsName.MediumPanicVolume, LowVolumeLimit);
        audioMixer.SetFloat(PrefsName.HighPanicVolume, LowVolumeLimit);
    }

    // Update is called once per frame
    private void Update()
    {
        var panicLevelValue = panicLevel.GetPanicLevel;
        if(panicLevelValue > 0f && panicLevelValue < MediumPanicLevel)
            audioMixer.SetFloat(PrefsName.LowPanicVolume, (-Coef * Mathf.Pow(panicLevelValue, 2)));
        if (panicLevelValue > MediumPanicLevel - MediationPanicLevel && panicLevelValue < HighPanicLevel)
            audioMixer.SetFloat(PrefsName.MediumPanicVolume, (-Coef * Mathf.Pow(panicLevelValue, 2)));
        if (panicLevelValue > HighPanicLevel - MediationPanicLevel)
            audioMixer.SetFloat(PrefsName.HighPanicVolume, (-Coef * Mathf.Pow(panicLevelValue, 2)));
    }
}
